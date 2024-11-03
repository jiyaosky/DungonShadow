using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using Unity.VisualScripting;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class InteractiveAbility : Ability
    {
        
        public string AbilityName = "";
        // 是否为单次交互
        bool isOnceInteract = false;
        // 是否已经交互
        bool isInteracted = false;
        // 交互范围
        public int Range = 1;

        public int GainGold = 0;

        public GameObject shopPanel;

        public ShopManager shopManager;

        public CellGrid _cellGrid;

        private void Start()
        {
            isInteracted = false;
        }


        // 在当前Chest Unit的Range范围内搜索是否有包含Component<RealPlayer>();的Unit
        public bool isPlayerInRange(CellGrid cellGrid)
        {
            var currentCell = UnitReference.Cell;
            // 获取当前currentCell的周围List<Cell>
            List<Cell> cellsInRange = FindCellsInRange(cellGrid, currentCell, Range);
            // 获取当前RealPlayer的Unit
            var realPlayer = cellGrid.Units.Find(unit => unit.GetComponent<RealPlayer>() != null);
            // realPlayer的Cell
            var realPlayerCell = realPlayer.Cell;
            // 如果realPlayer的Cell在cellsInRange中，则返回true
            if (cellsInRange.Contains(realPlayerCell))
            {
                Debug.Log("Player is in range");
                return true;
            }
            Debug.Log("Player is not in range");
            return false;
        }

        public List<Cell> FindCellsInRange(CellGrid cellgrid, Cell centerCell, float range)
        {
            var centerCellOffsetCoord = centerCell.OffsetCoord;
            List<Cell> cellsInRange = new List<Cell>();

            for (int x = (int)(centerCellOffsetCoord.x - range); x <= centerCellOffsetCoord.x + range; x++)
            {
                for (int y = (int)(centerCellOffsetCoord.y - range); y <= centerCellOffsetCoord.y + range; y++)
                {
                    Cell cell = cellgrid.GetCell(x, y);
                    if (centerCell.GetDistance(cell) <= range)
                    {
                        cellsInRange.Add(cell);
                    }
                }
            }

            return cellsInRange;
        }

        public void InterPlay(string abilityName)
        {
            if (!isInteracted)
            {
                // TODO:当玩家点击这个单元格的时候与其交互吧
                switch (abilityName)
                {
                    case "Chest":
                        DoChest();
                        break;
                    case "Door":
                        DoDoor();
                        break;
                    case "NPC":
                        DoNPC();
                        break;
                    case "EndPoint":
                        DoEndPoint();
                        break;
                    default:
                        Debug.Log("Invalid action number");
                        break;
                }
            }
        }

        public void DoChest()
        {
            // GameObject currentGameObject = this.gameObject;
            var openChest = transform.Find("Chest3A");
            var closeChest = transform.Find("Chest3B");
            openChest.gameObject.SetActive(true);
            closeChest.gameObject.SetActive(false);

            if (FindObjectOfType<GoldController>() != null)
            {
                GoldController controller = FindObjectOfType<GoldController>();
                controller.UpdateValue(0, GainGold);
            }

            isInteracted = true;
        }
        
        public void DoDoor()
        {
            // GameObject currentGameObject = this.gameObject;
            var openDoor = transform.Find("DoorOpen");
            var closeDoor = transform.Find("DoorClose");

            var currentCell = UnitReference.Cell;
            bool taken = currentCell.IsTaken; 
            if (taken)
            {
                openDoor.gameObject.SetActive(true);
                closeDoor.gameObject.SetActive(false);
                currentCell.IsTaken = false;
            }
            else
            {
                openDoor.gameObject.SetActive(false);
                closeDoor.gameObject.SetActive(true);
                currentCell.IsTaken = true;
            }
        }

        public void DoNPC()
        {
            //Shop related
            shopPanel.SetActive(true);
            shopManager = shopPanel.GetComponent<ShopManager>();
            shopManager.Refresh();
        }

        public void DoEndPoint()
        {
            _cellGrid.CheckGameFinished();
        }
    }
}