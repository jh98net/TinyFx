using Newtonsoft.Json;
using System.Text;


namespace TinyFx.OAuth
{
    public static class DictionaryExtensions
    {

        /// <summary>
        /// 把字典集合拼接成&符号链接的字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string SpellParams(this Dictionary<string, object> dicParams)
        {
            StringBuilder builder = new StringBuilder();
            if (dicParams.Count > 0)
            {
                builder.Append("");
                int i = 0;
                foreach (KeyValuePair<string, object> item in dicParams)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, Convert.ToString(item.Value));
                    i++;
                }
            }
            return builder.ToString();
        }
 
        /// <param name="urldata"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this string urldata)
        {
            //var uri = new Uri(url);

            //var collection = HttpUtility.ParseQueryString(url);

            Dictionary<string, string> dic = urldata.Substring(1)
                //2.通过&划分各个参数
                .Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                //3.通过=划分参数key和value,且保证只分割第一个=字符
                .Select(param => param.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
                //4.通过相同的参数key进行分组
                .GroupBy(part => part[0], part => part.Length > 1 ? part[1] : string.Empty)
                //5.将相同key的value以,拼接
                .ToDictionary(group => group.Key, group => string.Join(",", group));
            return dic;
        }
        //object的字典集合
        public static string GetString(this Dictionary<string, object> dic, string key)
        {
            if (dic == null)
                return "";
            if (dic.ContainsKey(key))
            {
                return Convert.ToString(dic[key]);
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 获取参数Int32类型
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static int GetInt32(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToInt32(paramValue);
                }
                catch
                {
                    return -1;
                }
            }
            return -1;
        }

        /// <summary>
        /// 获取参数Int64类型
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static long GetLong(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToInt64(paramValue);
                }
                catch 
                {
                    return -1;
                }
            }
            return -1;
        }


        /// <summary>
        /// 获取参数Bool类型
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static bool GetBool(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToBoolean(paramValue);
                }
                catch 
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取参数字符串并且转换为字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetJSONObject(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return paramValue.ParseObject();
                }
                catch 
                {
                    return new Dictionary<string, object>();
                }
            }
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取参数字符串并且转换为字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> GetJSONArray(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return paramValue.ParseListObject();
                }
                catch 
                {
                    return new List<Dictionary<string, object>>();
                }
            }
            return new List<Dictionary<string, object>>();
        }
        //json字符串转换为字典集合
        public static List<Dictionary<string, object>> ParseListObject(this string jsonStr)
        {
            var retDic = new List<Dictionary<string, object>>();
            if (!string.IsNullOrWhiteSpace(jsonStr))
            {
                try
                {
                    retDic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonStr);
                }
                catch
                {
                }
            }
            return retDic;
        }
        //json字符串转换为字典集合
        public static Dictionary<string, object> ParseObject(this string jsonStr)
        {
            var retDic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(jsonStr))
            {
                try
                {
                    retDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
                }
                catch 
                {
                }
            }
            return retDic;
        }
        ///// <summary>
        ///// 获取参数字符串类型
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="paramName"></param>
        ///// <returns></returns>
        /*public static string getString(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.getString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToString(paramValue);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }*/

        /// <summary>
        /// 获取参数日期类型
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static DateTime? GetParamDateTime(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToDateTime(paramValue);
                }
                catch 
                {
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// 获取参数double类型
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static double? GetParamDouble(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToDouble(paramValue);
                }
                catch 
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取参数Decimal类型
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static Decimal? GetParamDecimal(this Dictionary<string, object> request, string paramName)
        {
            var paramValue = request.GetString(paramName);
            if (!string.IsNullOrWhiteSpace(paramValue))
            {
                try
                {
                    return Convert.ToDecimal(paramValue);
                }
                catch 
                {
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// 字典排序,asc排序
        /// </summary>
        /// <param name="dic">需排序的字典对象</param>
        /// <param name="isAsc">true=正序，反之倒序</param>
        public static Dictionary<string, object> Sort(this Dictionary<string, object> dic, bool isAsc = true)
        {
            Dictionary<string, object> rdic = new Dictionary<string, object>();
            if (dic.Count > 0)
            {
                List<KeyValuePair<string, object>> lst = new List<KeyValuePair<string, object>>(dic);
                lst.Sort(delegate (KeyValuePair<string, object> s1, KeyValuePair<string, object> s2)
                {
                    if (isAsc)
                    {
                        return String.CompareOrdinal(s1.Key, s2.Key);
                    }
                    else
                    {
                        return String.CompareOrdinal(s2.Key, s1.Key);
                    }
                });

                foreach (KeyValuePair<string, object> kvp in lst)
                    rdic.Add(kvp.Key, kvp.Value);
            }
            return rdic;
        }
    }
}
