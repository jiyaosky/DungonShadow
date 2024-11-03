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
        
        public void Initialize()
        {
            Debug.Log("初始化金币");
            FindObjectOfType<CellGrid>().GameStarted += OnGameStarted;
        }

        
        public void Awake()
        {
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
            if (!Account.ContainsKey(playerNumber)){Account.Add(0, StartingAmount);}
            Account[playerNumber] += delta;
            //text update
            gold_text.text = "" + Account[playerNumber];
        }
    }
}