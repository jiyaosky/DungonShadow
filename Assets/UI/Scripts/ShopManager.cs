using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    
    //Brute Force Code
    private GameObject abilityPanel;
    private GameObject RelicPanel;

    private GameObject abilitySlot1;
    private GameObject abilitySlot2;
    private GameObject abilitySlot3;

    private Button buyButton1;
    private Button buyButton2;
    private Button buyButton3;

    private int index1;
    private int index2;
    private int index3;

    private GameObject skill1;
    private GameObject skill2;
    private GameObject skill3;

    private GameObject allSkills;
    private int skillSize;

    // Call SkillManager
    //private GameObject skillManager;
    private SkillManager skillManager;

    private Button leaveButton;

    void Start()
    {
        //Get all skills
        allSkills = GameObject.Find("Canvas/AbilitySet");
        skillSize = allSkills.transform.childCount;

        skillManager = GameObject.Find("Canvas/ActionPanel/SkillManager").GetComponent<SkillManager>();

        //initiate all variables
        abilityPanel = this.gameObject.transform.GetChild(0).gameObject;
        RelicPanel = this.gameObject.transform.GetChild(1).gameObject;

        abilitySlot1 = abilityPanel.transform.GetChild(0).gameObject;
        abilitySlot2 = abilityPanel.transform.GetChild(1).gameObject;
        abilitySlot3 = abilityPanel.transform.GetChild(2).gameObject;

        buyButton1 = abilityPanel.transform.GetChild(3).GetComponent<Button>();
        buyButton2 = abilityPanel.transform.GetChild(4).GetComponent<Button>();
        buyButton3 = abilityPanel.transform.GetChild(5).GetComponent<Button>();

        //Leave button
        leaveButton = this.gameObject.transform.Find("LeaveButton").GetComponent<Button>();
        leaveButton.onClick.AddListener(() => { 
            this.gameObject.SetActive(false);
            skill1.SetActive(false);
            skill2.SetActive(false);
            skill3.SetActive(false);
        });

        Refresh();
    }

    public void Refresh() {

        //Randomly select three skill
        index1 = Random.Range(0, skillSize);
        index2 = Random.Range(0, skillSize);
        index3 = Random.Range(0, skillSize);

        //Remove Dupolicate
        while (index2 == index1) { index2 = Random.Range(0, skillSize); }
        while (index3 == index1 || index3 == index2) { index3 = Random.Range(0, skillSize); }
        Debug.Log("Duplication Check" + index1 + index2 + index3);
        
        //Load Skills
        skill1 = allSkills.transform.GetChild(index1).gameObject;
        skill2 = allSkills.transform.GetChild(index2).gameObject;
        skill3 = allSkills.transform.GetChild(index3).gameObject;

        skill1.SetActive(true);
        skill2.SetActive(true);
        skill3.SetActive(true);

        abilitySlot1.SetActive(false);
        abilitySlot2.SetActive(false);
        abilitySlot3.SetActive(false);

        skill1.transform.position = new Vector3(abilitySlot1.transform.position.x, abilitySlot1.transform.position.y, 0); 
        skill2.transform.position = new Vector3(abilitySlot2.transform.position.x, abilitySlot2.transform.position.y, 0);
        skill3.transform.position = new Vector3(abilitySlot3.transform.position.x, abilitySlot3.transform.position.y, 0);



        //Set buy button
        buyButton1.onClick.AddListener(() => {
            skillManager.getSkill(index1);
            skill1.SetActive(false);
        });
        buyButton2.onClick.AddListener(() => {
            skillManager.getSkill(index2);
            skill2.SetActive(false);
        });
        buyButton3.onClick.AddListener(() => {
            skillManager.getSkill(index3);
            skill3.SetActive(false);
        });
    }   

}
