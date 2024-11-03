using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

namespace TbsFramework
{
    public class GoldController : MonoBehaviour
    {

        [SerializeField]
        public TextMeshProUGUI  gold_text;

        private Dictionary<int, int> Account = new Dictionary<int, int>();
        public int StartingAmount = 5;
        // public int realPlayerGold = ;
        
        public void Awake()
        {
            FindObjectOfType<CellGrid>().GameStarted += OnGameStarted;

            //text update
            gold_text.text = "" + StartingAmount;
        }

        private void OnGameStarted(object sender, System.EventArgs e)
        {
            Account.Add(0, StartingAmount);
        }

        public int GetValue(int playerNumber)
        {
            if (Account.ContainsKey(playerNumber))
            {
                return Account[playerNumber];
            }
            return 0;
        }
        public void UpdateValue(int playerNumber, int delta)
        {
            Assert.IsTrue(Account.ContainsKey(playerNumber), string.Format("The Account of player number {0} was not initialized", playerNumber));
            Account[playerNumber] += delta;
            
            //text update
            gold_text.text = "" + Account[playerNumber];
        }
    }
}