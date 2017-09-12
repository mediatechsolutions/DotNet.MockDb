using Irony.Parsing;
using System.Linq;

namespace DotNet.MockDb.Constraints.Expressions
{
    public class StringExpressionConstraint : IExpressionConstraint
	{
        private readonly string _value;

        public StringExpressionConstraint(string value)
		{
            _value = value;
        }

		public bool AppliesTo(ParseTreeNode parseTreeNode) 
		{
            return parseTreeNode.Term.Name == "string_literal"
            	&& parseTreeNode.Token?.ValueString == _value;
        }
	}
}

