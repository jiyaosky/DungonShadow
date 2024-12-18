using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using TbsFramework.Cells;
using TbsFramework.Cells.Highlighters;
using TbsFramework.Grid;

#if UNITY_EDITOR
using UnityEditorInternal;
#endif

namespace Autotiles3D
{
    public class Autotiles3D_BlockBehaviour : Square
    {

        [HideInInspector]
        [SerializeField] private int _tileID = -1;
        [HideInInspector]
        [SerializeField] private int _ruleID;
        [HideInInspector]
        [SerializeField] private string _tileName;
        [HideInInspector]
        [SerializeField] private string _group;
        [HideInInspector]
        [SerializeField] private Vector3Int _internalPosition;
        [HideInInspector]
        [SerializeField] private Quaternion _localRotation;
        [HideInInspector]
        [SerializeField] private bool _isBaked;

        private Autotiles3D_Grid _grid;
        private Autotiles3D_Anchor _anchor;
        public GameObject View;

        public string TileName => _tileName;
        public int TileID => _tileID;
        public string GroupName => _group;
        public int RuleID => _ruleID;
        public Vector3Int InternalPosition { get => _internalPosition; set => _internalPosition = value; }
        public Quaternion LocalRotation { get => _localRotation; set => _localRotation = value; }

        public Autotiles3D_Grid Grid
        {
            get
            {
                if (this == null)
                    return null;
                if (_grid == null)
                    _grid = transform.GetComponentInParent<Autotiles3D_Grid>();
                return _grid;
            }
        }
        public Autotiles3D_Anchor Anchor
        {
            get
            {
                if (this == null)
                    return null;
                if (_anchor == null)
                    _anchor = transform.GetComponentInParent<Autotiles3D_Anchor>();
                return _anchor;
            }
        }

#if UNITY_EDITOR

        public Autotiles3D_Tile GetTile()
        {
            return Autotiles3D_Utility.GetTile(_tileID, _tileName, _group);
        }


        public void UpdateTileInfo(Autotiles3D_Tile tile)
        {
            _group = tile.Group;
            _tileName = tile.Name;
            _tileID = tile.TileID;
        }
        public InternalNode Randomize(bool focusInstance = true)
        {
            InternalNode node = GetInternalNode();
            if (node != null)
            {
                node.Randomize();
            }
            else
            {
                Debug.LogWarning("Couldnt retrieve Internal node. Is this block part of the correct Grid->Layer->Anchor hierarchy?");
            }
            return node;
        }

#endif
        public void ToggleView(bool enable)
        {
            if (View != null)
                View.SetActive(enable);
        }

        public InternalNode GetInternalNode()
        {
            if (Anchor != null)
            {
                Autotiles3D_TileLayer layer = Anchor.Layer;
                if (layer != null)
                {
                    if (layer.ContainsKey(_internalPosition))
                        return layer.GetInternalNode(_internalPosition);
                }
            }
            return null;
        }
        public bool IsBaked
        {
            get
            {
                if (Anchor == null)
                    _isBaked = false;
                else if (Anchor.BakedParent == null)
                    _isBaked = false;
                return _isBaked;
            }
            set
            {
                _isBaked = value;
            }
        }
        public void OnInstanceUpdate(InternalNode node)
        {
            this._tileID = node.TileID;
            this._group = node.TileGroupName;
            this._tileName = node.TileName;
            this._ruleID = node.RuleID;

            if (Mathf.Abs(node.TileID) < 5)
            {
                Debug.Log("assigning empty tileiD");
            }

            this.InternalPosition = node.InternalPosition;
            this.LocalRotation = node.LocalRotation;
        }

        public override Vector3 GetCellDimensions()
        {
            return GetComponent<Renderer>().bounds.size;
        }
        
        public CellHighlighter MaskAISight;
        // public CellHighlighter UnMaskAISight;
        public void MaskAsAISight()
        {
            var cell = this.transform.Find("Inline");
            if (cell != null)
            {
                this.transform.Find("Inline").gameObject.SetActive(true);
                MaskAISight.Apply(this);
            }
        }

        // public void UnmaskAISight()
        // {
        //     var cell = this.transform.Find("Inline");
        //     if (cell != null)
        //     {
        //         this.transform.Find("Inline").gameObject.SetActive(false);
        //         // UnMaskAISight.Apply(this);
        //     }
        // }
        // public override void UnMark()
        // {
        //     UnmaskAISight();
        //     base.UnMark();
        // }
    }

}


