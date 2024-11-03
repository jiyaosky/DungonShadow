using System.Collections;
using System.Collections.Generic;
using TbsFramework.Example4;
using TbsFramework.Units;
using UnityEngine;
using UnityEngine.UI;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class SupplyPackBuff : Buff
    {
        private int originMaxAP;
        public int addMaxAp = 1;
        public int Price = 18;
        public override void Apply(Unit unit)
        {
            originMaxAP = (int)(unit as RealPlayer).totalActionPoints;
            (unit as RealPlayer).totalActionPoints += addMaxAp;
        }

        public override void Undo(Unit unit)
        {
            (unit as RealPlayer).totalActionPoints -= originMaxAP;
        }
    }
}