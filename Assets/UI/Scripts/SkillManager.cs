using System.Collections;
using System.Collections.Generic;
using TbsFramework;
using UnityEngine;
using UnityEngine.UI;


//Control all the skill button
public class SkillManager : MonoBehaviour
{

    //Count how many skills players has at the moment
    private int skillCount;

    //Read to empty skill slot
    private GameObject skillSlot;

    //Read the Skill List
    [SerializeField]
    public GameObject allSkills;

    [SerializeField]
    public GameObject tempHolder;

    private GameObject changeSkill;

    void Start()
    {

        skillCount = 0;

        //Get all skills
        //allSkills = GameObject.FindChild("Canvas/AbilitySet");
        //allSkills = GameObject.Find("Units/Thief/Canvas/AbilitySelect/Panel");

    }

    public void getSkill(int index) {


        //Changing the next empty skill slot into a skill button
        if (skillCount <= 3)
        {

            skillSlot = this.gameObject.transform.GetChild(skillCount).gameObject;
            changeSkill = allSkills.transform.GetChild(index).gameObject;


            // 设置一下这个技能已经启用
            var skill = changeSkill.GetComponent<SkillDetails>().Skill;
            skill.IsActive = true;
            changeSkill.SetActive(true);
            changeSkill.transform.position = skillSlot.transform.position;

            changeSkill.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                //Debug.Log("Setting Skills");
                skill.Activate();
             });


            changeSkill.GetComponent<SkillDetails>().Skill.Activate();

            skillSlot.SetActive(false);

            //Debug.Log("Changing skills" + this.gameObject.transform.GetChild(skillCount).gameObject.name);

            skillCount++;

        }

        else return;
    }

}
