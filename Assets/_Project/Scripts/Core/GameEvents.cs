using System;

namespace SurvivalPawnShop.Core
{
    /// <summary>
    /// 静态事件中心：只用于跨模块广播。模块内部调用直接走方法，不走事件。
    /// </summary>
    public static class GameEvents
    {
        /// <summary>余额变动（参数：新余额）。</summary>
        public static event Action<int> OnMoneyChanged;

        /// <summary>仓库库存变动（增删物品后）。</summary>
        public static event Action OnInventoryChanged;

        /// <summary>一笔交易完成。</summary>
        public static event Action OnTradeCompleted;

        /// <summary>一天结束，弹出日报。</summary>
        public static event Action OnDayEnd;

        /// <summary>一周开始（报童上门 / 行情刷新）。</summary>
        public static event Action OnWeekStart;

        public static void RaiseMoneyChanged(int newMoney) => OnMoneyChanged?.Invoke(newMoney);
        public static void RaiseInventoryChanged() => OnInventoryChanged?.Invoke();
        public static void RaiseTradeCompleted() => OnTradeCompleted?.Invoke();
        public static void RaiseDayEnd() => OnDayEnd?.Invoke();
        public static void RaiseWeekStart() => OnWeekStart?.Invoke();
    }
}
