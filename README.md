# 生存档铺

Unity 2D 生存经营模拟游戏：玩家经营一间**租来的档铺**，收购与售卖商品赚取差价，在**周期性上涨的租金**压力下维持经营。

## 项目状态

### 已完成

- **格子库存模型** `GridInventory`：统一放置/旋转/占用算法，仓库(24×10)、交易台、客户窗口共用
- **物品系统**：`ItemConfigSO` 静态档案（售价、品类、占格形状）+ `ItemInstance` 运行时数据
- **核心基础设施**：`GameServices` 服务定位器 + `GameEvents` 事件中心
- **网格工具**：`GridMath` 屏幕 ↔ 网格坐标换算
- **网格 UI**：`InventoryGridUI` + `InventorySlot`，按 `InventoryConfigSO` 动态生成 24×10 格子
- **空心边框 Shader**：`UIHollowBorder` + `HollowBorderMat` 材质

### 规划中

- `InventorySystem` 让网格接入数据层、`DragController` 拖拽交互
- 交易 / 经济 / 时间循环 / 顾客 / 报纸事件系统

详见 [策划文档](策划文档.md)、[架构设计](架构设计.md)、[开发记录](开发记录_2026-09-19.md)。

## 目录结构

```
Assets/_Project/Scripts/
├── Core/       # GameServices 服务定位器、GameEvents 事件中心
├── Config/     # InventoryConfigSO、ItemConfigSO 配置档案
├── Model/      # GridInventory、ItemInstance 数据模型
├── UI/         # InventoryGridUI、InventorySlot 网格表现
└── Utils/      # GridMath 坐标换算

Assets/_Project/
├── Prefabs/    # ItemSlot 等预制体
├── Materials/  # UI 材质
├── Shaders/    # UI 自定义 Shader
├── Art/        # UI 美术资源
└── Scenes/     # Test 场景
```

## 运行说明

1. 打开 Unity 项目
2. 打开 `Assets/_Project/Scenes/Test.unity` 场景
3. 确认场景中的 `InventoryGridUI` 已挂载，并拖入 `InventoryConfigSO` 资产（`Assets/_Project/Config/InventoryConfigSO.asset`）
4. 运行场景即可生成 24×10 格子

## 命名空间

- `SurvivalPawnShop.Core` — 基础设施
- `SurvivalPawnShop.Config` — 配置档案
- `SurvivalPawnShop.Model` — 纯数据模型
- `SurvivalPawnShop.UI` / `SurvivalPawnShop.Utils` — 表现层与工具

## 备注

项目已上传至 GitHub：https://github.com/aaazzzjjj/survival-pawn-shop
