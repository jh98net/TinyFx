using Microsoft.AspNetCore.DataProtection.KeyManagement;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Data.SqlSugar;

namespace TinyFx.DbCaching
{
    internal class PageDataProvider
    {
        private const int DATA_PAGE_SIZE = 1000;
        private string _configId { get; }
        private string _tableName { get; }
        public PageDataProvider(string configId, string tableName)
        {
            _configId = configId;
            _tableName = tableName;
        }

        public async Task<int> GetPageCount()
        {
            var rowCount = await DbUtil.GetDb(_configId).Queryable<object>()
                          .AS(_tableName).CountAsync();
            return (int)TinyFxUtil.GetPageCount(rowCount, DATA_PAGE_SIZE);
        }

        public async Task<string> GetPageData(int pageIndex)
        {
            var list = await DbUtil.GetDb(_configId).Queryable<object>()
                .AS(_tableName).ToPageListAsync(pageIndex, DATA_PAGE_SIZE);
            return SerializerUtil.SerializeJson(list);
        }

        public async Task SetRedisValues()
        {
            var dcache = DbCacheDataDCache.Create();
            var pageCount = await GetPageCount();
            var key = DbCachingUtil.GetCacheKey(_configId, _tableName);
            await dcache.SetAsync($"{key}|0", pageCount.ToString());
            for (int i = 1; i <= pageCount; i++)
            {
                var list = await GetPageData(i);
                await dcache.SetAsync($"{key}|{i}", list);
                await Task.Delay(200);
            }
        }

        public async Task<List<string>> GetRedisValues()
        {
            var ret = new List<string>();
            var dcache = DbCacheDataDCache.Create();
            var key = DbCachingUtil.GetCacheKey(_configId, _tableName);
            var pageCount = await dcache.GetOrLoadAsync($"{key}|0");
            if (!pageCount.HasValue)
                throw new Exception($"DbCacheDataDCache缓存没有值。key:{key}");
            for (int i = 1; i <= pageCount.Value.ToInt32(); i++)
            {
                var pageKey = $"{key}|{i}";
                var pageData = await dcache.GetOrLoadAsync(pageKey);
                ret.Add(pageData.Value);
                await Task.Delay(200);
            }
            return ret;
        }

        public async Task<List<TEntity>> GetRedisValues<TEntity>() where TEntity : class
        {
            List<TEntity> ret = new List<TEntity>();
            var list = await GetRedisValues();
            foreach (var item in list)
            {
                var value = SerializerUtil.DeserializeJson<List<TEntity>>(item);
                ret.AddRange(value);
            }
            return ret;
        }
    }
}
