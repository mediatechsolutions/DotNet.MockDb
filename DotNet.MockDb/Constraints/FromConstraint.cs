using System.Linq;
using Irony.Parsing;

namespace DotNet.MockDb.Constraints
{
    public class FromConstraint : IConstraint
    {
        private readonly string _table;
        private readonly string _schema;

        public FromConstraint(string table, string schema = null)
        {
            _table = table;
            _schema = schema;
        }

        public bool AppliesTo(ParseTreeNode parseTreeNode)
        {
            return parseTreeNode.ChildNodes.Any(idlist => idlist.Term.Name == "idlist"
                && idlist.ChildNodes.Any(Id => Id.Term.Name == "Id"
                    && Id.ChildNodes.ElementAt(Id.ChildNodes.Count - 1).Token.Value as string == _table
                    && (_schema == null || Id.ChildNodes.ElementAt(Id.ChildNodes.Count - 2).Token.Value as string == _schema)));
        }
    }
}