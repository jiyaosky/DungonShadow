using UnityEngine;
using System.Collections.Generic;
using TbsFramework.Example1;

namespace TbsFramework.Units
{
    public class RealPlayer : Unit
    {
        // 基础血量
        [SerializeField]
        public int baseHitPoints;
        // 当前血量
        private int currentHitPoints;
        // 总的行动点数上限
        [SerializeField]
        public float totalActionPoints;
        // 当前行动点数
        private float currentActionPoints;
        // 基础攻击力
        public int baseAttackFactor;
        // 当前攻击力
        private int currentAttackFactor;
        // 基础暗杀力
        public int baseAssassinationPower;
        // 当前暗杀力
        private int currentAssassinationPower;
        // 基础攻击范围
        public int baseAttackRange;
        // 当前攻击范围
        private int currentAttackRange;

        public RealPlayer()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            currentHitPoints = baseHitPoints;
            currentActionPoints = totalActionPoints;
            currentAttackFactor = baseAttackFactor;
            currentAssassinationPower = baseAssassinationPower;
            currentAttackRange = baseAttackRange;
        }

        // 可以被外部调用以减少血量的方法
        public void TakeDamage(int damage)
        {
            currentHitPoints -= damage;
            if (currentHitPoints <= 0)
            {
                OnDestroyed();
            }
        }

        // 减少行动点数的方法
        public void ConsumeActionPoints(float points)
        {
            currentActionPoints -= points;
        }

        // 更新攻击力的方法，可以在受到 buff 影响时调用
        public void UpdateAttackFactor(int newFactor)
        {
            currentAttackFactor = newFactor;
        }

        // 更新暗杀力的方法，可以在受到 buff 影响时调用
        public void UpdateAssassinationPower(int newPower)
        {
            currentAssassinationPower = newPower;
        }

        // 更新攻击范围的方法，可以在受到 buff 影响时调用
        public void UpdateAttackRange(int newRange)
        {
            currentAttackRange = newRange;
        }

        // 获取当前攻击力
        public int GetCurrentAttackFactor()
        {
            return currentAttackFactor;
        }

        // 获取当前暗杀力
        public int GetCurrentAssassinationPower()
        {
            return currentAssassinationPower;
        }

        // 获取当前攻击范围
        public int GetCurrentAttackRange()
        {
            return currentAttackRange;
        }
    }
}