using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject dialog_box;

    void Start()
    {
        dialog_box = this.gameObject.transform.GetChild(0).gameObject;
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
        Debug.Log("The cursor entered the selectable UI element.");
    }



}
