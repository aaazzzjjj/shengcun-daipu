using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryConfig", menuName = "GameConfig/InventoryConfig")]
public class InventoryConfig : ScriptableObject
{
    [Header("网格尺寸")]
    public int columns = 24;
    public int rows = 10;
    
    [Header("格子大小（不动）")]
    public Vector2 cellSize = new Vector2(32, 32);

    [Header("预制体引用")]
    public GameObject slotPrefab;
    
    // 如果你以后需要配置其他属性，比如背景图、边框宽度，都可以加在这里
}