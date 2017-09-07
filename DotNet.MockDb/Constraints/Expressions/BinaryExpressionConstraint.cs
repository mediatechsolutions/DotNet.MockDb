using System;
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
			throw new NotImplementedException();
		}
	}
}

