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

    /// <summary>
    /// 构建距离场
    /// </summary>
    static GridUtils(){
        RadialPattern = new Vector2Int[10000];
        RadialPatternRadii = new float[10000];
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

    /// <summary>
    /// 获取视野内所有格子
    /// </summary>
    /// <param name="grids">地图</param>
    /// <param name="output">结果</param>
    /// <param name="position">中心位置
    /// <param name="range">半径</param>
    /// <param name="direcion">朝向(y轴旋转)
    /// <param name="angle">视野角度</param>
    public static void GetCellsInsight(this CellGrid grids, List<Cell> output, Vector2 position, float range,float direcion, float angle)
    {
        output.Clear();
        int numCellsInRadius = NumCellsInRadius(range);
        Vector2Int positionInt = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));



        for (int i = 0; i < numCellsInRadius; i++)
        {
            Vector2Int vector2Int = RadialPattern[i];
            Vector2Int vector2Int2 = new Vector2Int(Mathf.RoundToInt(position.x) + vector2Int.x, Mathf.RoundToInt(position.y) + vector2Int.y);
            Cell cell = grids.GetCell(vector2Int2);
            Vector2 vectDirection = new Vector2(vector2Int.x, vector2Int.y);
            //rotate the vector by 90 degrees
            //Vector2 vectDirectionRotated = new Vector2(vectDirection.y, -vectDirection.x);

            if (cell != null &&
            IsLineOfSight(positionInt, vector2Int2, grids, null) &&
            Vector2.Angle(new Vector2(vector2Int.x, vector2Int.y), GetDirectionByAngle(direcion)) <= angle)
            {
                output.Add(cell);
            }

        }
    }

    /// <summary>
    /// 获取角度对应的方向
    /// </summary>
    /// <param name="angleY">角度</param>
    /// <returns></returns>
    public static Vector2 GetDirectionByAngle(float angleY)
    {
        float angle = angleY * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }

    /// <summary>
    /// 获取两点之间的所有格子
    /// </summary>
    /// <param name="grids">地图</param>
    /// <param name="output">结果</param>
    /// <param name="start">起点</param>
    /// <param name="end">终点</param>
    public static void GetCellsBetween(this CellGrid grids, List<Cell> output, Vector2 start, Vector2 end)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();
        Vector2Int startInt = new Vector2Int(Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y));
        Vector2Int endInt = new Vector2Int(Mathf.RoundToInt(end.x), Mathf.RoundToInt(end.y));
        BresenhamCellsBetween(listPos, startInt.x, startInt.y, endInt.x, endInt.y);
        output.Clear();
        foreach (var pos in listPos)
        {
            Cell cell = grids.GetCell(pos);
            if (cell != null)
            {
                output.Add(cell);
            }
        }
    }

    /// <summary>
    /// 获取两点之间的所有格子坐标
    /// </summary>
    /// <param name="output"></param>
    /// <param name="startX"></param>
    /// <param name="startY"></param>
    /// <param name="endX"></param>
    /// <param name="endY"></param>
    public static void BresenhamCellsBetween(List<Vector2Int> output, int startX, int startY, int endX, int endY)
    {
        output.Clear();
        int intervalX = Mathf.Abs(endX - startX);
        int signX = (startX < endX) ? 1 : (-1);
        int minusIntevalY = -Mathf.Abs(endY - startY);
        int signY = (startY < endY) ? 1 : (-1);
        int value = intervalX + minusIntevalY;
        int maxIteration = 1000;
        while (true)
        {
            output.Add(new Vector2Int(startX, startY));
            if (startX == endX && startY == endY)
            {
                break;
            }
            int num = 2 * value;
            if (num >= minusIntevalY)
            {
                value += minusIntevalY;
                startX += signX;
            }
            if (num <= intervalX)
            {
                value += intervalX;
                startY += signY;
            }
            maxIteration--;
            if (maxIteration <= 0)
            {

                break;
            }
        }
    }

    /// <summary>
    /// 两点直接视线是否遮挡
    /// </summary>
    /// <param name="start">起点</param>
    /// <param name="end">终点</param>
    /// <param name="map">地图</param>
    /// <param name="validator"></param>
    /// <returns></returns>
    public static bool IsLineOfSight(Vector2Int start, Vector2Int end, CellGrid map, Func<Vector2Int, bool> validator = null)
    {
        RectInt startRect = new RectInt(start.x, start.y, 1, 1);
        RectInt endRect = new RectInt(end.x, end.y, 1, 1);
        return IsLineOfSight(start, end, map, startRect, endRect, validator);
    }

    public static bool IsLineOfSight(Vector2Int start, Vector2Int end, CellGrid map, RectInt startRect, RectInt endRect, Func<Vector2Int, bool> validator = null)
    {
        bool flag = (start.x != end.x) ? (start.x < end.x) : (start.y < end.y);
        int intervalX = Mathf.Abs(end.x - start.x);
        int intervalY = Mathf.Abs(end.y - start.y);
        int startX = start.x;
        int startY = start.y;
        int length = 1 + intervalX + intervalY;
        int signX = (end.x > start.x) ? 1 : (-1);
        int signY = (end.y > start.y) ? 1 : (-1);
        int interval = intervalX - intervalY;
        intervalX *= 2;
        intervalY *= 2;
        Vector2Int intVec = default;
        while (length > 1)
        {
            intVec.x = startX;
            intVec.y = startY;
            if (endRect.Contains(intVec))
            {
                return true;
            }
            if (!startRect.Contains(intVec))
            {
                // todo: check if cell is obsetacle
                Cell cell = map.GetCell(intVec);
                if (cell != null && cell.IsSightObstructed)
                {
                    return false;
                }

                if (validator != null && !validator(intVec))
                {
                    return false;
                }
            }
            if (interval > 0 || (interval == 0 && flag))
            {
                startX += signX;
                interval -= intervalY;
            }
            else
            {
                startY += signY;
                interval += intervalX;
            }
            length--;
        }
        return true;
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
