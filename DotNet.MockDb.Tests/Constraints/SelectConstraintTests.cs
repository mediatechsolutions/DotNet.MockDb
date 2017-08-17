using DotNet.MockDb.Constraints;
using DotNet.MockDb.Grammars;
using Irony.Parsing;
using NUnit.Framework;

namespace DotNet.MockDb.Tests.Constraints
{
    [TestFixture]
    public class SelectConstraintTests
    {
        [TestCase("SELECT [Schema1].[Table1].[Column1] FROM Schema1.Table1")]
        [TestCase("select [Schema1].[Table1].[Column1] FROM Schema1.Table1")]
        [TestCase("Select [Schema1].[Table1].[Column1] FROM Schema1.Table1")]
        public void CaseIsIgnored(string sql)
        {
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint().AppliesTo(parseTree.Root), Is.True);
        }
        
        [Test]
        public void MatchingColumn()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .WithColumn("Column1", "Table1", "Schema1")
                .AppliesTo(parseTree.Root), Is.True);
        }

        [Test]
        public void NotMatchingColumn()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .WithColumn("Column3", "Table1", "Schema1")
                .AppliesTo(parseTree.Root), Is.False);
        }

        [Test]
        public void MatchingFrom()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .From("Table1", "Schema1")
                .AppliesTo(parseTree.Root), Is.True);
        }
        
        [Test]
        public void NotMatchingFrom()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .From("Table2", "Schema1")
                .AppliesTo(parseTree.Root), Is.False);
        }

		[Test]
		public void MatchingWhere()
		{
			var sql = @"SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2
				FROM Schema1.Table1
				WHERE [Table1].[Column1] = 1 AND ([Table1].[Column2] LIKE '%test%' OR [Table1].[Column3] < 16)";
			var parser = new Parser(new Sql89Grammar());
			var parseTree = parser.Parse(sql);
			Assert.That(new SelectConstraint()
				.From("Table1", "Schema1")
				.Where()
				.AppliesTo(parseTree.Root), Is.True);
			/*
			 * binExpr -> binExpr  binOp  exprList
			 * 
			 * binExpr -> Id   binOp  number
			 * 
			 * binOp ->  Token.Value 
			 * 
			 * number -> Token.Value
			 * 
			 * exprList -> binExpr
			 * 
			 * unExpr -> unOp | string_literal | number | funCall
			*/
		}
    }
}