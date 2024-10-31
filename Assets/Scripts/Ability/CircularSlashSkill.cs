using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var enemies = cellGrid.GetAIEnemies();
            var unitsInRange = enemies.Where(u => u.Cell.GetDistance(UnitReference.Cell) <= 2);
            foreach (var enemy in unitsInRange)
            {
                UnitReference.AttackHandler(enemy, APCost);
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
                StartCoroutine(HumanExecute(cellGrid));
            }
        }
    }
}
