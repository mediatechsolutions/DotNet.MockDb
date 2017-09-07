using System.Collections.Generic;
using System.Linq;
using Irony.Parsing;
using DotNet.MockDb.Constraints.Expressions;

namespace DotNet.MockDb.Constraints
{
    public class SelectConstraint : IConstraint
    {
        private readonly List<ColumnConstraint> _columnConstraints;
        private FromConstraint _fromConstraint;
		private WhereConstraint _whereConstraint;

        public SelectConstraint()
        {
            _columnConstraints = new List<ColumnConstraint>();
        }

        public SelectConstraint WithColumn(string column, string table=null, string schema = null)
        {
            _columnConstraints.Add(new ColumnConstraint(column, table, schema));
            return this;
        }

        public SelectConstraint From(string table, string schema = null)
        {
            _fromConstraint = new FromConstraint(table, schema);
            return this;
        }

		public SelectConstraint Where(IExpressionConstraint expression)
		{
			_whereConstraint = new WhereConstraint(expression);
			return this;
		}

        public bool AppliesTo(ParseTreeNode parseTreeNode)
        {
            if (parseTreeNode == null || parseTreeNode.Term.Name != "stmtList")
                return false;

            return parseTreeNode.ChildNodes.Any(statement => IsSelectStatement(statement)
                && AppliesToAllTheColumnConstraints(statement)
                && AppliesToFromConstraint(statement)
				&& AppliesToWhereConstraint(statement));
        }

        private static bool IsSelectStatement(ParseTreeNode parseTreeNode)
        {
            return parseTreeNode.Term.Name == "selectStmt";
        }

        private bool AppliesToAllTheColumnConstraints(ParseTreeNode parseTreeNode)
        {
            if (!_columnConstraints.Any())
                return true;
            
            return parseTreeNode.ChildNodes.Any(selectList => selectList.Term.Name == "selList"
            	&& _columnConstraints.All(constraint => constraint.AppliesTo(selectList)));
        }

        private bool AppliesToFromConstraint(ParseTreeNode parseTreeNode)
        {
            if (_fromConstraint == null)
                return true;

            return parseTreeNode.ChildNodes.Any(from => from.Term.Name == "fromClauseOpt"
            	&& _fromConstraint.AppliesTo(from));
        }

		private bool AppliesToWhereConstraint(ParseTreeNode parseTreeNode)
		{
			if (_whereConstraint == null)
				return true;

			return parseTreeNode.ChildNodes.Any(were => were.Term.Name == "whereClauseOpt"
				&& _whereConstraint.AppliesTo(were));
		}
    }
}