using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Cache
{
    public interface ICaching
    {
        public IDistributedCache Cache { get; }

        /// <summary>
        /// 設定快取項目，指定快取鍵、值與可選的過期時間。
        /// </summary>
        /// <typeparam name="T">快取值的型別。</typeparam>
        /// <param name="cacheKey">快取鍵。</param>
        /// <param name="value">快取值。</param>
        /// <param name="expire">過期時間（可選）。</param>
        void Set<T>(string cacheKey, T value, TimeSpan? expire = null);

        /// <summary>
        /// 非同步設定快取項目。
        /// </summary>
        /// <typeparam name="T">快取值的型別。</typeparam>
        /// <param name="cacheKey">快取鍵。</param>
        /// <param name="value">快取值。</param>
        /// <param name="expire">過期時間（可選）。</param>
        Task SetAsync<T>(string cacheKey, T value, TimeSpan? expire = null);

        /// <summary>
        /// 取得快取值，若不存在則回傳預設值。
        /// </summary>
        /// <typeparam name="T">快取值的型別。</typeparam>
        /// <param name="cacheKey">快取鍵。</param>
        /// <returns>快取值。</returns>
        T Get<T>(string cacheKey);

        /// <summary>
        /// 非同步取得快取值，若不存在則回傳預設值。
        /// </summary>
        /// <typeparam name="T">快取值的型別。</typeparam>
        /// <param name="cacheKey">快取鍵。</param>
        /// <returns>快取值。</returns>
        Task<T> GetAsync<T>(string cacheKey);

        /// <summary>
        /// 檢查快取是否存在指定鍵。
        /// </summary>
        /// <param name="cacheKey">快取鍵。</param>
        /// <returns>是否存在。</returns>
        bool Exists(string cacheKey);

        /// <summary>
        /// 非同步檢查快取是否存在指定鍵。
        /// </summary>
        /// <param name="cacheKey">快取鍵。</param>
        /// <returns>是否存在。</returns>
        Task<bool> ExistsAsync(string cacheKey);

        /// <summary>
        /// 刪除指定快取鍵。
        /// </summary>
        /// <param name="cacheKey">快取鍵。</param>
        void Remove(string cacheKey);

        /// <summary>
        /// 非同步刪除指定快取鍵。
        /// </summary>
        /// <param name="cacheKey">快取鍵。</param>
        Task RemoveAsync(string cacheKey);

        /// <summary>
        /// 批次刪除具有指定前綴的快取項目（僅適用於記錄快取鍵的實作）。
        /// </summary>
        /// <param name="prefix">快取鍵前綴。</param>
        void DelByPattern(string prefix);

        /// <summary>
        /// 非同步批次刪除具有指定前綴的快取項目。
        /// </summary>
        /// <param name="prefix">快取鍵前綴。</param>
        Task DelByPatternAsync(string prefix);

        /// <summary>
        /// 取得字串類型的快取值。
        /// </summary>
        string GetString(string cacheKey);

        /// <summary>
        /// 非同步取得字串類型的快取值。
        /// </summary>
        Task<string> GetStringAsync(string cacheKey);

        /// <summary>
        /// 非同步設定字串快取。
        /// </summary>
        Task SetStringAsync(string cacheKey, string value);

        /// <summary>
        /// 非同步設定字串快取（含過期時間）。
        /// </summary>
        Task SetStringAsync(string cacheKey, string value, TimeSpan expire);

        /// <summary>
        /// 非同步取得指定 key 的 List 資料。
        /// </summary>
        Task<List<T>> GetListAsync<T>(string key);

        /// <summary>
        /// 非同步取得多個 key 對應的 List 資料。
        /// </summary>
        Task<List<T>> GetListAsync<T>(List<string> key);

        /// <summary>
        /// 非同步設定 List 資料至快取。
        /// </summary>
        Task SetListAsync<T>(string key, List<T> list, TimeSpan? expiry = null);

        /// <summary>
        /// 非同步移除 List 中符合條件的項目。
        /// </summary>
        Task RemoveListItemAsync<T>(string key, Func<T, bool> predicate);
    }
}
