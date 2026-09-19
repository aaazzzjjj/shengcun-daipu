using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridUI : MonoBehaviour
{
    [Header("配置文件引用")]
    [SerializeField] private InventoryConfig config; // 这里拖入刚才创建的配置文件

    private GridLayoutGroup gridLayout;
    private RectTransform rectTransform;

    void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }


    void Start()
    {
        if (config == null)
        {
            Debug.LogError("InventoryConfig 未赋值！");
            return;
        }

        GenerateGrid();
    }

    public void GenerateGrid()
    {
        if (config.slotPrefab == null) return;

        // 1. 清理旧格子
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 2. 根据配置文件设置 GridLayoutGroup
        gridLayout.cellSize = config.cellSize;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = config.columns;

        // 3. 动态设置 SlotContainer 宽高
        rectTransform.sizeDelta = new Vector2(config.columns * config.cellSize.x, config.rows * config.cellSize.y);

        // 4. 循环生成
        for (int y = 0; y < config.rows; y++)
        {
            for (int x = 0; x < config.columns; x++)
            {
                GameObject slotGO = Instantiate(config.slotPrefab, transform);
                slotGO.name = $"Slot_{x}_{y}";

                Slot slot = slotGO.GetComponent<Slot>();
                if (slot != null)
                {
                    slot.Init(x, y);
                }
            }
        }

        Debug.Log($"根据配置生成仓库：{config.columns} x {config.rows}，单格 {config.cellSize}");
    }
}
