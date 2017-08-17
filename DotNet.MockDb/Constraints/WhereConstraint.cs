using System;
using Irony.Parsing;
using DotNet.MockDb.Constraints.Expressions;

namespace DotNet.MockDb.Constraints
{
	public class WhereConstraint: IConstraint
	{
		IExpressionConstraint expression;
		public WhereConstraint(IExpressionConstraint expression)
		{
			this.expression = expression;
		}

		public static IExpressionConstraint And(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint("and", op1, op2);
		}

		public static IExpressionConstraint Or(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint("or", op1, op2);
		}

		public static IExpressionConstraint EqualTo(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint("=", op1, op2);
		}

		public static IExpressionConstraint GreaterThanOrEqualTo(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint(">=", op1, op2);
		}

		public static IExpressionConstraint GreaterThan(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint(">", op1, op2);
		}

		public static IExpressionConstraint LessThanOrEqualTo(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint("<=", op1, op2);
		}

		public static IExpressionConstraint LessThan(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint("<", op1, op2);
		}

		public static IExpressionConstraint NotEqualTo(IExpressionConstraint op1, IExpressionConstraint op2)
		{
			return new BinaryExpressionConstraint("<>", op1, op2);
		}

		public bool AppliesTo(ParseTreeNode parseTreeNode) 
		{
			return true;
		}
	}
}

