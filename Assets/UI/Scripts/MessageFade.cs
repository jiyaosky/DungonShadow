using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageFade : MonoBehaviour
{
    public Image messagePanelImage;

    public TextMeshProUGUI messagePanelText;

    private void Awake()
    {
        messagePanelImage = this.gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        RunFade();
    }


    public IEnumerator FadeAway()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime/2)
        {
                // set color with i as alpha
                messagePanelImage.color = new Color(messagePanelImage.color.r, messagePanelImage.color.g, messagePanelImage.color.b, i);

                messagePanelText.color = new Color(messagePanelText.color.r, messagePanelText.color.g, messagePanelText.color.b, i);

                yield return null;
        }

        this.gameObject.SetActive(false);

        messagePanelImage.color = new Color(messagePanelImage.color.r, messagePanelImage.color.g, messagePanelImage.color.b, 1);
        messagePanelText.color = new Color(messagePanelText.color.r, messagePanelText.color.g, messagePanelText.color.b, 1);
    }

    public void RunFade() {
        StartCoroutine(FadeAway());
    }
}
