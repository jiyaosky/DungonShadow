using UnityEngine;
using System.Collections.Generic;
using TbsFramework.Example1;
using TbsFramework.Units.Abilities;
using System;
using TbsFramework.Cells;
using System.Collections;
using TbsFramework.Grid;
using UnityEngine.UI;

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
        [SerializeField]
        public float currentActionPoints;

        // 基础攻击力
        public int baseAttackFactor;

        // 当前攻击力
        [SerializeField]
        private int currentAttackFactor;

        // 基础暗杀力
        public int baseAssassinationPower;

        // 当前暗杀力
        [SerializeField]
        private int currentAssassinationPower;

        // 基础攻击范围
        public int baseAttackRange;
        // 当前攻击范围
        [SerializeField]
        private int currentAttackRange;

        private Animator playerAnimator;

        // 攻击能力
        public PlayerAttackAbility PlayerAttackAbility { get; set; }
        // 交互能力
        public InteractionAbility InteractionAbility { get; set; }
        // 移动能力
        public PlayerMoveAbility MovementAbility { get; set; }

        // 选择的技能
        public Button buttonAttackAbility; 
        public Button buttonInteractionAbility;
        public Button buttonScoutAbility;
        public Button buttonAbility1;
        public Button buttonAbility2;
        public Button buttonAbility3;
        public Ability SelectedAbility;
        // 设置选择的技能
        public void SetSelectedAbility(Ability ability)
        {
            SelectedAbility = ability;
        }

        public Ability SelectAbility()
        {
            if (SelectedAbility == null)
            {
                SelectedAbility = PlayerAttackAbility;
            }

            return SelectedAbility;
        }



        /// <summary>
        /// Method is called when unit is selected.
        /// 当单位被选择时调用的方法。
        /// </summary>
        public override void OnUnitSelected()
        {

            base.OnUnitSelected();
        }
        /// <summary>
        /// Method is called when unit is deselected. 
        /// 当单位被取消选择时调用的方法。
        /// </summary>
        public override void OnUnitDeselected()
        {
            SelectedAbility = null;
            base.OnUnitDeselected();
        }

        // 初始化方法
        public override void Initialize()
        {
            playerAnimator = GetComponentInChildren<Animator>();

            currentHitPoints = baseHitPoints;
            currentActionPoints = totalActionPoints;
            currentAttackFactor = baseAttackFactor;
            currentAssassinationPower = baseAssassinationPower;
            currentAttackRange = baseAttackRange;
            // MovementPoints = currentActionPoints;
            ActionPoints = currentActionPoints;
            // 注册一下相关能力
            PlayerAttackAbility = GetComponent<PlayerAttackAbility>();
            InteractionAbility = GetComponent<InteractionAbility>();
            
            // 初始化技能按钮
            buttonAttackAbility.onClick.AddListener(() => SetSelectedAbility(PlayerAttackAbility));
            buttonInteractionAbility.onClick.AddListener(() => SetSelectedAbility(InteractionAbility));
            // buttonScoutAbility.onClick.AddListener(() => SetSelectedAbility(xxx));

            
            base.Initialize();
        }

        public override float MovementPoints
        {
            get
            {
                return currentActionPoints;
            }
            protected set
            {
                currentActionPoints = value;
            }
        }


        // 重写父类的DealDamage
        protected override AttackAction DealDamage(Unit unitToAttack)
        {
            return new AttackAction(currentAttackFactor, PlayerAttackAbility.AbilityCost);
        }

        // 重写父类的方法，当攻击动作完成时调用，减少行动点数
        protected override void AttackActionPerformed(float actionCost)
        {
            currentActionPoints -= actionCost;
        }

        // 重写父类是否能击中敌人的方法
        public override bool IsUnitAttackable(Unit other, Cell sourceCell)
        {
            return IsUnitAttackable(other, other.Cell, sourceCell);
        }
        public override bool IsUnitAttackable(Unit other, Cell otherCell, Cell sourceCell)
        {
            return sourceCell.GetDistance(otherCell) <= AttackRange
                && other.PlayerNumber != PlayerNumber
                && currentActionPoints >= 1;
        }

        // 重写父类的方法，当回合结束时调用，重置行动点数，以及其他可能需要重置的属性
        public override void OnTurnEnd()
        {
            base.OnTurnEnd();
            // 这个属性可以理解为最大的可移动点数
            // MovementPoints = totalActionPoints;
            // 这里可能还会重置一下ActionPoints
            currentActionPoints = totalActionPoints;
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

        // 获取当前的攻击Ability
        public Ability GetCurrentAttackAbility()
        {
            return GetComponent<PlayerAttackAbility>();
        }


        // 动画相关
        // 重写父类的移动动画效果，添加了动画效果
        protected override IEnumerator MovementAnimation(IList<Cell> path)
        {
            Animator playerAnimator = GetComponentInChildren<Animator>();
            bool isMoving = true;
            if (playerAnimator != null)
            {
                playerAnimator.Play("Base Layer.RunForward", 0, 0f);
            }
            while (isMoving)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    var currentCell = path[i];
                    Vector3 destination_pos = FindObjectOfType<CellGrid>().Is2D ? new Vector3(currentCell.transform.localPosition.x, currentCell.transform.localPosition.y, transform.localPosition.z) : new Vector3(currentCell.transform.localPosition.x, currentCell.transform.localPosition.y, currentCell.transform.localPosition.z);
                    while (transform.localPosition != destination_pos)
                    {
                        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementAnimationSpeed);
                        // 角色转向！
                        ChangeFoward(transform.position, destination_pos);
                        yield return null;
                    }
                }
                isMoving = false;
            }

            if (playerAnimator != null)
            {
                playerAnimator.StopPlayback();
            }

            OnMoveFinished();
        }

    }
}
