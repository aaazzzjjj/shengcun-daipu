using System;
using UnityEngine;
using SurvivalPawnShop.Config;

namespace SurvivalPawnShop.Model
{
    /// <summary>
    /// 运行时物品数据（纯类）：引用静态档案 + 持有动态状态，可脱离 Unity 单测。
    /// </summary>
    [Serializable]
    public class ItemInstance
    {
        /// <summary>静态档案引用（由 <see cref="ItemConfigSO"/> 提供不变属性）。</summary>
        public ItemConfigSO config;

        /// <summary>锚点（左上角，单位：格）。</summary>
        public Vector2Int gridPos;

        /// <summary>是否旋转（旋转 = 形状宽高互换）。</summary>
        public bool isRotated;

        // 后续动态字段：保质期、品质等

        public int ItemID => config != null ? config.itemID : -1;
        public string Name => config != null ? config.itemName : "?";
        public ItemCategory Category => config != null ? config.category : ItemCategory.Water;

        public ItemInstance() { }

        public ItemInstance(ItemConfigSO config)
        {
            this.config = config;
        }

        /// <summary>旋转后的占格尺寸（宽、高）。</summary>
        public Vector2Int Size => GetShapeSize(config, isRotated);

        /// <summary>计算形状的宽高；旋转则互换。</summary>
        public static Vector2Int GetShapeSize(ItemConfigSO config, bool rotated)
        {
            if (config == null || config.shapeOffsets == null || config.shapeOffsets.Length == 0)
            {
                return Vector2Int.one;
            }

            int w = 0, h = 0;
            foreach (var o in config.shapeOffsets)
            {
                w = Mathf.Max(w, o.x + 1);
                h = Mathf.Max(h, o.y + 1);
            }
            return rotated ? new Vector2Int(h, w) : new Vector2Int(w, h);
        }
    }
}
