using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DataSplit.Common
{
    public enum HandlerMode
    {
        Unknow = 0,
        /// <summary>
        /// 1-删除 
        ///     保留天数：MoveKeepMode + MoveKeepValue
        /// </summary>
        Delete = 1,
        /// <summary>
        /// 2-迁移
        ///     保留天数：MoveKeepMode + MoveKeepValue
        ///     目标表名: MoveTableMode + MoveTableValue
        /// </summary>
        Move = 2,
        /// <summary>
        /// 3-分表(按最大行数分)
        /// </summary>
        SplitMaxRows = 3
    }

}
