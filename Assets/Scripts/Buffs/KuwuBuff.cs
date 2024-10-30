using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class KuwuBuff : Buff
    {
        public int Range = 4;
        public int APCost = 2;
        public int ColdDown = 3;
        public int Count = 3;
        public int Price = 12;

        private RealPlayer realPlayer;

        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // 假设存在一个方法可以获取目标敌人并造成暗杀力数值的伤害
            Unit targetEnemy = GetTargetEnemy(unit);
            if (targetEnemy != null)
            {
                // targetEnemy.TakeDamage(unit.assassinationPower);
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
    }
}