using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 狂暴技能类
    public class FrenzySkill : SkillAbility
    {
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            // UnitReference.attackHasCD = false;
            yield return null;
        }

        public override IDictionary<string, string> Encapsulate()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ability_type", "Frenzy");
            return dict;
        }

        public override IEnumerator Apply(CellGrid cellGrid, IDictionary<string, string> actionParams,
            bool isNetworkInvoked = true)
        {
            // UnitReference.attackHasCD = false;
            yield return null;
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }
    }
}