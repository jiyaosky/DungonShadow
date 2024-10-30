using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Grid;
using TbsFramework.Units.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace TbsFramework.Units.Abilities
{
    public class InteractionAbility : Ability
    {
        // 交互按钮
        public Button ActivationButton;

        // 交互单位
        private Unit InteractionUnit;

        // 交互能力
        private InteractiveAbility haveInteractiveAbility;


        private RealPlayer Player;

        public void Start()
        {
            Player = GetComponent<RealPlayer>();
            InteractionUnit = Player;
        }


        // TODO: 交互的时候要做的事，比如打开宝箱
        public override IEnumerator Act(CellGrid cellGrid, bool isNetworkInvoked = false)
        {
            Player = GetComponent<RealPlayer>();

            if (CanPerform(cellGrid))
            {
                // TODO：也许交互会有消耗，那么消耗的应该是玩家的currentActionPoints
                var interactiveCost = 2;

                // ready to interact
                Debug.Log("Ready to interact");
                haveInteractiveAbility.InterPlay(haveInteractiveAbility.AbilityName);
                Player.currentActionPoints -= interactiveCost;
            }

            yield return null;
        }


        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            // Debug.Log("OnUnitClicked InteractionAbility");
            InteractionUnit = unit;
            if (unit.GetComponent<InteractiveAbility>() == null)
            {
                return;
            }
            if (unit.GetComponent<InteractiveAbility>() != null)
            {
                haveInteractiveAbility = unit.GetComponent<InteractiveAbility>();
                if (CanPerform(cellGrid))
                {
                    // ActivationButton.gameObject.SetActive(true);
                    // ActivationButton.onClick.RemoveAllListeners();
                    // ActivationButton.onClick.AddListener(() => OnInteraction(cellGrid, InteractionUnit));
                    OnInteraction(cellGrid, InteractionUnit);
                }
            }
            // StartCoroutine(HumanExecute(cellGrid));
        }


        // 判断是否能交互
        public override bool CanPerform(CellGrid cellGrid)
        {
            bool checkPlayerInRange = true;
            if (haveInteractiveAbility != null)
            {
                checkPlayerInRange = haveInteractiveAbility.isPlayerInRange(cellGrid);
            }

            return InteractionUnit.PlayerNumber != Player.PlayerNumber && Player.currentActionPoints > 0 && checkPlayerInRange;
        }

        public void OnInteraction(CellGrid cellGrid, Unit unit)
        {
            StartCoroutine(Act(cellGrid));
        }

    }
}