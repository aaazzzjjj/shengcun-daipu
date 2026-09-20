using System;
using System.Collections.Generic;

namespace SurvivalPawnShop.Core
{
    /// <summary>
    /// 轻量服务定位器：统一注册与获取系统服务，替代分散的 MonoBehaviour 单例。
    /// 所有 System（InventorySystem / TradeSystem / ...）在 Awake 里 Register，其它模块通过 Get 获取。
    /// </summary>
    public static class GameServices
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        /// <summary>注册一个服务实例（按类型 key）。重复注册会覆盖并告警。</summary>
        public static void Register<T>(T service) where T : class
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service), $"[GameServices] 不能注册 null 服务 {typeof(T).Name}。");
            }

            var type = typeof(T);
            if (services.ContainsKey(type))
            {
                UnityEngine.Debug.LogWarning($"[GameServices] 服务 {type.Name} 已存在，被覆盖。");
                services[type] = service;
            }
            else
            {
                services.Add(type, service);
            }
        }

        /// <summary>获取服务；未注册时抛异常。</summary>
        public static T Get<T>() where T : class
        {
            if (services.TryGetValue(typeof(T), out var service))
            {
                return service as T;
            }
            throw new InvalidOperationException($"[GameServices] 服务 {typeof(T).Name} 未注册。");
        }

        /// <summary>尝试获取服务；未注册返回 false。</summary>
        public static bool TryGet<T>(out T service) where T : class
        {
            if (services.TryGetValue(typeof(T), out var obj))
            {
                service = obj as T;
                return true;
            }
            service = null;
            return false;
        }

        /// <summary>清空所有服务（场景重载 / 退出时调用）。</summary>
        public static void Clear()
        {
            services.Clear();
        }
    }
}
