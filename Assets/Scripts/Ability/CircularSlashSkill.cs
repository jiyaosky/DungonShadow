using System.Collections;
using System.Collections.Generic;
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
                // enemy.TakeDamage(UnitReference.attackPower);
            }

            yield return null;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ability_type", "CircularSlash");
            return dict;
        }

        public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams,
            bool isNetworkInvoked = true)
        {
            List<Unit> enemiesAround = GetEnemiesAround(UnitReference);
            foreach (var enemy in enemiesAround)
            {
                // enemy.TakeDamage(UnitReference.attackPower);
            }

            yield return null;
        }

        private List<Unit> GetEnemiesAround(Unit unit)
        {
            List<Unit> enemies = new List<Unit>();
            // TODO: 实现获取周围一格敌人的逻辑
            return enemies;
        }
    }
}
