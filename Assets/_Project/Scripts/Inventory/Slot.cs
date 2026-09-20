using UnityEngine;
using UnityEngine.EventSystems;

namespace SurvivalPawnShop.UI
{
    /// <summary>
    /// 格子表现：仅负责悬停/高亮等视图职责，持有视图索引 GridPos。
    /// 占用真相在 <see cref="SurvivalPawnShop.Model.GridInventory"/>。
    /// </summary>
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>视图索引（该格子所在行列），非物品数据。</summary>
        public Vector2Int GridPos { get; private set; }

        public void Init(int x, int y)
        {
            GridPos = new Vector2Int(x, y);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // 鼠标进入格子时的逻辑（比如显示 Tooltip）
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // 鼠标离开格子时的逻辑
        }
    }
}