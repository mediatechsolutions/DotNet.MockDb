using DotNet.MockDb.Constraints;
using DotNet.MockDb.Grammars;
using Irony.Parsing;
using NUnit.Framework;

namespace DotNet.MockDb.Tests.Constraints
{
    [TestFixture]
    public class ColumnConstraintTests
    {
        [TestCase("SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1", "Column1", "Table1", "Schema1")]
        [TestCase("SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1", "Column1", "Table1", null)]
        [TestCase("SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1", "Column1", null, null)]
        [TestCase("SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1", "Column2", "Table1", "Schema1")]
        [TestCase("SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1", "Column2", "Table1", null)]
        [TestCase("SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1", "Column2", null, null)]
        public void ColumnsMatch(string sql, string column, string table, string schema)
        {
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .WithColumn(column, table, schema)
                .AppliesTo(parseTree.Root), Is.True);
        }

        [Test]
        public void ColumnDoesNotMatch()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .WithColumn("Column3", "Table1", "Schema1")
                .AppliesTo(parseTree.Root), Is.False);
        }
        
        [Test]
        public void TableDoesNotMatch()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .WithColumn("Column1", "Table2", "Schema1")
                .AppliesTo(parseTree.Root), Is.False);
        }

        [Test]
        public void SchemaDoesNotMatch()
        {
            var sql = "SELECT [Schema1].[Table1].[Column1], Schema1.Table1.Column2 FROM Schema1.Table1";
            var parser = new Parser(new Sql89Grammar());
            var parseTree = parser.Parse(sql);
            Assert.That(new SelectConstraint()
                .WithColumn("Column1", "Table2", "Schema2")
                .AppliesTo(parseTree.Root), Is.False);
        }
    }
}