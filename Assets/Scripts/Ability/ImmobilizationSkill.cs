using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using UnityEngine;

namespace TbsFramework.Units.Abilities
{
// 定身术技能类
    public class ImmobilizationSkill : SkillAbility
    {
        public ImmobilizationSkillBuff immobilizationBuff;
        
        private int originAttackRange;
        // 可以添加特定于 Skill 的属性和方法
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            // UnitReference.AttackHandler(UnitToAttack, APCost, false);
            UnitToAttack.AddBuff(immobilizationBuff);
            UnitToAttack.SetAnimation("Base Layer.StunnedLoop", Time.deltaTime * UnitReference.MovementAnimationSpeed);
            yield return null;
        }
        
        public override bool CanPerform(CellGrid cellGrid)
        {
            if (IsActive)
            {
                return true;
            }
            return false;
        }

        public override void Activate()
        {
            var cellGrid = FindObjectOfType<CellGrid>();
            if (CanPerform(cellGrid))
            {
                // StartCoroutine(HumanExecute(cellGrid));
                cellGrid.cellGridState = new CellGridStateAbilitySelected(cellGrid, UnitReference, new List<Ability>() { this });
            }

            // UnitReference.AttackRange = originAttackRange;
        }

        public override void OnAbilitySelected(CellGrid cellGrid)
        {
            originAttackRange = (UnitReference as RealPlayer).AttackRange;
            (UnitReference as RealPlayer).AttackRange = Range;
        }

        public override void OnAbilityDeselected(CellGrid cellGrid)
        {
            (UnitReference as RealPlayer).AttackRange = originAttackRange;
        }

        List<Unit> inAttackRange;
        public override void Display(CellGrid cellGrid)
        {
            var enemyUnits = cellGrid.GetEnemyUnits(cellGrid.CurrentPlayer);
            inAttackRange = enemyUnits.Where(u => UnitReference.IsUnitAttackable(u, UnitReference.Cell)).ToList();
            inAttackRange.ForEach(u => u.MarkAsReachableEnemy());
        }

        // 选择目标时候执行
        private Unit UnitToAttack;
        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            if (UnitReference.IsUnitAttackable(unit, UnitReference.Cell))
            {
                UnitToAttack = unit;
                StartCoroutine(HumanExecute(cellGrid));
            }
            else if (cellGrid.GetCurrentPlayerUnits().Contains(unit))
            {
                cellGrid.cellGridState = new CellGridStateAbilitySelected(cellGrid, unit, unit.GetComponents<Ability>().ToList());
            }
        }
    }
}