using UnityEngine;

namespace SurvivalPawnShop.Utils
{
    /// <summary>
    /// 网格坐标换算工具：坐标原点左上角 (0,0)，右为 X+，下为 Y+。
    /// </summary>
    public static class GridMath
    {
        /// <summary>
        /// 世界坐标 -> 网格坐标（左上原点）。
        /// </summary>
        /// <param name="worldPos">世界/屏幕坐标。</param>
        /// <param name="gridOrigin">网格左上角的世界坐标。</param>
        /// <param name="cellSize">单格大小（px）。</param>
        public static Vector2Int WorldToGrid(Vector2 worldPos, Vector2 gridOrigin, float cellSize)
        {
            int x = Mathf.FloorToInt((worldPos.x - gridOrigin.x) / cellSize);
            int y = Mathf.FloorToInt((gridOrigin.y - worldPos.y) / cellSize);
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// 网格坐标 -> 世界坐标（返回格子中心点）。
        /// </summary>
        public static Vector2 GridToWorld(Vector2Int cell, Vector2 gridOrigin, float cellSize)
        {
            float x = gridOrigin.x + (cell.x + 0.5f) * cellSize;
            float y = gridOrigin.y - (cell.y + 0.5f) * cellSize;
            return new Vector2(x, y);
        }
    }
}
