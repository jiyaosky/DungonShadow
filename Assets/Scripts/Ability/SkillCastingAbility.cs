using System.Collections.Generic;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using TbsFramework.Units.Abilities;
using UnityEngine;
using UnityEngine.UI;


namespace TbsFramework
{
    public class SkillCastingAbility : Ability
    {
        public GameObject AbilitySelect;
        // public GameObject SpellPanel;
        // public GameObject CancelButton;
        public List<SkillAbility> Skills;
        public List<GameObject> SkillPanels;
        
        
        // public int ManaRecoveryRate;


        public override void Initialize()
        {
            // CurrentMana = MaxMana;
            // SkillPanels = new List<GameObject>();
            foreach (var skill in Skills)
            {
                if (skill.IsActive)
                {
                    UnitReference.RegisterAbility(skill);
                }
            }
        }

        public override void Display(CellGrid cellGrid)
        {
            AbilitySelect.SetActive(true);

            foreach (var panel in SkillPanels)
            {
                var skill = panel.GetComponent<SkillDetails>().Skill;
                if (skill.IsActive)
                {
                    panel.SetActive(true);
                }
                else
                {
                    panel.SetActive(false);
                }
            }
        }

        public override void CleanUp(CellGrid cellGrid)
        {
            // foreach (var panel in SkillPanels)
            // {
            //     Destroy(panel);
            // }

            AbilitySelect.SetActive(false);
            // SkillPanels = new List<GameObject>();
        }

        public override void OnTurnStart(CellGrid cellGrid)
        {
            // CurrentMana = Mathf.Min(CurrentMana + ManaRecoveryRate, MaxMana);
            foreach (var spell in Skills)
            {
                spell.OnTurnStart(cellGrid);
            }
        }

        public override void OnTurnEnd(CellGrid cellGrid)
        {
            foreach (var spell in Skills)
            {
                spell.OnTurnEnd(cellGrid);
            }
        }

        public void CancelCasting()
        {
            var cellGrid = FindObjectOfType<CellGrid>();
            cellGrid.cellGridState = new CellGridStateWaitingForInput(cellGrid);
            // CancelButton.SetActive(false);
        }
    }
}