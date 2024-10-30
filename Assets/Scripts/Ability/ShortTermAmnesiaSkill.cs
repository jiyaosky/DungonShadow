using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 短暂失忆技能类
    public class ShortTermAmnesiaSkill : SkillAbility
    {
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            Unit targetEnemy = GetTargetEnemy(UnitReference);
            if (targetEnemy != null)
            {
                // targetEnemy.SetToPatrolState();
            }

            yield return null;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ability_type", "ShortTermAmnesia");
            return dict;
        }

        public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams,
            bool isNetworkInvoked = true)
        {
            Unit targetEnemy = GetTargetEnemy(UnitReference);
            if (targetEnemy != null)
            {
                // targetEnemy.SetToPatrolState();
            }

            yield return null;
        }

        private Unit GetTargetEnemy(Unit unit)
        {
            Unit enemy = null;
            // TODO: 实现获取目标敌人的逻辑
            return enemy;
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }
    }
}