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
        private Quaternion facingRotation;
        // 0 ->0
        // 
        // public override void MaskAsAISight()
        // {
        //     // facingDirection = transform.rotation.eulerAngles.y;
        //     Check();
        // }
        public void Check()
        {
            List<Cell> cells = GetVisibleCells(FindObjectOfType<CellGrid>());
            for (int i = 0; i < cells.Count; i++)
            {
                // (Autotiles3D_BlockBehaviour)cells[i].MaskAsAISight();
                Autotiles3D_BlockBehaviour cell = cells[i].GetComponent<Autotiles3D_BlockBehaviour>();
                cell.MaskAsAISight();
            }
        }

        // 获取视野范围内的所有单元格
        public List<Cell> GetVisibleCells(CellGrid cellGrid)
        {
            List<Cell> visibleCells = new List<Cell>();
            Cell currentCell = this.Cell;
            int range = 3;

            var ox = currentCell.OffsetCoord.x;
            var oy = currentCell.OffsetCoord.y;
            
            for (int x = (int)ox - range; x <= currentCell.OffsetCoord.x + range; x++)
            {
                for (int y = (int)oy - range; y <= currentCell.OffsetCoord.y + range; y++)
                {
                    Cell cell = cellGrid.GetCell(x,y);
                    if (cell != null)
                    {
                        int dx = x - (int)ox;
                        int dy = y - (int)oy;
                        Vector3 direction = new Vector3(dx, 0, dy).normalized;
                        float angle = Vector3.Angle(direction, facingRotation * Vector3.forward);
                        if (angle <= 90)
                        {
                            visibleCells.Add(cell);
                        }
                    }
                }
            }

            return visibleCells;
        }

    }
}