using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MessageMove : MonoBehaviour
{

    public TextMeshProUGUI messagePanelText;
    public string message;
    public Vector3 originalPosition;

    void Awake()
    {
        //originalPosition = messagePanelText.gameObject.transform.position;
        //Debug.Log("1 time" + originalPosition.x + "," + originalPosition.y + "," + originalPosition.z);
        originalPosition = new Vector3 (587, 316, 0);
        LoadMessage("ÓÎÏ·¿ªÊ¼");
        RunMoveUp();
    }


    public void LoadMessage(string text) {
        messagePanelText.text = text;
        messagePanelText.gameObject.SetActive(true);
        //Debug.Log("2 time" + originalPosition.x + "," + originalPosition.y + "," + originalPosition.z);
        messagePanelText.gameObject.transform.position = new Vector3 (587, 316, 0);
        //Debug.Log("message position" + messagePanelText.gameObject.transform.position.x + "," + messagePanelText.gameObject.transform.position.y + "," + messagePanelText.gameObject.transform.position.z);
    }
    

    public IEnumerator MoveUp()
        {
            for (float i = 0; i <= 1.2; i += Time.deltaTime)
            {
                    messagePanelText.gameObject.transform.position = new Vector3(messagePanelText.gameObject.transform.position.x, messagePanelText.gameObject.transform.position.y + i/2, messagePanelText.gameObject.transform.position.z);
                    //Debug.Log("2 time" + messagePanelText.gameObject.transform.position.x + "," + messagePanelText.gameObject.transform.position.y + "," + messagePanelText.gameObject.transform.position.z);
                    yield return null;
            }

            messagePanelText.gameObject.SetActive(false);

        }

    public void RunMoveUp() {
        StartCoroutine(MoveUp());
    }
}
