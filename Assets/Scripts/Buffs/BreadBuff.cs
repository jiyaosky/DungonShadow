using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class BreadBuff : Buff
    {
        // private int originMaxHP;
        public int addMaxHP = 1;
        public override void Apply(Unit unit)
        {
            // originMaxHP = unit.HitPoints;
            unit.HitPoints += addMaxHP;
        }

        public override void Undo(Unit unit)
        {
            unit.HitPoints -= addMaxHP;
        }
    }
}
