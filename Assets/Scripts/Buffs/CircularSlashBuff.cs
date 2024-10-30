using System.Collections;
using System.Collections.Generic;
using TbsFramework.Units;
using UnityEngine;


namespace TbsFramework
{
    [CreateAssetMenu]
    public class CircularSlashBuff : Buff
    {
        public int Range = 1;
        public int APCost = 4;
        public int ColdDown = 0;
        public int Count = 1;
        public int Price = 12;

        private RealPlayer realPlayer;

        public override void Apply(Unit unit)
        {
            realPlayer = unit as RealPlayer;
            // 这里假设存在一个获取周围一格敌人并造成伤害的方法
            List<Unit> enemiesAround = GetEnemiesAround(unit);
            foreach (var enemy in enemiesAround)
            {
                // enemy.TakeDamage(unit.attackPower);
            }
        }

        public override void Undo(Unit unit)
        {
            // 无需撤销操作
        }

        private List<Unit> GetEnemiesAround(Unit unit)
        {
            // 实现获取周围一格敌人的逻辑
            List<Unit> enemies = new List<Unit>();
            // TODO: 具体实现逻辑
            return enemies;
        }
    }
}