using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 脚底抹油技能类
    public class SlipAwaySkill : SkillAbility
    {
        public int AddActionPoint = 2;

        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            var realPlayer = UnitReference as RealPlayer;
            realPlayer.currentActionPoints += AddActionPoint;
            realPlayer.attackLimit = 0;
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