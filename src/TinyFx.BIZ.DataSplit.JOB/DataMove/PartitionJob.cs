using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.BIZ.DataSplit.DAL;
using TinyFx.Data.SqlSugar;
using TinyFx.Logging;
using TinyFx.Text;

namespace TinyFx.BIZ.DataSplit.JOB.DataMove
{
    internal class PartitionJob : BaseDataMoveJob
    {
        public PartitionJob(Ss_split_tableEO item, string defaultConfigId, DateTime execTime)
            : base(item, defaultConfigId, execTime)
        {
            if ((HandleMode)item.HandleMode != HandleMode.Partition)
                throw new Exception($"{GetType().FullName}时HandleMode必须是Partition");
        }

        protected override Task ExecuteJob()
        {
            throw new NotImplementedException();
        }

        private string GenPartitionSql()
        {
            var sb = new StringBuilder();
            sb.Append($"alter table `{_item.TableName}` partition by ");
            switch ((PartitionMethod)_item.PartitionMethod)
            {
                case PartitionMethod.Range:
                    sb.Append($"range({_item.PartitionExpr})(");
                    var vs1 = _item.PartitionValue.Split('|');
                    for (int i = 0; i < vs1.Length; i++)
                    {
                        sb.AppendLine($" partition p{i} values less than({vs1[i]}),");
                    }
                    sb.Append(")");
                    break;
                case PartitionMethod.List:
                    sb.Append($"list({_item.PartitionExpr})(");
                    var vs2 = _item.PartitionValue.Split('|');
                    for (int i = 0; i < vs2.Length; i++)
                    {
                        sb.AppendLine($" partition p{i} values in({vs2[i]}),");
                    }
                    sb.Append(")");
                    break;
                case PartitionMethod.Hash:
                    sb.Append($"hash({_item.PartitionExpr}) partition {_item.PartitionValue}");
                    break;
                case PartitionMethod.Key:
                    sb.Append($"key({_item.PartitionExpr}) partition {_item.PartitionValue}");
                    break;
                case PartitionMethod.RangeColumns:
                    sb.Append($"range columns({_item.PartitionExpr})(");
                    var vs3 = _item.PartitionValue.Split('|');
                    for (int i = 0; i < vs3.Length; i++)
                    {
                        sb.AppendLine($" partition p{i} values less than({vs3[i]}),");
                    }
                    sb.Append(")");
                    break; ;
                case PartitionMethod.ListColumns:
                    sb.Append($"list columns({_item.PartitionExpr})(");
                    var vs4 = _item.PartitionValue.Split('|');
                    for (int i = 0; i < vs4.Length; i++)
                    {
                        sb.AppendLine($" partition p{i} values less than({vs4[i]}),");
                    }
                    sb.Append(")");
                    break;
                default:
                    throw new Exception("不支持的PartitionMethod");
            }
            return sb.ToString();
        }
    }
}
