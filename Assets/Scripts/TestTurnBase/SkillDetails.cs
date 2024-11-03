using TbsFramework.Units.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TbsFramework
{
    public class SkillDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string skillName;
        public Text DescriptionText;
        public SkillAbility Skill;

        public void OnPointerEnter(PointerEventData eventData)
        {
            //DescriptionText.text = Skill.name;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //DescriptionText.text = "";
        }
    }
}