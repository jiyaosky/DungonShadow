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
            var enemies = cellGrid.GetAIEnemies();
            var unitsInRange = enemies.Where(u => u.Cell.GetDistance(UnitReference.Cell) <= Range);
            foreach (var enemy in unitsInRange)
            {
                UnitReference.ChangeFoward(UnitReference.transform.position,enemy.transform.position);
                UnitReference.AttackHandler(enemy, APCost, false);
                
                // 获取气功拳推到的目标点
                Cell pushTarget = enemy.Cell;
                IList<Cell> path = new List<Cell>();
                path.Add(pushTarget);
                Vector2 unitDirection = new Vector3(UnitReference.currentForward.x, UnitReference.currentForward.z);
                for (int i = 1; i <= pushRange; i++)
                {
                    
                    Vector2 targetPosition = UnitReference.Cell.OffsetCoord + unitDirection * i;
                    Cell targetCell = cellGrid.GetCell(targetPosition);
                    pushTarget = targetCell;
                    if (pushTarget == null)
                    {
                        
                    }
                    if (targetCell.IsTaken && !targetCell.Equals(pushTarget))
                    {
                        break;
                    }
                }
                
                
                yield return enemy.Move(pushTarget, path);
            }

            yield return null;
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

        List<Unit> inAttackRange;
        public override void Display(CellGrid cellGrid)
        {
            // originAttackRange = UnitReference.AttackRange;
            UnitReference.AttackRange = Range;
            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            inAttackRange = enemyUnits.Where(u => UnitReference.IsUnitAttackable(u, UnitReference.Cell)).ToList();
            inAttackRange.ForEach(u => u.MarkAsReachableEnemy());
        }

        // 选择目标时候执行
        private Unit UnitToAttack;
        // private int UnitToAttackID;
        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            // if (Player.SelectedAbility is not PlayerAttackAbility) return;
            // 检测当前Unit如果是可交互的则返回空
            if (unit.GetComponent<InteractiveAbility>() != null)
            {
                // Debug.Log("Unit is interactive");
                return;
            }
            if (UnitReference.IsUnitAttackable(unit, UnitReference.Cell))
            {
                UnitToAttack = unit;
                // UnitToAttackID = UnitToAttack.UnitID;
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
        
        
    }
}