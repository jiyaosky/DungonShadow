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
        originalPosition = messagePanelText.gameObject.transform.position;
        LoadMessage("Test Message Moving Up");
        RunMoveUp();
    }


    public void LoadMessage(string text) {
        messagePanelText.text = text;
        messagePanelText.gameObject.SetActive(true);
        messagePanelText.gameObject.transform.position = originalPosition;

    }
    

    public IEnumerator MoveUp()
        {
            for (float i = 0; i <= 1.2; i += Time.deltaTime)
            {
                    // set color with i as alpha
                    messagePanelText.gameObject.transform.position = new Vector3(messagePanelText.gameObject.transform.position.x, messagePanelText.gameObject.transform.position.y + i/2, messagePanelText.gameObject.transform.position.z);
                    yield return null;
            }

            messagePanelText.gameObject.SetActive(false);

        }

    public void RunMoveUp() {
        StartCoroutine(MoveUp());
    }
}
