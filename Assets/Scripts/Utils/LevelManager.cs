using System.Collections;
using System.Collections.Generic;
using RTS_Cam;
using TbsFramework;
using TbsFramework.Grid;
using TbsFramework.Units;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels;
    public RTS_Camera RTSCamera;
    public Unit currentPlayer;

    public GameGUIController UIController;
    
    // 武器
    public int weaponIndex;
    // buff
    public List<Buff> BuffList;
    // 技能
    // public List<>
    
    public CellGrid currentCellGrid;
    public void InstantiateLevel(int weapon)
    {
        // 先记住自己的武器吧
        weaponIndex = weapon;
        // 启动关卡
        levels[0].SetActive(true);
        
        // 找到CellGrid
        currentCellGrid = levels[0].GetComponentInChildren<CellGrid>();
        // 开始游戏吧
        currentCellGrid.InitializeAndStart();
        currentPlayer = currentCellGrid.AIGetEnemyUnits()[0];
        (currentPlayer as RealPlayer).PlayerAttackAbility.SetCurrentAttackAbility(weapon);
        currentCellGrid.GameEnded += OnGameEnd;
        
        // 设置RTSCamera
        RTSCamera = FindObjectOfType<RTS_Camera>();
        RTSCamera.SetTarget(currentPlayer.transform);
        
        // 设置GUI的CellGrid
        UIController.CellGrid = currentCellGrid;
        UIController.Canvas.GetComponentInChildren<GoldController>().Initialize();
    }

    public void EndLevel()
    {
        levels[0].SetActive(false);
        currentCellGrid.GameEnded -= OnGameEnd;
        NextLevel(1);
    }

    private void OnGameEnd(object sender, GameEndedArgs e)
    {
        currentPlayer.OnDestroy();
        EndLevel();
    }
    
    public void NextLevel(int levelIndex)
    {
        levels[levelIndex].SetActive(true);
        // 找到CellGrid
        currentCellGrid = levels[levelIndex].GetComponentInChildren<CellGrid>();
        // 开始游戏吧
        currentCellGrid.InitializeAndStart();
        currentCellGrid.GameEnded += OnGameEnd;
        currentPlayer = currentCellGrid.AIGetEnemyUnits()[0];
        (currentPlayer as RealPlayer).PlayerAttackAbility.SetCurrentAttackAbility(weaponIndex);
        // 设置RTSCamera
        RTSCamera = FindObjectOfType<RTS_Camera>();
        RTSCamera.SetTarget(currentPlayer.transform);
        // 设置GUI的CellGrid
        UIController.CellGrid = currentCellGrid;
    }
}
