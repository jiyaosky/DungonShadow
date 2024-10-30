using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 脚底抹油技能类
    public class SlipAwaySkill : SkillAbility
    {
        public int AddActionPoint = 2;

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            // UnitReference.currentActionPoints += AddActionPoint;
            // UnitReference.attackLimit = 0;
            yield return null;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ability_type", "SlipAway");
            dict.Add("add_action_point", AddActionPoint.ToString());
            return dict;
        }

        public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams,
            bool isNetworkInvoked = true)
        {
            int addActionPoint = int.Parse(actionParams["add_action_point"]);
            // UnitReference.currentActionPoints += addActionPoint;
            // UnitReference.attackLimit = 0;
            yield return null;
        }
    }
}