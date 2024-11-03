using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ToolTip_Basic: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    [SerializeField]
    private GameObject dialog_box;

    [SerializeField]
    private GameObject messagePanel;
    [SerializeField]
    private TextMeshProUGUI messagePopUp;

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
