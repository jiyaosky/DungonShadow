using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour
{

    [SerializeField]
    public GameObject turnController;

    public GUIController guiController;

    public int totleRound;
    public int currentRound;

    // Start is called before the first frame update
    void Start()
    {
        guiController = turnController.GetComponent<GUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        //totleRound = guiController.totleRound;
        //currentRound = guiController.currentRound; 
    }
}
