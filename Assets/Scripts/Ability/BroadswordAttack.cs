using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using TbsFramework.Units.Abilities;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class BroadswordAttack : Ability
    {
        public PlayerAttackAbility AttackAbility;

        public override void Initialize()
        {
            AttackAbility = GetComponent<PlayerAttackAbility>();
        }
        
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            // 这里写击退
            yield return null;
        }
        
    }
}