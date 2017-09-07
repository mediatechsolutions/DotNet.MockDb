using Irony.Parsing;
using System.Linq;

namespace DotNet.MockDb.Constraints.Expressions
{
    public class NumberExpressionConstraint : IExpressionConstraint
	{
        private readonly decimal _value;

        public NumberExpressionConstraint(decimal value)
		{
            _value = value;
        }

		public bool AppliesTo(ParseTreeNode parseTreeNode) 
		{
            return parseTreeNode.Term.Name == "number"
                           && decimal.Parse(parseTreeNode.Token.Value as string) == _value;
        }
	}
}

