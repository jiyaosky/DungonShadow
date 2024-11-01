using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    
    //Brute Force Code
    private GameObject abilityPanel;
    private GameObject relicPanel;

    private GameObject abilitySlot1;
    private GameObject abilitySlot2;
    private GameObject abilitySlot3;

    private Button buyButton1;
    private Button buyButton2;
    private Button buyButton3;

    private int index1;
    private int index2;
    private int index3;
    private int index4;
    private int index5;

    private GameObject skill1;
    private GameObject skill2;
    private GameObject skill3;

    private GameObject relicSlot1;
    private GameObject relicSlot2;

    private GameObject relic1;
    private GameObject relic2;

    [SerializeField]
    public GameObject allSkills;

    private int skillSize;

    [SerializeField]
    public GameObject allRelics;

    private int relicSize;

    // Call SkillManager
    private SkillManager skillManager;

    [SerializeField]
    private Button leaveButton;

    //record shop pool
    List<int> skillPool;



    void Start()
    {
        //Get all skills
        //allSkills = GameObject.FindChild("Canvas/AbilitySet");
        //allSkills = GameObject.Find("Units/Thief/Canvas/AbilitySelect/Panel");

        skillManager = GameObject.Find("Canvas/ActionPanel/SkillManager").GetComponent<SkillManager>();

        //Tracking the pool
        skillPool = new List<int>();
        for (var i = 0; i < allSkills.transform.childCount; i++) {
            skillPool.Add(i);
        }
        

        //initiate all variables
        abilityPanel = this.gameObject.transform.GetChild(0).gameObject;
        relicPanel = this.gameObject.transform.GetChild(1).gameObject;

        abilitySlot1 = abilityPanel.transform.GetChild(0).gameObject;
        abilitySlot2 = abilityPanel.transform.GetChild(1).gameObject;
        abilitySlot3 = abilityPanel.transform.GetChild(2).gameObject;

        relicSlot1 = relicPanel.transform.GetChild(0).gameObject;
        relicSlot2 = relicPanel.transform.GetChild(1).gameObject;

        buyButton1 = abilityPanel.transform.GetChild(3).GetComponent<Button>();
        buyButton2 = abilityPanel.transform.GetChild(4).GetComponent<Button>();
        buyButton3 = abilityPanel.transform.GetChild(5).GetComponent<Button>();

        //Leave button
        //leaveButton = this.gameObject.transform.Find("LeaveButton").GetComponent<Button>();
        //Removeall
        leaveButton.onClick.AddListener(() => { 
            this.gameObject.SetActive(false);
            
            
            if (buyButton1.interactable == true) {
                skill1.SetActive(false);
            }

            if (buyButton2.interactable == true)
            {
                skill2.SetActive(false);
            }

            if (buyButton3.interactable == true)
            {
                skill3.SetActive(false);
            }

        });

        Refresh();

    }

    public void Refresh() {

        //Clear();

        skillSize = allSkills.transform.childCount;
        relicSize = allRelics.transform.childCount;

        //Randomly select three skill
        index1 = Random.Range(0, skillPool.Count);
        index2 = Random.Range(0, skillPool.Count);
        index3 = Random.Range(0, skillPool.Count);


        //get actually shop index
        index1 = skillPool[index1];
        index2 = skillPool[index2];
        index3 = skillPool[index3];

        //Remove Dupolicate
        while (index2 == index1) { index2 = Random.Range(0, skillSize); }
        while (index3 == index1 || index3 == index2) { index3 = Random.Range(0, skillSize); }
        //Debug.Log("Duplication Check" + index1 + index2 + index3);

        //Load Skills
        /*
        skill1 = allSkills.transform.GetChild(index1).gameObject;
        skill2 = allSkills.transform.GetChild(index2).gameObject;
        skill3 = allSkills.transform.GetChild(index3).gameObject;
        

        skill1.SetActive(true);
        skill2.SetActive(true);
        skill3.SetActive(true);

        skill1.transform.position = abilitySlot1.transform.position;
        skill2.transform.position = abilitySlot2.transform.position;
        skill3.transform.position = abilitySlot3.transform.position;
        */

        abilitySlot1.SetActive(false);
        abilitySlot2.SetActive(false);
        abilitySlot3.SetActive(false);

        allSkills.transform.GetChild(index1).gameObject.SetActive(true);
        allSkills.transform.GetChild(index2).gameObject.SetActive(true);
        allSkills.transform.GetChild(index3).gameObject.SetActive(true);

        allSkills.transform.GetChild(index1).gameObject.transform.position = abilitySlot1.transform.position;
        allSkills.transform.GetChild(index2).gameObject.transform.position = abilitySlot2.transform.position;
        allSkills.transform.GetChild(index3).gameObject.transform.position = abilitySlot3.transform.position;


        buyButton1.gameObject.SetActive(true);
        buyButton2.gameObject.SetActive(true);
        buyButton3.gameObject.SetActive(true);

        //Set buy button
        buyButton1.onClick.AddListener(() => {
            skillManager.getSkill(index1);
            //skill1.SetActive(false);
            buyButton1.gameObject.SetActive(false);

            //remove item from the pool
            skillPool.Remove(index1);

        });
        buyButton2.onClick.AddListener(() => {
            skillManager.getSkill(index2);
            //skill2.SetActive(false);
            buyButton2.gameObject.SetActive(false);

            //remove item from the pool
            skillPool.Remove(index2);
        });
        buyButton3.onClick.AddListener(() => {
            skillManager.getSkill(index3);
            //skill3.SetActive(false);
            buyButton3.gameObject.SetActive(false);

            //remove item from the pool
            skillPool.Remove(index3);
        });

        //Load Relics, Relics come with button
        index4 = Random.Range(0, relicSize);
        index5 = Random.Range(0, relicSize);
        while (index5 == index4) { index5 = Random.Range(0, relicSize); }

        relic1 = allRelics.transform.GetChild(index4).gameObject;
        relic2 = allRelics.transform.GetChild(index5).gameObject;

        relicSlot1.SetActive(false);
        relicSlot2.SetActive(false);

        relic1.SetActive(true);
        relic2.SetActive(true);

        relic1.transform.position = relicSlot1.transform.position;
        relic2.transform.position = relicSlot2.transform.position;
    }


    public void Clear() {

        if (skill1 != null) { skill1.SetActive(false); }
        if (skill2 != null) { skill2.SetActive(false); }
        if (skill3 != null) { skill3.SetActive(false); }
        if (relic1 != null) { relic1.SetActive(false); }
        if (relic2 != null) { relic2.SetActive(false); }

    }
}
