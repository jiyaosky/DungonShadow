using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Units;
using Unity.VisualScripting;
using UnityEngine;

using Unit = TbsFramework.Units.Unit;

[RequireComponent(typeof(MeshRenderer))]
public class Fog : MonoBehaviour
{
    public CellGrid CellGrid;
    public Color DarkColor;
    public Color LightColor;
    public Material Material;
    public float Radius = 5;
    public Transform PlayerTransform;
    public bool through = false;

    private Texture2D _fogTexture;
    private Color[] _fogColors;
    private Material _materialInstance;
    private Renderer _renderer;

    private readonly List<Cell> _cells = new List<Cell>();

    private int _lastPlayerX = -1;
    private int _lastPlayerY = -1;

    public float ViewAngle = 90;

    private void Awake()
    {
        CellGrid.LevelLoadingDone += (object sender, EventArgs e) =>
        {
            _fogTexture = new Texture2D(CellGrid.Width, CellGrid.Height);
            _fogColors = new Color[CellGrid.Width * CellGrid.Height];
            _materialInstance = new Material(Material);
            _materialInstance.SetTexture("_MainTex", _fogTexture);
            _materialInstance.renderQueue = 4000;
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = _materialInstance;

            //test
            //LightUpRadius(new Vector2(10, 10), 5);
        };
    }

    public void SetAllDark()
    {
        for (int i = 0; i < _fogColors.Length; i++)
        {
            _fogColors[i] = DarkColor;
        }
    }

    public void SetAllLight()
    {
        for (int i = 0; i < _fogColors.Length; i++)
        {
            _fogColors[i] = LightColor;
        }
    }

    public void LightUpRadius(Vector2 position, float radius, bool through)
    {
        SetAllDark();
        CellGrid.GetCellsInsight(_cells, position, radius, PlayerTransform.rotation.eulerAngles.y, ViewAngle, through);
        foreach (var cell in _cells)
        {
            Vector2Int cellIndex = CellGrid.GetCellIndexInMap(cell);
            //Debug.Log(cellIndex);
            SetLight(cellIndex.x, cellIndex.y);
        }

        UpdateToGPU();

        List<Unit> allUnits = CellGrid.GetAIEnemies();


        Vector2 playerDirection = GridUtils.GetDirectionByAngle(PlayerTransform.rotation.eulerAngles.y);

        foreach (var unit in allUnits)
        {
            Vector2 enemyPosition = new Vector2(unit.transform.position.x, unit.transform.position.z);
            if (Vector2.SqrMagnitude(enemyPosition - position) <= radius * radius &&
                Vector2.Angle(enemyPosition - position, playerDirection) <= ViewAngle)
            {
                //todo: 显示
            }
            else
            {
                //todo: 隐藏
            }
        }

    }

    public void SetDark(int x, int y)
    {
        _fogColors[y * CellGrid.Width + x] = DarkColor;
    }

    public void SetLight(int x, int y)
    {
        _fogColors[y * CellGrid.Width + x] = LightColor;
    }

    public void UpdateToGPU()
    {
        _fogTexture.SetPixels(_fogColors);
        _fogTexture.Apply();
    }

    void Update(){
        if (PlayerTransform == null)
        {
            return;
        }

        Vector2 playerPosition = new Vector2(PlayerTransform.position.x, PlayerTransform.position.z);
        Vector2Int playerIndex = CellGrid.GetCellIndexInMap((int)playerPosition.x, (int)playerPosition.y);
        if (playerIndex.x != _lastPlayerX || playerIndex.y != _lastPlayerY)
        {
            LightUpRadius(playerPosition, Radius, through);
            _lastPlayerX = playerIndex.x;
            _lastPlayerY = playerIndex.y;
        }
    }

}
