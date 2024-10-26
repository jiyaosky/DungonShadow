using System;
using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using TbsFramework.Grid;
using UnityEngine;

public static class GridUtils
{
    public static readonly Vector2Int[] RadialPattern;
    private static readonly float[] RadialPatternRadii;

    static GridUtils(){
        List<Vector2Int> list = new List<Vector2Int>();
        for (int i = -60; i < 60; i++)
        {
            for (int j = -60; j < 60; j++)
            {
                list.Add(new Vector2Int(i, j));
            }
        }
        list.Sort(delegate (Vector2Int A, Vector2Int B)
        {
            float num = A.x * (float)A.x + A.y * (float)A.y;
            float num2 = B.x * (float)B.x + B.y * (float)B.y;
            if (num < num2)
            {
                return -1;
            }
            return (num != num2) ? 1 : 0;
        });
        for (int k = 0; k < 10000; k++)
        {
            RadialPattern[k] = list[k];
            Vector2Int vector2Int = list[k];
            RadialPatternRadii[k] = Mathf.Sqrt(vector2Int.x * (float)vector2Int.x + vector2Int.y * (float)vector2Int.y);
        }
    }

    public static void GetCellsInsight(this CellGrid grids,List<Cell> output, Vector2 position, float range)
    {
        output.Clear();
        int numCellsInRadius = NumCellsInRadius(range);
        for (int i = 0; i < numCellsInRadius; i++)
        {
            Vector2Int vector2Int = RadialPattern[i];
            Vector2Int vector2Int2 = new Vector2Int(Mathf.RoundToInt(position.x) + vector2Int.x, Mathf.RoundToInt(position.y) + vector2Int.y);
            Cell cell = grids.GetCell(vector2Int2);
            if (cell != null)
            {
                output.Add(cell);
            }
        }
    }

    public static int NumCellsInRadius(float radius)
    {
        float num = radius + float.Epsilon;
        int num2 = Array.BinarySearch(RadialPatternRadii, num);
        if (num2 < 0)
        {
            return ~num2;
        }
        for (int i = num2; i < 10000; i++)
        {
            if (RadialPatternRadii[i] > num)
            {
                return i;
            }
        }
        return 10000;
    }


}
