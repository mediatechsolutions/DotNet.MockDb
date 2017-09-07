using Irony.Parsing;
using System.Linq;

namespace DotNet.MockDb.Constraints.Expressions
{
    public class ColumnIdExpressionConstraint : IExpressionConstraint
	{
        private readonly string _column;
        private readonly string _table;
        private readonly string _schema;

        public ColumnIdExpressionConstraint(string column, string table = null, string schema = null)
		{
            _column = column;
            _table = table;
            _schema = schema;
        }

		public bool AppliesTo(ParseTreeNode parseTreeNode) 
		{
            return parseTreeNode.Term.Name == "Id"
                           && parseTreeNode.ChildNodes.ElementAt(parseTreeNode.ChildNodes.Count - 1).Token.Value as string == _column
                           && (_table == null || parseTreeNode.ChildNodes.ElementAt(parseTreeNode.ChildNodes.Count - 2).Token.Value as string == _table)
                           && (_schema == null || parseTreeNode.ChildNodes.ElementAt(parseTreeNode.ChildNodes.Count - 3).Token.Value as string == _schema);
        }
	}
}

