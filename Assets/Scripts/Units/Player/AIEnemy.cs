using System;
using System.Collections;
using System.Collections.Generic;
using Autotiles3D;
using TbsFramework.Cells;
using TbsFramework.Example1;
using TbsFramework.Grid;
using TbsFramework.Units.Abilities;
using UnityEngine;


namespace TbsFramework.Units
{
    public class AIEnemy : MyUnit
    {
        // 0 ->0
        // 
        // public override void MaskAsAISight()
        // {
        //     // facingDirection = transform.rotation.eulerAngles.y;
        //     Check();
        // }
        public override void MaskAsAISight()
        {
            foreach (var cell in insightCells)
            {
                cell.transform.Find("Inline").gameObject.SetActive(false);
            }
            CheckAISight();
        }

        public void CheckAISight()
        {
            List<Cell> cells = GetVisibleCells(FindObjectOfType<CellGrid>());
            for (int i = 0; i < cells.Count; i++)
            {
                // (Autotiles3D_BlockBehaviour)cells[i].MaskAsAISight();
                Autotiles3D_BlockBehaviour cell = cells[i].GetComponent<Autotiles3D_BlockBehaviour>();
                cell.MaskAsAISight();
            }
        }
        
        public float sightRange = 3f;
        public float sightAngle = 45f;
        private List<Cell> insightCells = new List<Cell>();

        // 获取视野范围内的所有单元格
        public List<Cell> GetVisibleCells(CellGrid cellGrid)
        {
            GridUtils.GetCellsInsight(cellGrid, insightCells,
                new Vector2Int(Mathf.RoundToInt(this.Cell.OffsetCoord.x), Mathf.RoundToInt(this.Cell.OffsetCoord.y)),
                sightRange,
                transform.rotation.eulerAngles.y,
                sightAngle);
            return insightCells;
        }

    }
}