using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;


namespace TbsFramework
{
    [CreateAssetMenu]
    public class SlipAwayBuff : Buff
    {
        public int AddActionPoint = 2;

        public int Range = 0;
        public int APCost = 0;
        public int ColdDown = 2;
        public int Count = 2;
        public int Price = 8;
        
        private RealPlayer realPlayer;
        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            realPlayer.currentActionPoints += AddActionPoint;
            realPlayer.attackLimit = 0;
        }

        public override void Undo(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            realPlayer.currentActionPoints -= AddActionPoint;
        }
    }

}
