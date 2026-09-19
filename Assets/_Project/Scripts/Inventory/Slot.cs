using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 这里用 public 无所谓，因为是只读的数据容器，或者同样用 [SerializeField] private + 属性
    public int PosX { get; private set; }
    public int PosY { get; private set; }

    // 提供一个初始化方法，明确告知外部怎么给它赋值
    public void Init(int x, int y)
    {
        PosX = x;
        PosY = y;
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