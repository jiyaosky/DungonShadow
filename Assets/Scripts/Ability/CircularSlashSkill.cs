using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 环形斩技能类
    public class CircularSlashSkill : SkillAbility
    {
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            List<Unit> enemiesAround = GetEnemiesAround(UnitReference);
            foreach (var enemy in enemiesAround)
            {
                UnitReference.AttackHandler(enemy, APCost);
            }

            yield return null;
        }
        
        private List<Unit> GetEnemiesAround(Unit unit)
        {
            var cellGrid = FindObjectOfType<CellGrid>();
            var allEnemy = cellGrid.AIGetEnemyUnits();
            List<Unit> enemies = new List<Unit>();
            // TODO: 实现获取周围一格敌人的逻辑
            List<Cell> neighboursCells = unit.Cell.GetNeighbours(cellGrid.Cells);

            foreach (var e in allEnemy)
            {
                foreach (var cell in neighboursCells)
                {
                    if (cell.Equals(e.Cell))
                    { 
                        enemies.Add(e);
                    }
                }
            }
            
            return enemies;
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
                StartCoroutine(HumanExecute(cellGrid));
            }
        }
    }
}
