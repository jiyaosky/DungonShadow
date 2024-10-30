using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class InformationPerceptionBuff : Buff
    {
        public int Range = 0;
        public int APCost = 2;
        public int ColdDown = 3;
        public int Count = 2;
        public int Price = 8;

        private RealPlayer realPlayer;
        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // realPlayer.hasPenetratingVision = true;
            // realPlayer.visionRange++;
        }

        public override void Undo(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // realPlayer.hasPenetratingVision = false;
            // realPlayer.visionRange--;
        }
    }
    
    
    
}