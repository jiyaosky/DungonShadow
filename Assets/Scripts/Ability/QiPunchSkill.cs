using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using TbsFramework.Players;
using UnityEngine;
using UnityEngine.UIElements;

namespace TbsFramework.Units.Abilities
{
// 气冲拳技能类
    public class QiPunchSkill : SkillAbility
    {
        public int pushRange;
        // public int originAttackRange = 1;
        // 可以添加特定于 Skill 的属性和方法
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            var realPlayer = UnitReference as RealPlayer;
            realPlayer.AttackHandler(UnitToAttack, APCost, false);
            // 获取气功拳推到的目标点
            Cell pushTarget = UnitToAttack.Cell;
            IList<Cell> path = new List<Cell>();
            path.Add(pushTarget);
            Vector2 unitDirection = new Vector3(realPlayer.currentForward.x, realPlayer.currentForward.z);
            for (int i = 1; i <= pushRange; i++)
            {
                Vector2 targetPosition = realPlayer.Cell.OffsetCoord + unitDirection * i;
                Cell targetCell = cellGrid.GetCell(targetPosition);
                if (!targetCell.IsTaken)
                {
                    path.Add(targetCell);
                }

                pushTarget = targetCell;
            }

            path.Reverse();
            yield return UnitToAttack.Move(pushTarget, path);
                
        }
        
        public override bool CanPerform(CellGrid cellGrid)
        {
            if (IsActive)
            {
                return true;
            }
            return false;
        }
        

        public override void Activate()
        {
            var cellGrid = FindObjectOfType<CellGrid>();
            if (CanPerform(cellGrid))
            {
                // StartCoroutine(HumanExecute(cellGrid));
                cellGrid.cellGridState = new CellGridStateAbilitySelected(cellGrid, UnitReference, new List<Ability>() { this });
            }

            // UnitReference.AttackRange = originAttackRange;
        }

        private int originAttackRange;
        public override void OnAbilitySelected(CellGrid cellGrid)
        {
            originAttackRange = (UnitReference as RealPlayer).AttackRange;
            (UnitReference as RealPlayer).AttackRange = Range;
        }

        public override void OnAbilityDeselected(CellGrid cellGrid)
        {
            (UnitReference as RealPlayer).AttackRange = originAttackRange;
        }

        List<Unit> inAttackRange;
        public override void Display(CellGrid cellGrid)
        {
            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            inAttackRange = enemyUnits.Where(u => UnitReference.IsUnitAttackable(u, UnitReference.Cell)).ToList();
            inAttackRange.ForEach(u => u.MarkAsReachableEnemy());
        }

        // 选择目标时候执行
        private Unit UnitToAttack;
        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            if (UnitReference.IsUnitAttackable(unit, UnitReference.Cell))
            {
                UnitToAttack = unit;
                StartCoroutine(HumanExecute(cellGrid));
            }
            else if (cellGrid.GetCurrentPlayerUnits().Contains(unit))
            {
                cellGrid.cellGridState = new CellGridStateAbilitySelected(cellGrid, unit, unit.GetComponents<Ability>().ToList());
            }
        }
        
        
    }
}