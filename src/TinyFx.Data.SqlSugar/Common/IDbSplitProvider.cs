﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.Data.SqlSugar
{
    public interface IDbSplitProvider
    {
        /// <summary>
        /// 分库(根据类型和分库标识)
        ///     1) 按上下文分库，如：登录用户的集团ID
        ///     2) 按指定值分库，如splitDbKey参数传入合作商ID
        ///     3) 按指定表和值分库，即T+splitDbKey
        /// </summary>
        /// <typeparam name="T">如果是object，则表示所有表按约定的值分库</typeparam>
        /// <param name="splitDbKey"></param>
        /// <returns></returns>
        string SplitDb<T>(object splitDbKey = null);

        /// <summary>
        /// 分表(根据实体类型和表数据)
        /// </summary>
        /// <returns></returns>
        ISplitTableService SplitTable();
    }
    public class DefaultSplitProvider : IDbSplitProvider
    {
        public string SplitDb<T>(object splitDbKey = null)
        {
            return null;
        }

        public ISplitTableService SplitTable()
        {
            return null;
        }
    }
}
