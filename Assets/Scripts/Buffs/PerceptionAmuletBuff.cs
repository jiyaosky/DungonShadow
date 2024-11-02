using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class PerceptionAmulet : Buff
    {
        public int Price = 12;
        private int originSightRange;
        public int addSightRange = 1;
        public override void Apply(Unit unit)
        {
            var fog = FindObjectOfType<Fog>();

            fog.Radius += addSightRange;
            // fog.through = true;
        }

        public override void Undo(Unit unit)
        {
            var fog = FindObjectOfType<Fog>();

            fog.Radius -= addSightRange;
        }
    }
}
