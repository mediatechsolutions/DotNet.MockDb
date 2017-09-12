using System;
using DotNet.MockDb.Constraints.Expressions;
using DotNet.MockDb.Grammars;
using Irony.Parsing;
using NUnit.Framework;

namespace DotNet.MockDb.Tests
{
	[TestFixture]
	public class StringExpressionConstraintTests
	{
		[Test]
		public void ValidStringLiteral()
		{
			var sql = "'test'";
			var parser = new Parser(new Sql89Grammar());
			var parseTree = parser.Parse(sql);
			Assert.That(new StringExpressionConstraint("test").AppliesTo(parseTree.Root), Is.True);
		}

	}
}
