using UnityEngine;

namespace SurvivalPawnShop.Config
{
    /// <summary>物品品类（用于价格波动事件定位）。</summary>
    public enum ItemCategory
    {
        Water = 0,   // 水类
        Soda = 1,    // 汽水类
        Juice = 2,   // 果汁类
    }

    /// <summary>
    /// 物品静态档案（ScriptableObject）：只存不变属性。
    /// 运行时数据放 <see cref="SurvivalPawnShop.Model.ItemInstance"/>，二者严格分离。
    /// </summary>
    [CreateAssetMenu(fileName = "NewItemConfig", menuName = "GameConfig/ItemConfig")]
    public class ItemConfigSO : ScriptableObject
    {
        [Header("标识")]
        public int itemID;
        public string itemName;

        [Header("外观")]
        public Sprite icon;

        [Header("品类（用于价格波动）")]
        public ItemCategory category;

        [Header("固定售价（唯一价格属性）")]
        public int sellPrice;

        [Header("占格形状（含 (0,0) 锚点，单位：格）")]
        public Vector2Int[] shapeOffsets = new Vector2Int[] { new Vector2Int(0, 0) };

        /// <summary>批发价 = 固定售价 × 0.8，自动计算，不单独存储。</summary>
        public int WholesalePrice => Mathf.RoundToInt(sellPrice * 0.8f);
    }
}
