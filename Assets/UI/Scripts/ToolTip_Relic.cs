using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ToolTip_Relic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //Manage gold
    [SerializeField]
    public GameObject goldManager;
    private GoldController goldController;
    

    [SerializeField]
    private Button buyButton;
    
    [SerializeField]
    public int price;

    [SerializeField]
    private GameObject dialog_box;

    // Start is called before the first frame update
    void Start()
    {
        goldController = goldManager.GetComponent<GoldController>();
        price = 0 - price;

        buyButton.onClick.AddListener(() => {
            goldController.UpdateValue(0,price);
        });
    }

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
