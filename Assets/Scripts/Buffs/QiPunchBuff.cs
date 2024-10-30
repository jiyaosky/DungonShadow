using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class QiPunchBuff : Buff
    {
        public int Range = 2;
        public int APCost = 3;
        public int ColdDown = 2;
        public int Count = 2;
        public int Price = 12;

        private RealPlayer realPlayer;

        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // 假设存在一个方法可以获取目标敌人并击退
            Unit targetEnemy = GetTargetEnemy(unit);
            if (targetEnemy != null)
            {
                // 实现击退逻辑，假设存在一个方法来实现击退
                KnockBack(targetEnemy, 3);
            }
        }

        public override void Undo(Unit unit)
        {
            // 无需撤销操作
        }

        private Unit GetTargetEnemy(Unit unit)
        {
            // 实现获取目标敌人的逻辑
            Unit enemy = null;
            // TODO: 具体实现逻辑
            return enemy;
        }

        private void KnockBack(Unit enemy, int distance)
        {
            // 实现击退逻辑
            // TODO: 具体实现逻辑
        }
    }
}