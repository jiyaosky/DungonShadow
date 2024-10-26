using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class InteractiveAbility : PlayerAttackAbility
    {
        public int Range = 1;
        private void Start()
        {
        }

        private void Update()
        {

        }

        // public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        // {
        //     var myUnits = cellGrid.GetCurrentPlayerUnits();
        //     var unitsInRange = myUnits.Where(u => u.Cell.GetDistance(UnitReference.Cell) <= Range);

        //     foreach (var unit in unitsInRange)
        //     {
        //         if (unit.Equals(UnitReference) && !ApplyToSelf)
        //         {
        //             continue;
        //         }
        //         unit.AddBuff(Buff);
        //     }

        //     yield return null;
        // }


        public virtual void InterPlay()
        {

        }

    }
}