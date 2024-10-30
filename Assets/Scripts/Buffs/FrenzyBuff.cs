using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class FrenzyBuff : Buff
    {
        public int Range = 0;
        public int APCost = 3;
        public int ColdDown = 3;
        public int Count = 1;
        public int Price = 20;

        private RealPlayer realPlayer;

        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // realPlayer.attackHasCD = false;
        }

        public override void Undo(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // realPlayer.attackHasCD = true;
        }
    }
}