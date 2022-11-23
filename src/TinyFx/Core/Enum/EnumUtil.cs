using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Concurrent;

namespace TinyFx
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumUtil
    {
        #region EnumInfo

        /// <summary>
        /// 枚举描述缓存
        /// </summary>
        private static readonly ConcurrentDictionary<string, EnumInfo> _descsCache = new ConcurrentDictionary<string, EnumInfo>();

        /// <summary>
        /// 获取枚举类型的描述信息
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static EnumInfo GetInfo(Type enumType)
        {
            EnumInfo ret = null;
            if (!_descsCache.TryGetValue(enumType.FullName, out ret))
            {
                ret = new EnumInfo(enumType);
                _descsCache.AddOrUpdate(enumType.FullName, ret, (key, value) => ret);
            }
            return ret;
        }

        /// <summary>
        /// 获取枚举类型的描述信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EnumInfo GetInfo<T>() => GetInfo(typeof(T));

        /// <summary>
        /// 获取枚举项描述信息
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumItem GetItemInfo(Type enumType, object value) => GetInfo(enumType).GetItem(value);

        /// <summary>
        /// 获取枚举项描述信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumItem GetItemInfo<T>(object value) => GetItemInfo(typeof(T), value);

        /// <summary>
        /// 获取枚举项描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumItem GetItemInfo(this Enum value) => GetItemInfo(value.GetType(), value);

        /// <summary>
        /// 获取枚举的Description
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetDescription(Type enumType) => GetInfo(enumType).Description;

        /// <summary>
        /// 获取枚举的Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDescription<T>() => GetInfo<T>().Description;

        /// <summary>
        /// 获取枚举项的Description
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetItemDescription(Type enumType, object value) => GetItemInfo(enumType, value).Description;

        /// <summary>
        /// 获取枚举项的Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetItemDescription<T>(object value) => GetItemInfo<T>(value).Description;

        /// <summary>
        /// 获取枚举项的Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetItemDescription(this Enum value)
            => GetItemInfo(value.GetType(), value).Description;

        #endregion // EnumInfo

        /// <summary>
        /// 判断flags枚举类型variable是否包含value
        /// </summary>
        /// <param name="variable">要验证的Enum值</param>
        /// <param name="value">判断value是否包含在variable中</param>
        /// <returns></returns>
        public static bool HasFlag(Enum variable, Enum value)
        {
            var valueNum = Convert.ToUInt64(value);
            return (Convert.ToUInt64(variable) & valueNum) == valueNum;
        }

        #region ToEnum
        /// <summary>
        /// 将int转换成Enum，失败抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value)
            where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new Exception($"int数值{value}在枚举{typeof(T).FullName}中没有定义");
            return (T)(object)value;
        }
        /// <summary>
        /// 将int转换成Enum，失败返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? ToEnumN<T>(this int value)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
                return null;
            return (T)(object)value;
        }

        /// <summary>
        /// 将int转换成Enum，失败转换成默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value, T defaultValue)
            where T : Enum
            => Enum.IsDefined(typeof(T), value) ? (T)(object)value : defaultValue;


        /// <summary>
        /// 将string转换成Enum，失败抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                    throw new Exception($"string数值{value}在枚举{typeof(T).FullName}中没有定义");
            }
        }

        /// <summary>
        /// 将string转换成Enum，失败返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? ToEnumN<T>(this string value)
            where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将string转换成Enum，失败转换成默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue)
                 where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return defaultValue;
            }
        }
        #endregion
    }
}
