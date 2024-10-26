using System;
using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using TbsFramework.Grid;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Fog : MonoBehaviour
{
    public CellGrid CellGrid;
    public Color DarkColor;
    public Color LightColor;
    public Material Material;
    public float Radius = 5;
    public Transform PlayerTransform;

    private Texture2D _fogTexture;
    private Color[] _fogColors;
    private Material _materialInstance;
    private Renderer _renderer;

    private int _lastPlayerX = -1;
    private int _lastPlayerY = -1;

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

    public void LightUpRadius(Vector2 position, float radius)
    {
        SetAllDark();
        List<Cell> cells = new List<Cell>();
        CellGrid.GetCellsInsight(cells, position, radius);
        foreach (var cell in cells)
        {
            Vector2Int cellIndex = CellGrid.GetCellIndexInMap(cell);
            //Debug.Log(cellIndex);
            SetLight(cellIndex.x, cellIndex.y);
        }

        UpdateToGPU();
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
            LightUpRadius(playerPosition, Radius);
            _lastPlayerX = playerIndex.x;
            _lastPlayerY = playerIndex.y;
        }
    }

}
