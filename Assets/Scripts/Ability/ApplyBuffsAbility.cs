using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using TbsFramework.Units;
using TbsFramework.Units.Abilities;
using UnityEngine.UI;

namespace TbsFramework
{
    public class ApplyBuffsAbility : Ability
    {
        public List<Buff> BuffList;

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            foreach (var buff in BuffList)
            {
                UnitReference.AddBuff(buff);
                // 可以设置动画效果
            }
            yield return null;
        }

        public override void Display(CellGrid cellGrid)
        {
        }

        public override void CleanUp(CellGrid cellGrid)
        {
        }

        public void Activate()
        {
            StartCoroutine(Act(FindObjectOfType<CellGrid>(), false));
        }

        // UI触发器
        public void AddToBuffList(Buff buff)
        {
            BuffList.Add(buff);
        }
    }
}