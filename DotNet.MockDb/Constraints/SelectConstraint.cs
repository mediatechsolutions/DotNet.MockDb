using System.Collections.Generic;
using System.Linq;
using Irony.Parsing;

namespace DotNet.MockDb.Constraints
{
    public class SelectConstraint : IConstraint
    {
        private readonly List<ColumnConstraint> _columnConstraints;
        private FromConstraint _fromConstraint;

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

        public bool AppliesTo(ParseTreeNode parseTreeNode)
        {
            if (parseTreeNode == null || parseTreeNode.Term.Name != "stmtList")
                return false;

            return parseTreeNode.ChildNodes.Any(
                statement => IsSelectStatement(statement)
                             && AppliesToAllTheColumnConstraints(statement)
                             && AppliesToFromConstraint(statement));
        }

        private static bool IsSelectStatement(ParseTreeNode parseTreeNode)
        {
            return parseTreeNode.Term.Name == "selectStmt";
        }

        private bool AppliesToAllTheColumnConstraints(ParseTreeNode parseTreeNode)
        {
            if (!_columnConstraints.Any())
                return true;
            
            return parseTreeNode.ChildNodes.Any(
                selectList => selectList.Term.Name == "selList"
                              && _columnConstraints.All(constraint => constraint.AppliesTo(selectList)));
        }

        private bool AppliesToFromConstraint(ParseTreeNode parseTreeNode)
        {
            if (_fromConstraint == null)
                return true;

            return parseTreeNode.ChildNodes.Any(
                from => from.Term.Name == "fromClauseOpt"
                        && _fromConstraint.AppliesTo(from));
        }
    }
}