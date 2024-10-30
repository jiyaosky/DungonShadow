using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework
{
    [CreateAssetMenu]
    public class ShortTermAmnesiaBuff : Buff
    {
        public int Range = 2;
        public int APCost = 3;
        public int ColdDown = 3;
        public int Count = 2;
        public int Price = 14;

        private RealPlayer realPlayer;

        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // 假设存在一个方法可以获取目标敌人并使其回到巡逻状态
            Unit targetEnemy = GetTargetEnemy(unit);
            if (targetEnemy != null)
            {
                // 实现使目标敌人回到巡逻状态的逻辑
                // targetEnemy.SetToPatrolState();
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