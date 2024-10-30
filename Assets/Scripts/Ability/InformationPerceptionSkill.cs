using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 信息感知技能类
    public class InformationPerceptionSkill : SkillAbility
    {
        public int AddSightRange = 1;
        
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            var fog = FindObjectOfType<Fog>();

            fog.Radius += AddSightRange;
            fog.through = true;
            
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

        public override void OnTurnEnd(CellGrid cellGrid)
        {
            var fog =  FindObjectOfType<Fog>();
            fog.Radius = 5;
            fog.through = false;
        }
    }
}