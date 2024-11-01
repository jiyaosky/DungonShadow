using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        
        skillSlot = this.gameObject.transform.GetChild(skillCount).gameObject;
        changeSkill = allSkills.transform.GetChild(index).gameObject;
        changeSkill.SetActive(true);
        changeSkill.transform.position = skillSlot.transform.position;
        skillSlot.SetActive(false);

        //Debug.Log("Changing skills" + this.gameObject.transform.GetChild(skillCount).gameObject.name);

        skillCount++;

    }


}
