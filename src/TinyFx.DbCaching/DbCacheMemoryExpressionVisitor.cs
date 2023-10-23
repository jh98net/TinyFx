using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TinyFx.DbCaching
{
    internal class DbCacheMemoryExpressionVisitor: ExpressionVisitor
    {
        private List<string> DictKeys = new();
        private List<string> ValueKeys = new();

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            DictKeys.Add(node.Member.Name);
            return base.VisitMemberAssignment(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            ValueKeys.Add(Convert.ToString(node.Value));
            return base.VisitConstant(node);
        }
        public (string DictKey, string ValueKey) GetKeys()
        {
            return (string.Join('|', DictKeys), string.Join('|', ValueKeys));
        }
    }
}
