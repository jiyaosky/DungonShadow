using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour
{
    public Image messagePanelImage;
    //public TextMeshProUGUI

    void Start()
    {
        messagePanelImage = this.gameObject.GetComponent<Image>();

        //RunFade();
    }


    public IEnumerator FadeAway()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime/2)
        {
                // set color with i as alpha
                messagePanelImage.color = new Color(messagePanelImage.color.r, messagePanelImage.color.g, messagePanelImage.color.b, i);
                yield return null;
        }

        //this.gameObject.SetActive(false);

    }

    void RunFade() {
        StartCoroutine(FadeAway());
    }
}
