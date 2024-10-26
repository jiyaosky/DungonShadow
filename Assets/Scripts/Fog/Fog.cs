using System.Collections;
using System.Collections.Generic;
using TbsFramework.Grid;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Fog : MonoBehaviour
{
    public CellGrid CellGrid;
    public Color DarkColor;
    public Color LightColor;
    public Shader Shader;

    private Texture2D _fogTexture;
    private Color[] _fogColors;
    private Material _materialInstance;
    private Renderer _renderer;

    private void Start()
    {
        _fogTexture = new Texture2D(CellGrid.Width, CellGrid.Height);
        _fogColors = new Color[CellGrid.Width * CellGrid.Height];
        _materialInstance = new Material(Shader);
        _materialInstance.SetTexture("_FogTex", _fogTexture);
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _materialInstance;
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

}
