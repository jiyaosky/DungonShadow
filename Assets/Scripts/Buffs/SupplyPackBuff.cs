using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class SupplyPackBuff : Buff
    {
        private int originMaxAP;
        public int addMaxAp = 1;
        public override void Apply(Unit unit)
        {
            originMaxAP = (int)unit.TotalActionPoints;
            unit.TotalActionPoints += addMaxAp;
        }

        public override void Undo(Unit unit)
        {
            unit.TotalActionPoints = originMaxAP;
        }
    }
}