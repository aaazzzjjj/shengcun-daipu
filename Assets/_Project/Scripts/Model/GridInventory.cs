using System;
using System.Collections.Generic;
using UnityEngine;
using SurvivalPawnShop.Config;

namespace SurvivalPawnShop.Model
{
    /// <summary>
    /// 统一格子库存模型：全游戏唯一一套占用/放置/旋转算法。
    /// 仓库(24×10)、交易台(2×6)、客户窗口(2×6) 都是它的实例。
    /// 坐标原点为左上角 (0,0)，右为 X+，下为 Y+。
    /// </summary>
    public class GridInventory
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        private bool[,] occupied;
        private readonly Dictionary<Vector2Int, ItemInstance> items = new Dictionary<Vector2Int, ItemInstance>();

        public GridInventory(int columns, int rows)
        {
            if (columns <= 0 || rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns), "网格行列数必须为正。");
            }
            Columns = columns;
            Rows = rows;
            occupied = new bool[columns, rows];
        }

        public IEnumerable<ItemInstance> AllItems => items.Values;

        /// <summary>获取锚点格上的物品（非锚点格请用 <see cref="FindItemAt"/>）。</summary>
        public ItemInstance GetItem(Vector2Int cell)
        {
            return items.TryGetValue(cell, out var item) ? item : null;
        }

        /// <summary>任意格查询物品（含非锚点格）。</summary>
        public ItemInstance FindItemAt(Vector2Int cell)
        {
            foreach (var item in items.Values)
            {
                var cells = GetOccupiedCells(item.config, item.gridPos, item.isRotated);
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == cell) return item;
                }
            }
            return null;
        }

        /// <summary>某格是否被占用（越界视为不占用，便于拖拽高亮判断）。</summary>
        public bool IsCellOccupied(Vector2Int cell)
        {
            return cell.x >= 0 && cell.x < Columns && cell.y >= 0 && cell.y < Rows && occupied[cell.x, cell.y];
        }

        /// <summary>能否在指定锚点、指定旋转下放置。</summary>
        public bool CanPlace(ItemConfigSO config, Vector2Int anchor, bool rotated)
        {
            if (config == null) return false;

            var cells = GetOccupiedCells(config, anchor, rotated);
            for (int i = 0; i < cells.Length; i++)
            {
                var c = cells[i];
                if (c.x < 0 || c.x >= Columns || c.y < 0 || c.y >= Rows) return false; // 越界
                if (occupied[c.x, c.y]) return false; // 重叠
            }
            return true;
        }

        /// <summary>尝试放置；成功则写回 item 的 gridPos / isRotated 并返回 true。</summary>
        public bool TryPlace(ItemInstance item, Vector2Int anchor, bool rotated)
        {
            if (item == null || item.config == null) return false;
            if (!CanPlace(item.config, anchor, rotated)) return false;

            var cells = GetOccupiedCells(item.config, anchor, rotated);
            for (int i = 0; i < cells.Length; i++)
            {
                var c = cells[i];
                occupied[c.x, c.y] = true;
            }

            item.gridPos = anchor;
            item.isRotated = rotated;
            items[anchor] = item;
            return true;
        }

        /// <summary>移除锚点格上的物品并返回；找不到返回 null。</summary>
        public ItemInstance Remove(Vector2Int origin)
        {
            if (!items.TryGetValue(origin, out var item)) return null;

            var cells = GetOccupiedCells(item.config, item.gridPos, item.isRotated);
            for (int i = 0; i < cells.Length; i++)
            {
                var c = cells[i];
                occupied[c.x, c.y] = false;
            }

            items.Remove(origin);
            return item;
        }

        /// <summary>计算物品旋转后占用的实际格坐标（含锚点，锚点保持 (0,0) 偏移不变）。</summary>
        public static Vector2Int[] GetOccupiedCells(ItemConfigSO config, Vector2Int anchor, bool rotated)
        {
            if (config == null || config.shapeOffsets == null || config.shapeOffsets.Length == 0)
            {
                return new Vector2Int[] { anchor };
            }

            var offsets = config.shapeOffsets;
            var cells = new Vector2Int[offsets.Length];
            for (int i = 0; i < offsets.Length; i++)
            {
                var o = offsets[i];
                // 旋转 90°：交换 (x, y)，锚点 (0,0) 保持不变。
                cells[i] = rotated ? anchor + new Vector2Int(o.y, o.x) : anchor + o;
            }
            return cells;
        }
    }
}
