using System.Linq;
using Irony.Parsing;

namespace DotNet.MockDb.Constraints
{
    public class ColumnConstraint: IConstraint
    {
        private readonly string _column;
        private readonly string _table;
        private readonly string _schema;
        
        public ColumnConstraint(string column, string table=null, string schema=null)
        {
            _column = column;
            _table = table;
            _schema = schema;
        }
         
        public bool AppliesTo(ParseTreeNode parseTreeNode)
        {
            return parseTreeNode.ChildNodes.Any(columnItemList => columnItemList.Term.Name == "columnItemList"
                && columnItemList.ChildNodes.Any(columnItem => columnItem.Term.Name == "columnItem"
                    && columnItem.ChildNodes.Any(columnSource => columnSource.Term.Name == "columnSource"
                       && columnSource.ChildNodes.Any(Id => Id.Term.Name == "Id"
                           && Id.ChildNodes.ElementAt(Id.ChildNodes.Count-1).Token.Value as string == _column
                           && (_table == null || Id.ChildNodes.ElementAt(Id.ChildNodes.Count-2).Token.Value as string == _table)
                           && (_schema == null || Id.ChildNodes.ElementAt(Id.ChildNodes.Count-3).Token.Value as string == _schema)))));
        }
    }
}