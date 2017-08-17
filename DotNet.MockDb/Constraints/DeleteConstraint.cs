using System.Collections.Generic;
using System.Linq;
using Irony.Parsing;

namespace DotNet.MockDb.Constraints
{
    public class DeleteConstraint : IConstraint
    {
        private FromConstraint _fromConstraint;
		private WhereConstraint _whereConstraint;

		public DeleteConstraint()
        {
        }

		public DeleteConstraint From(string table, string schema = null)
        {
            _fromConstraint = new FromConstraint(table, schema);
            return this;
        }

		public DeleteConstraint Where()
		{
			_whereConstraint = new WhereConstraint();
			return this;
		}

        public bool AppliesTo(ParseTreeNode parseTreeNode)
        {
            if (parseTreeNode == null || parseTreeNode.Term.Name != "stmtList")
                return false;

			return parseTreeNode.ChildNodes.Any(statement => IsDeleteStatement(statement)
               && AppliesToFromConstraint(statement)
				&& AppliesToWhereConstraint(statement));
        }

        private static bool IsDeleteStatement(ParseTreeNode parseTreeNode)
        {
            return parseTreeNode.Term.Name == "deleteStmt";
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

			return parseTreeNode.ChildNodes.Any(from => from.Term.Name == "whereClauseOpt"
				&& _whereConstraint.AppliesTo(from));
		}
    }
}