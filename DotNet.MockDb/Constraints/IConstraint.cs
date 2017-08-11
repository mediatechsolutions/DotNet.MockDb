using Irony.Parsing;

namespace DotNet.MockDb.Constraints
{
    public interface IConstraint
    {
        bool AppliesTo(ParseTreeNode parseTreeNode);
    }
}