using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    // Start is called before the first frame update


    void Awake()
    {

        this.gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
             });
    }

}
