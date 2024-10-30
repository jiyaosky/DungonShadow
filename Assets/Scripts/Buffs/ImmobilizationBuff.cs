using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class ImmobilizationBuff : Buff
    {
        public int Range = 2;
        public int APCost = 3;
        public int ColdDown = 2;
        public int Count = 2;
        public int Price = 15;

        private RealPlayer realPlayer;
        private Unit targetEnemy;

        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            targetEnemy = GetTargetEnemy(unit);
            if (targetEnemy != null)
            {
                // 记录目标敌人当前的移动能力状态
                // targetEnemy.canMoveNextTurn = targetEnemy.CanMoveNextTurn;
                // targetEnemy.CanMoveNextTurn = false;
            }
        }

        public override void Undo(Unit unit)
        {
            if (targetEnemy != null)
            {
                // targetEnemy.CanMoveNextTurn = targetEnemy.canMoveNextTurn;
            }
        }

        private Unit GetTargetEnemy(Unit unit)
        {
            // 实现获取目标敌人的逻辑
            Unit enemy = null;
            // TODO: 具体实现逻辑
            return enemy;
        }
    }
}