using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private Button refreshButton;

    [SerializeField]
    private GameObject messagePanel;
    [SerializeField]
    private TextMeshProUGUI messagePopUp;

    public GameObject SkillPanel;

    //record shop pool
    List<int> skillPool;

    //Manage gold
    [SerializeField]
    public GameObject goldManager;
    private GoldController goldController;

    void Start()
    {
        //Get all skills
        //allSkills = GameObject.FindChild("Canvas/AbilitySet");
        //allSkills = GameObject.Find("Units/Thief/Canvas/AbilitySelect/Panel");

        skillManager = SkillPanel.GetComponent<SkillManager>();
        goldController = goldManager.GetComponent<GoldController>();


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

        //refreshButton.gameObject.SetActive(true);


        //Leave button
        //leaveButton = this.gameObject.transform.Find("LeaveButton").GetComponent<Button>();
        //Removeall
        leaveButton.onClick.AddListener(() => { 
            this.gameObject.SetActive(false);
            //refreshButton.gameObject.SetActive(false);

            Clear();

            if (skill1 != null && buyButton1.interactable == true) {
                skill1.SetActive(false);
            }

            if (skill2 != null && buyButton2.interactable == true)
            {
                skill2.SetActive(false);
            }

            if (skill3 != null && buyButton3.interactable == true)
            {
                skill3.SetActive(false);
            }

        });

        Refresh();
        Refresh();

    }

    public void Refresh() {

        Clear();

        if (skillPool != null) {
        
        skillSize = skillPool.Count;

        relicSize = allRelics.transform.childCount;

        //Randomly select three skill
        index1 = Random.Range(0, skillPool.Count);
        index2 = Random.Range(0, skillPool.Count);
        index3 = Random.Range(0, skillPool.Count);

        //Remove Dupolicate
        while (index2 == index1) { index2 = Random.Range(0, skillSize); }
        while (index3 == index1 || index3 == index2) { index3 = Random.Range(0, skillSize); }
        //Debug.Log("Duplication Check" + index1 + index2 + index3);

        Debug.Log("Select Item " + index1 + index2 + index3);

        //get actually shop index
        index1 = skillPool[index1];
        index2 = skillPool[index2];
        index3 = skillPool[index3];


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
        
        //set price text
        buyButton1.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + allSkills.transform.GetChild(index1).GetComponent<ToolTip>().getPrice();
        buyButton2.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + allSkills.transform.GetChild(index2).GetComponent<ToolTip>().getPrice();
        buyButton3.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + allSkills.transform.GetChild(index3).GetComponent<ToolTip>().getPrice();



        Debug.Log("PlayerGold" + goldController.GetValue(0));

        //Set buy button
        buyButton1.onClick.AddListener(() => {
  
            if (goldController.GetValue(0) < allSkills.transform.GetChild(index1).GetComponent<ToolTip>().getPrice()) {
                messagePanel.SetActive(true);
                messagePopUp.text = "Not Enough Gold";
                messagePanel.transform.GetComponent<MessageFade>().RunFade();
            }

            else {
                skillManager.getSkill(index1);
                //skill1.SetActive(false);
                buyButton1.gameObject.SetActive(false);

                //remove item from the pool
                skillPool.Remove(index1);

                //update gold
                var int1 = 0 - allSkills.transform.GetChild(index1).GetComponent<ToolTip>().getPrice();
                goldController.UpdateValue(0,int1);
                //Debug.Log("Remove item " + index1);
            }

        });
        buyButton2.onClick.AddListener(() => {

            if (goldController.GetValue(0) < allSkills.transform.GetChild(index2).GetComponent<ToolTip>().getPrice()) {
                messagePanel.SetActive(true);
                messagePanel.transform.GetComponent<MessageFade>().RunFade();
                messagePopUp.text = "Not Enough Gold";
            }

            else{

                skillManager.getSkill(index2);
                //skill2.SetActive(false);
                buyButton2.gameObject.SetActive(false);

                //remove item from the pool
                skillPool.Remove(index2);

                //update gold
                var int2 = 0 - allSkills.transform.GetChild(index2).GetComponent<ToolTip>().getPrice();
                goldController.UpdateValue(0,int2);

                //Debug.Log("Remove item " + index2);
            }

        });
        buyButton3.onClick.AddListener(() => {


            if (goldController.GetValue(0) < allSkills.transform.GetChild(index3).GetComponent<ToolTip>().getPrice()) {
                messagePanel.SetActive(true);
                messagePopUp.text = "Not Enough Gold";
                messagePanel.transform.GetComponent<MessageFade>().RunFade();
            }

            else {

                skillManager.getSkill(index3);

                //skill3.SetActive(false);
                buyButton3.gameObject.SetActive(false);

                //remove item from the pool
                skillPool.Remove(index3);

                //update gold
                var int3 = 0 - allSkills.transform.GetChild(index3).GetComponent<ToolTip>().getPrice();
                goldController.UpdateValue(0,int3);
                //Debug.Log("Remove item " + index3);
            }
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
    }


    public void Clear() {

        if (index1 != null && skillPool != null && skillPool.Contains(index1) == true)
        {
            allSkills.transform.GetChild(index1).gameObject.SetActive(false);
        }

        if (index2 != null && skillPool != null && skillPool.Contains(index2) == true)
        {
            allSkills.transform.GetChild(index2).gameObject.SetActive(false);
        }

        if (index3 != null && skillPool != null && skillPool.Contains(index3) == true)
        {
            allSkills.transform.GetChild(index3).gameObject.SetActive(false);
        }


        if (buyButton1 != null) {
            buyButton1.onClick.RemoveAllListeners();
        }

        if (buyButton2 != null) {
            buyButton2.onClick.RemoveAllListeners();
        }

        if (buyButton3 != null) {
            buyButton3.onClick.RemoveAllListeners();
        }

        if (relic1 != null) {
            relic1.SetActive(false);
        }
        if (relic2 != null) {
            relic2.SetActive(false);
        }


    }
}
