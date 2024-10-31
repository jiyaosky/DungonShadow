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
        public int MaxHP = 10;
        // 总的行动点数上限
        [SerializeField]
        public float totalActionPoints;
        // 当前行动点数
        [SerializeField]
        public float currentActionPoints;
        
        // 当前攻击力
        [SerializeField]
        private int currentAttackFactor;

        public int attackLimit = 1;
        

        // 当前暗杀力
        [SerializeField]
        private int currentAssassinationPower;

        private Animator playerAnimator;

        // 攻击能力
        public PlayerAttackAbility PlayerAttackAbility { get; set; }
        // 交互能力
        public InteractionAbility InteractionAbility { get; set; }
        // 移动能力
        public PlayerMoveAbility MovementAbility { get; set; }

        // 选择的技能
        public Ability SelectedAbility;
        // 设置选择的技能
        public void SetSelectedAbility(Ability ability)
        {
            SelectedAbility = ability;
        }
        
        // 
        
        // <summary>
        // 重写OnMouseDown方法，这里大概梳理一下逻辑：
        // 1. 如果当前是玩家回合，且当前玩家是当前单位的玩家，那么可以选择当前单位 选择单位第一时间触发的就是OnMouseDown方法
        // 2. 因为这里重写调用了父类的OnMouseDown方法，所以会触发父类的一个信号事件，事件名为UnitClicked，这个事件会在CellGridStateAbilitySelected中被监听，监听后会直接调用CellGrid中的OnUnitClicked方法
        // 3. 在CellGrid中，会调用cellGridState.OnUnitClicked方法，这份方法是CellGridState类型的，
        // 4. 我们这里调用的是CellGridStateWaitingForInput，这个类中的OnUnitClicked是重写的CellGridState中的方法，（可以把这个方法想象为CellGridState在等待玩家的选择输入）
        // 5. 这时候会new一个新的CellGridStateAbilitySelected对象，这个对象是一个CellGridState类型的对象（这里是为了给CellGridState做一个Ability的选择列表用），同时会调用这个对象的OnStateEnter方法，其实这里已经是nextState了，也就是有状态的
        // （注意，这里的OnStateEnter方法是可以被重写的）如果你想用另一种管理方式，可以重新建一个CellGridStateAbilitySelected类，然后在这个类中实现你的逻辑
        // 6. 在OnStateEnter方法中，会调用当前单位的OnUnitSelected方法，这个方法是在Unit中定义的，这个方法会在Unit被选择时调用
        // 7. 在OnStateEnter方法中，还会调用每个Ability的OnAbilitySelected方法，这个方法是在Ability中定义的，这个方法会做一些Ability被选中时候的操作
        // 8. 在OnStateEnter方法中，还会调用每个Ability的Display方法，这个方法是在Ability中定义的，这个方法会做一些显示上的操作
        // 9. 在OnStateEnter方法中，会判断当前单位是否可以执行任何操作，如果不能执行任何操作，那么会将当前单位的状态设置为UnitStateMarkedAsFinished，这个状态是在UnitStateMarkedAsFinished中定义的，这个状态会在Unit被标记为完成时调用
        // 10. 在判断是否可以执行操作时，用的是Ability自身的CanPerform方法，这个方法是在Ability中定义的，这个方法会判断当前单位是否可以执行操作
        // </summary>
        public override void OnMouseDown()
        {
            base.OnMouseDown();
        }
        
        /// <summary>
        /// Method is called when unit is selected.
        /// 当单位被选择时调用的方法。
        /// </summary>
        public override void OnUnitSelected()
        {
            SelectedAbility = MovementAbility;
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
            currentActionPoints = totalActionPoints;
            ActionPoints = currentActionPoints;
            // 注册一下相关能力
            PlayerAttackAbility = GetComponent<PlayerAttackAbility>();
            currentAttackFactor = PlayerAttackAbility.AbilityDamage;
            currentAssassinationPower = PlayerAttackAbility.AssassinationPower;
            AttackRange = PlayerAttackAbility.AbilityRange;
            InteractionAbility = GetComponent<InteractionAbility>();
            SetNewCurrentForward();
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
        
        public void SetNewCurrentForward()
        {
            var Directon2D = GridUtils.GetDirectionV3ByAngleY(transform.rotation.eulerAngles.y);
            currentForward = new Vector3(Directon2D.x, 0, Directon2D.z);
        }

        public override void ChangeFoward(Vector3 current, Vector3 target)
        {
            SetNewCurrentForward();
            base.ChangeFoward(current, target);
        }


        // 重写父类的DealDamage
        protected override AttackAction DealDamage(Unit unitToAttack, int cost)
        {
            var damage = currentAttackFactor;
            if (IsBehindAnotherUnit(unitToAttack))
            {
                Debug.Log("刺杀");
                damage = currentAttackFactor + currentAssassinationPower;
            }
            return new AttackAction(damage, PlayerAttackAbility.AbilityCost);
        }

        // 重写父类的方法，当攻击动作完成时调用，减少行动点数
        protected override void AttackActionPerformed(float actionCost, bool isSpendAttackLimit)
        {
            if (isSpendAttackLimit)
            {
                attackLimit--;
            }
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
            attackLimit = 1;
            base.OnTurnEnd();
            // 这个属性可以理解为最大的可移动点数
            // MovementPoints = totalActionPoints;
            // 这里可能还会重置一下ActionPoints
            currentActionPoints = totalActionPoints;
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
