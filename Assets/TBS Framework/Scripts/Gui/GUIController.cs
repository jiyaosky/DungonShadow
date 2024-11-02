using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using TbsFramework.Players;
using TbsFramework;



namespace TbsFramework
{
    public class GUIController : MonoBehaviour
    {
        public CellGrid CellGrid;
        public Button EndTurnButton;

        public GameObject realPlayer;
        public GameObject Canvas;

        // public Text currentRoundText;
        //
        // public Text totalRoundText;
        
        //[SerializeField]
        //public TextMeshProUGUI turnCount; 


        public int currentRound = 0;

        public int totalRound = 10;

        void Awake()
        {
            CellGrid.LevelLoading += OnLevelLoading;
            CellGrid.LevelLoadingDone += OnLevelLoadingDone;
            CellGrid.GameEnded += OnGameEnded;
            CellGrid.TurnEnded += OnTurnEnded;
            CellGrid.GameStarted += OnGameStarted;
            // totalRoundText.text = totalRound.ToString();

            //turnCount.transform.GetComponents<TextMeshPro>().text = totalRound + " Turn Left To Extract";
        }

        void Start()
        {
            // Canvas.transform.Find("AbilitySet").GetComponentsInChildren<ToolTip>();
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            if (EndTurnButton != null)
            {
                EndTurnButton.interactable = CellGrid.CurrentPlayer is HumanPlayer;
            }
        }

        private void OnTurnEnded(object sender, bool isNetworkInvoked)
        {
            if (EndTurnButton != null)
            {
                EndTurnButton.interactable = CellGrid.CurrentPlayer is HumanPlayer;
                currentRound++;
                // currentRoundText.text = currentRound.ToString();
            }
        }

        private void OnGameEnded(object sender, GameEndedArgs e)
        {
            Debug.Log(string.Format("Player{0} wins!", e.gameResult.WinningPlayers[0]));
            if (EndTurnButton != null)
            {
                EndTurnButton.interactable = false;
            }
        }

        private void OnLevelLoading(object sender, EventArgs e)
        {
            Debug.Log("Level is loading");
        }

        private void OnLevelLoadingDone(object sender, EventArgs e)
        {
            Debug.Log("Level loading done");
            Debug.Log("Press 'm' to end turn");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M) && !(CellGrid.cellGridState is CellGridStateAITurn))
            {
                EndTurn();//User ends his turn by pressing "m" on keyboard.
                var round = totalRound - currentRound;
                //turnCount.transform.GetComponents<TextMeshPro>().text = round + " Turn Left To Extract";
            }
        }

        public void EndTurn()
        {
            CellGrid.EndTurn();
        }

        
    }
}