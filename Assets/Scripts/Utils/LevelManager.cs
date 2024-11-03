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

    public int weaponIndex;

    public GUIController UIController;
    
    
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
        
        // 设置RTSCamera
        RTSCamera = FindObjectOfType<RTS_Camera>();
        RTSCamera.SetTarget(currentPlayer.transform);
        
        // 设置GUI的CellGrid
        UIController.CellGrid = currentCellGrid;
        
    }

    public void EndLevel()
    {
        
    }
    
    public void NextLevel(int levelIndex)
    {
        levels[levelIndex].SetActive(true);
        // 找到CellGrid
        currentCellGrid = levels[levelIndex].GetComponentInChildren<CellGrid>();
        // 开始游戏吧
        currentCellGrid.InitializeAndStart();
        currentPlayer = currentCellGrid.AIGetEnemyUnits()[0];
        // 设置RTSCamera
        RTSCamera = FindObjectOfType<RTS_Camera>();
        RTSCamera.SetTarget(currentPlayer.transform);
        // 设置GUI的CellGrid
        UIController.CellGrid = currentCellGrid;
    }
}
