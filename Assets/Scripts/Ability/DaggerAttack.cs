using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units.Abilities;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class DaggerAttack : Ability
    {
        
        public PlayerAttackAbility AttackAbility;
        
        public override void Initialize()
        {
            AttackAbility = GetComponent<PlayerAttackAbility>();
        }


    }
}