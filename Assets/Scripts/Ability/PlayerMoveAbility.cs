using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using TbsFramework.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TbsFramework.Units.Abilities
{
    public class PlayerMoveAbility : Ability
    {
        public Cell Destination { get; set; }
        private IList<Cell> currentPath;
        public HashSet<Cell> availableDestinations;

        public RealPlayer Player { get; set; }

        public override void Initialize()
        {
            Player = GetComponent<RealPlayer>();
        }

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {

            if (Player.currentActionPoints > 0 && availableDestinations.Contains(Destination))
            {
                var path = Player.FindPath(cellGrid.Cells, Destination);
                // var totalMovementCost = path.Sum(c => c.MovementCost);
                // var unit = Player;
                // if (unit!= null)
                // {
                //     unit.ConsumeActionPoints(totalMovementCost);
                // }
                yield return Player.Move(Destination, path);
            }
            yield return base.Act(cellGrid, isNetworkInvoked);
        }

        public override void Display(CellGrid cellGrid)
        {
            if (Player.currentActionPoints > 0)
            {
                foreach (var cell in availableDestinations)
                {
                    cell.MarkAsReachable();
                }
            }
        }

        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {

            if (IsPointerOverUIObject()) 
            {
            Debug.Log("Over UI");    
            return;}

            if (cellGrid.GetCurrentPlayerUnits().Contains(unit))
            {
                cellGrid.cellGridState = new CellGridStateAbilitySelected(cellGrid, unit, unit.GetComponents<Ability>().ToList());
            }
        }

        public override void OnCellClicked(Cell cell, CellGrid cellGrid)
        {

            if (IsPointerOverUIObject()) return;    

            if (availableDestinations.Contains(cell))
            {
                Destination = cell;
                currentPath = null;
                StartCoroutine(HumanExecute(cellGrid));
            }
            else
            {
                cellGrid.cellGridState = new CellGridStateWaitingForInput(cellGrid);
            }
        }

        public override void OnCellSelected(Cell cell, CellGrid cellGrid)
        {
            if (IsPointerOverUIObject()) return;

            if (Player.currentActionPoints > 0 && availableDestinations.Contains(cell))
            {
                currentPath = Player.FindPath(cellGrid.Cells, cell);
                foreach (var c in currentPath)
                {
                    c.MarkAsPath();
                }
            }
        }

        public override void OnCellDeselected(Cell cell, CellGrid cellGrid)
        {

            if (Player.currentActionPoints > 0 && availableDestinations.Contains(cell))
            {
                if (currentPath == null)
                {
                    return;
                }
                foreach (var c in currentPath)
                {
                    c.MarkAsReachable();
                }
            }
        }

        public override void OnAbilitySelected(CellGrid cellGrid)
        {
            Player.CachePaths(cellGrid.Cells);
            availableDestinations = Player.GetAvailableDestinations(cellGrid.Cells);
        }

        public override void CleanUp(CellGrid cellGrid)
        {
            foreach (var cell in availableDestinations)
            {
                cell.UnMark();
            }
        }

        public override bool CanPerform(CellGrid cellGrid)
        {
            return Player.ActionPoints > 0 && Player.GetAvailableDestinations(cellGrid.Cells).Count > 0;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            var actionParams = new Dictionary<string, string>();

            actionParams.Add("destination_x", Destination.OffsetCoord.x.ToString());
            actionParams.Add("destination_y", Destination.OffsetCoord.y.ToString());

            return actionParams;
        }

        public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams, bool isNetworkInvoked = false)
        {
            var actionDestination = cellGrid.Cells.Find(c => c.OffsetCoord.Equals(new UnityEngine.Vector2(float.Parse(actionParams["destination_x"]), float.Parse(actionParams["destination_y"]))));
            Destination = actionDestination;
            yield return StartCoroutine(RemoteExecute(cellGrid));
        }


        //UI Dectection
        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

    }
}