using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 气冲拳技能类
    public class QiPunchSkill : SkillAbility
    {
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            Unit targetEnemy = GetTargetEnemy(UnitReference);
            if (targetEnemy != null)
            {
                KnockBack(targetEnemy, 3);
            }

            yield return null;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ability_type", "QiPunch");
            return dict;
        }

        public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams,
            bool isNetworkInvoked = true)
        {
            Unit targetEnemy = GetTargetEnemy(UnitReference);
            if (targetEnemy != null)
            {
                KnockBack(targetEnemy, 3);
            }

            yield return null;
        }

        private Unit GetTargetEnemy(Unit unit)
        {
            Unit enemy = null;
            // TODO: 实现获取目标敌人的逻辑
            return enemy;
        }

        private void KnockBack(Unit enemy, int distance)
        {
            // TODO: 实现击退逻辑
        }
    }
}