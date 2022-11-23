using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFx.ShortId
{
    /// <summary>
    /// 提供对shortid库的编程配
    /// </summary>
    public class GenerationOptions
    {
        /// <summary>
        /// 确定是否在生成ID时使用数字
        /// Default: true.
        /// </summary>
        public bool UseNumbers { get; set; } = true;

        /// <summary>
        /// 确定是否在生成id时使用特殊字符
        /// Default: false.
        /// </summary>
        public bool UseSpecialCharacters { get; set; } = false;

        /// <summary>
        /// 确定生成的ID的长度
        /// Default: 7到15之间的随机长度.
        /// </summary>
        public int Length { get; set; } =
            RandomUtils.GenerateNumberInRange(Constants.MinimumAutoLength, Constants.MaximumAutoLength);
    }

}
