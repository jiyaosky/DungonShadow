using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class PlayerAttackAbility : Ability
    {
        public Unit UnitToAttack { get; set; }
        public int UnitToAttackID { get; set; }

        List<Unit> inAttackRange;

        // public List<Ability> PlayerAttackAbilities;
        // public Ability CurrentSelectedAttackAbility;

        public int AbilityCost = 1;

        // 不同攻击方式的攻击力加成和攻击范围

        public RealPlayer Player { get; set; }

        public override void Initialize()
        {
            Player = GetComponent<RealPlayer>();
        }

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            if (CanPerform(cellGrid) && Player.IsUnitAttackable(UnitToAttack, Player.Cell))
            {
                // 根据选择的攻击方式调整攻击力和攻击范围
                // TODO:
                // int adjustedAttackFactor = Player.GetCurrentAttackFactor() + attackFactorBonuses[(int)SelectedAttackType];
                // int adjustedAttackRange = attackRanges[(int)SelectedAttackType];

                // 执行攻击
                Player.AttackHandler(UnitToAttack);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        public override void Display(CellGrid cellGrid)
        {
            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            inAttackRange = enemyUnits.Where(u => Player.IsUnitAttackable(u, Player.Cell)).ToList();
            inAttackRange.ForEach(u => u.MarkAsReachableEnemy());
        }

        // unit 是敌人
        // 
        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            // 检测当前Unit如果是可交互的则返回空
            if (unit.GetComponent<InteractiveAbility>() != null)
            {
                // Debug.Log("Unit is interactive");
                return;
            }
            if (Player.IsUnitAttackable(unit, Player.Cell))
            {
                UnitToAttack = unit;
                UnitToAttackID = UnitToAttack.UnitID;
                // 这里可以添加逻辑让玩家选择攻击方式
                // TODO:
                // CurrentSelectedAttackAbility = PlayerAttackAbilities[0]; // 默认选择第一种攻击方式，实际可以通过某种交互来让玩家选择
                StartCoroutine(HumanExecute(cellGrid));
            }
            else if (cellGrid.GetCurrentPlayerUnits().Contains(unit))
            {
                cellGrid.cellGridState = new CellGridStateAbilitySelected(cellGrid, unit, unit.GetComponents<Ability>().ToList());
            }
        }

        public override void OnCellClicked(Cell cell, CellGrid cellGrid)
        {
            cellGrid.cellGridState = new CellGridStateWaitingForInput(cellGrid);
        }

        public override void CleanUp(CellGrid cellGrid)
        {
            inAttackRange.ForEach(u =>
            {
                if (u!= null)
                {
                    u.UnMark();
                }
            });
        }

        // TODO: 这里要改一下判断条件，因为这里的逻辑是判断是否有行动点数，而不是判断是否可以攻击
        public override bool CanPerform(CellGrid cellGrid)
        {
            if (Player.ActionPoints <= 0)
            {
                return false;
            }

            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            inAttackRange = enemyUnits.Where(u => Player.IsUnitAttackable(u, Player.Cell)).ToList();

            // 根据选择的攻击方式计算消耗的行动点数并判断是否可执行
            int attackCost = GetAttackCostForSelectedType();
            return attackCost <= Player.currentActionPoints;
        }

        private int GetAttackCostForSelectedType()
        {
            // 根据选择的攻击方式确定消耗的行动点数，这里只是示例值，可以根据实际需求调整
            return AbilityCost;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            Dictionary<string, string> actionParameters = new Dictionary<string, string>();
            actionParameters.Add("target_id", UnitToAttackID.ToString());
            actionParameters.Add("attack_type", AbilityCost.ToString());

            return actionParameters;
        }

        // public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams, bool isNetworkInvoked = false)
        // {
        //     var targetID = int.Parse(actionParams["target_id"]);
        //     var target = cellGrid.Units.Find(u => u.UnitID == targetID);

        //     UnitToAttack = target;
        //     UnitToAttackID = targetID;
        //     // TODO:
        //     SelectedAttackType = (AttackType)int.Parse(actionParams["attack_type"]);
        //     yield return StartCoroutine(RemoteExecute(cellGrid));
        // }
    }
}