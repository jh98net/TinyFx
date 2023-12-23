using Microsoft.AspNetCore.DataProtection.KeyManagement;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Collections;
using TinyFx.Configuration;
using TinyFx.Data.SqlSugar;
using TinyFx.DbCaching.Caching;
using TinyFx.Extensions.StackExchangeRedis;
using TinyFx.Security;

namespace TinyFx.DbCaching
{
    internal class PageDataProvider
    {
        private const int DATA_PAGE_SIZE = 1000;
        private string _configId { get; }
        private string _tableName { get; }
        public string ConnectionStringName { get; }
        public PageDataProvider(string configId, string tableName, string connectionStringName = null)
        {
            _configId = configId;
            _tableName = tableName;
            ConnectionStringName = connectionStringName;
        }

        public async Task<DbTableRedisData> SetRedisValues()
        {
            var listDCache = new DbCacheListDCache(ConnectionStringName);
            var dataDCache = new DbCacheDataDCache(_configId, _tableName, ConnectionStringName);
            var data = await GetDbTableData();

            var key = DbCachingUtil.GetCacheKey(_configId, _tableName);
            var listDo1 = await listDCache.GetAsync(key);
            // 避免并发
            using var redLock = await RedisUtil.LockAsync($"DbCacheDataDCache:{key}", 180);
            if (!redLock.IsLocked)
            {
                redLock.Release();
                throw new Exception($"DbCacheDataDCache获取缓存锁超时。key:{key}");
            }
            var listDo2 = await listDCache.GetAsync(key);
            if (listDo1.Value?.UpdateDate != listDo2.Value?.UpdateDate) //已更新
                return data;

            // 装载数据
            int i = 0;
            foreach (var pageString in data.PageList)
            {
                await dataDCache.SetAsync($"{i++}", pageString);
                await Task.Delay(100);
            }
            await listDCache.SetAsync(key, new DbCacheListDO()
            {
                ConfigId = _configId,
                TableName = _tableName,
                PageCount = data.PageCount,
                DataHash = data.DataHash,
                UpdateDate = DateTime.Now.ToFormatString()
            });
            return data;
        }

        public async Task<DbTableRedisData> GetRedisValues()
        {
            var listDCache = new DbCacheListDCache(ConnectionStringName);
            var dataDCache = new DbCacheDataDCache(_configId, _tableName, ConnectionStringName);
            var key = DbCachingUtil.GetCacheKey(_configId, _tableName);
            var listDo = await listDCache.GetAsync(key);
            if (!listDo.HasValue || !await dataDCache.KeyExistsAsync())
            {
                return await SetRedisValues();
            }

            var ret = new DbTableRedisData()
            {
                ConfigId = listDo.Value.ConfigId,
                TableName = listDo.Value.TableName,
                PageCount = listDo.Value.PageCount,
                DataHash = listDo.Value.DataHash,
                UpdateDate = listDo.Value.UpdateDate,
            };
            for (int i = 1; i <= listDo.Value.PageCount; i++)
            {
                var pageString = await dataDCache.GetOrExceptionAsync(i.ToString());
                ret.PageList.Add(pageString);
                await Task.Delay(100);
            }
            return ret;
        }
        private async Task<DbTableRedisData> GetDbTableData()
        {
            var ret = new DbTableRedisData()
            {
                ConfigId = _configId,
                TableName = _tableName,
            };
            var totalList = await DbUtil.GetDbById(_configId).Queryable<object>()
                .AS(_tableName).ToListAsync();
            var pageList = totalList.ToPage(DATA_PAGE_SIZE);
            ret.PageCount = pageList.Count;
            string dataString = null;
            foreach (var item in pageList)
            {
                var pageString = SerializerUtil.SerializeJsonNet(item);
                ret.PageList.Add(pageString);
                dataString += pageString;
            }
            ret.DataHash = SecurityUtil.MD5Hash(dataString);
            return ret;
        }
    }
    public class DbTableRedisData
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
        public int PageCount { get; set; }
        public string DataHash { get; set; }
        public string UpdateDate { get; set; }

        public List<string> PageList { get; set; } = new();
    }
}
