using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleCore.Common.Cache
{
    public class Caching :ICaching
    {
        public IDistributedCache Cache { get; }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        // 儲存快取鍵集合（模擬 key 管理，可擴充為 DB 或 Redis Set）
        private readonly List<string> _cacheKeys = new();

        public Caching(IDistributedCache cache)
        {
            Cache = cache;
        }

        private static string ToJson<T>(T value) => JsonSerializer.Serialize(value);
        private static T? FromJson<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public void Set<T>(string cacheKey, T value, TimeSpan? expire = null)
        {
            var json = ToJson(value);
            var options = new DistributedCacheEntryOptions();
            if (expire.HasValue)
                options.SetAbsoluteExpiration(expire.Value);

            Cache.SetString(cacheKey, json, options);
            AddCacheKey(cacheKey);
        }

        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? expire = null)
        {
            var json = ToJson(value);
            var options = new DistributedCacheEntryOptions();
            if (expire.HasValue)
                options.SetAbsoluteExpiration(expire.Value);

            await Cache.SetStringAsync(cacheKey, json, options);
            AddCacheKey(cacheKey);
        }

        public T Get<T>(string cacheKey)
        {
            var json = Cache.GetString(cacheKey);
            return json == null ? default! : FromJson<T>(json)!;
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var json = await Cache.GetStringAsync(cacheKey);
            return json == null ? default! : FromJson<T>(json)!;
        }

        public bool Exists(string cacheKey) => Cache.Get(cacheKey) != null;

        public async Task<bool> ExistsAsync(string cacheKey)
        {
            var value = await Cache.GetAsync(cacheKey);
            return value != null;
        }

        public void Remove(string cacheKey)
        {
            Cache.Remove(cacheKey);
            _cacheKeys.Remove(cacheKey);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await Cache.RemoveAsync(cacheKey);
            _cacheKeys.Remove(cacheKey);
        }

        public void DelByPattern(string prefix)
        {
            var matched = _cacheKeys.Where(k => k.StartsWith(prefix)).ToList();
            foreach (var key in matched)
                Remove(key);
        }

        public async Task DelByPatternAsync(string prefix)
        {
            var matched = _cacheKeys.Where(k => k.StartsWith(prefix)).ToList();
            foreach (var key in matched)
                await RemoveAsync(key);
        }

        public string GetString(string cacheKey) => Cache.GetString(cacheKey)!;

        public Task<string> GetStringAsync(string cacheKey) => Cache.GetStringAsync(cacheKey)!;

        public Task SetStringAsync(string cacheKey, string value) =>
            Cache.SetStringAsync(cacheKey, value);

        public Task SetStringAsync(string cacheKey, string value, TimeSpan expire) =>
            Cache.SetStringAsync(cacheKey, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expire
            });

        public async Task<List<T>> GetListAsync<T>(string key)
        {
            var json = await Cache.GetStringAsync(key);
            return json == null ? new List<T>() : FromJson<List<T>>(json)!;
        }

        public async Task<List<T>> GetListAsync<T>(List<string> keys)
        {
            var result = new List<T>();
            foreach (var key in keys)
            {
                var list = await GetListAsync<T>(key);
                result.AddRange(list);
            }
            return result;
        }

        public async Task SetListAsync<T>(string key, List<T> list, TimeSpan? expiry = null)
        {
            var json = ToJson(list);
            var options = new DistributedCacheEntryOptions();
            if (expiry.HasValue)
                options.SetAbsoluteExpiration(expiry.Value);

            await Cache.SetStringAsync(key, json, options);
            AddCacheKey(key);
        }

        public async Task RemoveListItemAsync<T>(string key, Func<T, bool> predicate)
        {
            var list = await GetListAsync<T>(key);
            var newList = list.Where(x => !predicate(x)).ToList();
            await SetListAsync(key, newList);
        }

        // 快取鍵管理功能
        private void AddCacheKey(string cacheKey)
        {
            if (!_cacheKeys.Contains(cacheKey))
                _cacheKeys.Add(cacheKey);
        }
    }
}
