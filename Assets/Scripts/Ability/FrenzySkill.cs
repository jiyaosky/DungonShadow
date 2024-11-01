using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 狂暴技能类
    public class FrenzySkill : SkillAbility
    {
        private int attackLimit = 1;

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            var realPlayer = UnitReference as RealPlayer;
            realPlayer.attackLimit = 99;
            IsActive = false;
            yield return null;
        }

        public override void Activate()
        {
            var cellGrid = FindObjectOfType<CellGrid>();
            if (CanPerform(cellGrid))
            {
                StartCoroutine(HumanExecute(cellGrid));
            }
        }
        
        // 选中的时候啥也不做就行了
        public override void OnAbilitySelected(CellGrid cellGrid)
        {
            base.OnAbilitySelected(cellGrid);
        }

        public override void OnTurnEnd(CellGrid cellGrid)
        {
            IsActive = true;
            var realPlayer = UnitReference as RealPlayer;
            realPlayer.attackLimit = attackLimit;
        }

        // 能否执行脚底抹油
        public override bool CanPerform(CellGrid cellGrid)
        {
            if (IsActive)
            {
                return true;
            }
            return false;
        }
        
        // 清理一下
        public override void CleanUp(CellGrid cellGrid)
        {

        }
    }
}