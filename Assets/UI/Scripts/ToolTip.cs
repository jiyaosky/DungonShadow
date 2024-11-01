using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject dialog_box;

    public GameObject skill;

    [SerializeField]
    public GameObject AP_panel1;
    [SerializeField]
    public GameObject CD_panle2;
    [SerializeField]
    public TextMeshProUGUI AP_info;
    [SerializeField]
    public TextMeshProUGUI CD_info;

    public int AP;
    public int CD;



    void Start()
    {
        //Set UI Position
        dialog_box.transform.position = new Vector3(this.gameObject.transform.position.x + 60, this.gameObject.transform.position.y + 60, this.gameObject.transform.position.z);

        AP_panel1.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y -25, this.gameObject.transform.position.z);
        AP_info.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 25, this.gameObject.transform.position.z);
        CD_panle2.transform.position = new Vector3(this.gameObject.transform.position.x + 20, this.gameObject.transform.position.y + 30, this.gameObject.transform.position.z);
        CD_info.transform.position = new Vector3(this.gameObject.transform.position.x + 20, this.gameObject.transform.position.y + 30, this.gameObject.transform.position.z);


        //Read AP and CD
        //AP = skill.GetComponent<ImmobilizationSkill>().APCost;
        //CD = skill.GetComponent<ImmobilizationSkill>().CoolDown;


    }
    

    //if (dialog_box == null) { Debug.Log("No child fopund"); }
        
    public void OnPointerEnter(PointerEventData eventData)
    {
        dialog_box.SetActive(true);
        //Debug.Log("The cursor entered the selectable UI element.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dialog_box.SetActive(false);
        //Debug.Log("The cursor entered the selectable UI element.");
    }



}
