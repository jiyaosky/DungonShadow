using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
    public class InteractiveAbility : Ability
    {
        bool isInteracted = false;
        public int Range = 1;
        private void Start()
        {
            isInteracted = false;
        }

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            var myUnits = cellGrid.GetCurrentPlayerUnits();
            var unitsInRange = myUnits.Where(u => u.Cell.GetDistance(UnitReference.Cell) <= Range);

            foreach (var unit in unitsInRange)
            {
                // TODO:当玩家进入这个范围的时候可以做些什么（例如提示 or UI显示）
                Debug.Log("Unit in range");
                // 先设置为可交互状态吧
                isInteracted = true;
            }

            yield return null;
        }


        public virtual void InterPlay()
        {
            if (isInteracted)
            {
                // TODO:当玩家点击这个单元格的时候与其交互吧
                Debug.Log("Interacted");
            }
        }

        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            InterPlay();
        }
    }
}