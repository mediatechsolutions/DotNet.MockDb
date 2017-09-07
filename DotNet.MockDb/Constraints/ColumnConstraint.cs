using System.Linq;
using Irony.Parsing;
using DotNet.MockDb.Constraints.Expressions;

namespace DotNet.MockDb.Constraints
{
    public class ColumnConstraint: IConstraint
    {
        private readonly ColumnIdExpressionConstraint _columnIdExpressionConstraint;
        
        public ColumnConstraint(string column, string table=null, string schema=null)
        {
            _columnIdExpressionConstraint = new ColumnIdExpressionConstraint(column, table, schema);
        }
         
        public bool AppliesTo(ParseTreeNode parseTreeNode)
        {
            return parseTreeNode.ChildNodes.Any(columnItemList => columnItemList.Term.Name == "columnItemList"
                && columnItemList.ChildNodes.Any(columnItem => columnItem.Term.Name == "columnItem"
                    && columnItem.ChildNodes.Any(columnSource => columnSource.Term.Name == "columnSource"
                       && columnSource.ChildNodes.Any(Id => _columnIdExpressionConstraint.AppliesTo(Id)))));
        }
    }
}