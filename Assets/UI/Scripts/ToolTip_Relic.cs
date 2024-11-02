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
    private Button fakeBuyButton;

    [SerializeField]
    public int price;

    [SerializeField]
    private GameObject dialog_box;

    
    [SerializeField]
    private GameObject messagePanel;
    [SerializeField]
    private TextMeshProUGUI messagePopUp;

    public int gold;


    // Start is called before the first frame update
    void Start()
    {
        goldController = goldManager.GetComponent<GoldController>();
        gold = goldController.GetValue(0);
        Refresh();   
    }

    void Update()
    {
        if (gold != goldController.GetValue(0)) {
            gold = goldController.GetValue(0);
            Refresh();
        }
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

    public void Refresh(){
        

        fakeBuyButton.onClick.AddListener(() => {
              messagePanel.SetActive(true);
              messagePopUp.text = "Not Enough Gold";  
        });

        buyButton.onClick.AddListener(() => {
            goldController.UpdateValue(0, (0 - price));   
        });
        
        //Debug.Log("Gold" + goldController.GetValue(0) + "Price" + price);

        if (goldController.GetValue(0) < price) {

                buyButton.gameObject.SetActive(false);
                fakeBuyButton.gameObject.SetActive(true);
                //messagePanel.SetActive(true);
                //messagePopUp.text = "Not Enough Gold";

        }

        else {
                buyButton.gameObject.SetActive(true);
                fakeBuyButton.gameObject.SetActive(false);
        }
    }

}
