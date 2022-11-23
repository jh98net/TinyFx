using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TinyFx.Common
{
    /// <summary>
    /// 字符串模板类，用于模板字符串替换，变量格式如: {{key}}
    /// </summary>
    public class StringTemplate
    {
        private string Content { get; set; }

        /// <summary>
        /// 模版引擎
        /// </summary>
        /// <param name="content"></param>
        public StringTemplate(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static StringTemplate Create(string content)
        {
            return new StringTemplate(content);
        }

        /// <summary>
        /// 设置变量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public StringTemplate Set(string key, string value)
        {
            Content = Content.Replace("{{" + key + "}}", value);
            return this;
        }

        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <param name="check">是否检查未使用的模板变量</param>
        /// <returns></returns>
        public string Render(bool check = true)
        {
            var mc = Regex.Matches(Content, @"\{\{.+?\}\}");
            if (check)
            {
                foreach (Match m in mc)
                {
                    throw new ArgumentException($"模版变量{m.Value}未被使用");
                }
            }

            return Content;
        }
    }
}
