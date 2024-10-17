using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EclipseDraw : MonoBehaviour
{
    private static readonly int ShaderId_Color = Shader.PropertyToID("_Color");

    private MeshFilter _meshFilter;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Shader _shader;
    private Material _material;

    [ColorUsage(true, true)]
    public Color color = Color.white;


    private Mesh Mesh
    {
        get
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.sharedMaterial.renderQueue = 2500;
                }
            }

            if (_meshFilter == null)
            {
                _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
                //last
                _skinnedMeshRenderer.sharedMaterial.renderQueue = 2500;
            }
            return _meshFilter == null ? _skinnedMeshRenderer.sharedMesh : _meshFilter.sharedMesh;
        }
    }

    private Shader Shader
    {
        get
        {
            if (_shader == null)
            {
                _shader = Shader.Find("Unlit/InverseDepthTest");
            }
            return _shader;
        }
    }

    private Material Material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(Shader);
            }
            return _material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Material.SetColor(ShaderId_Color, color);
        Graphics.DrawMesh(Mesh, transform.localToWorldMatrix, Material, 0);
    }

    private void OnDestroy()
    {
        if (_material != null)
        {
            DestroyImmediate(_material);
        }
    }
}
