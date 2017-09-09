using System;
using System.Linq;
using Irony.Parsing;

namespace DotNet.MockDb.Constraints.Expressions
{
	public class BinaryExpressionConstraint: IExpressionConstraint
	{
		private string operation;
		private IExpressionConstraint operand1;
		private IExpressionConstraint operand2;

		public BinaryExpressionConstraint(string operation, IExpressionConstraint operand1, IExpressionConstraint operand2)
		{
			this.operation = operation;
			this.operand1 = operand1;
			this.operand2 = operand2;
		}

		public bool AppliesTo(ParseTreeNode parseTreeNode) 
		{
			return parseTreeNode.Term.Name == "binExpr"
				&& (parseTreeNode.ChildNodes.ElementAt(1).Term.Name == "binOp" 
					&& parseTreeNode.ChildNodes.ElementAt(1).Token.Value == operation)
				&& ((operand1.AppliesTo (parseTreeNode.ChildNodes.ElementAt (0)) 
						&& operand2.AppliesTo (parseTreeNode.ChildNodes.ElementAt (2)))
					|| (operand1.AppliesTo (parseTreeNode.ChildNodes.ElementAt (2)) 
							&& operand2.AppliesTo (parseTreeNode.ChildNodes.ElementAt(0))));
		}
	}
}

