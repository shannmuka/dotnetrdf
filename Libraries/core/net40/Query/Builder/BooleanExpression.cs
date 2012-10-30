using VDS.RDF.Query.Expressions;
using VDS.RDF.Query.Expressions.Conditional;

namespace VDS.RDF.Query.Builder
{
    public sealed class BooleanExpression : SparqlExpression<ISparqlExpression>
    {
        public BooleanExpression(ISparqlExpression expression)
            : base(expression)
        {
        }

        public BooleanExpression And(BooleanExpression rightExpression)
        {
            Expression = new AndExpression(Expression, rightExpression.Expression);
            return this;
        }

        public BooleanExpression Or(BooleanExpression rightExpression)
        {
            Expression = new OrExpression(Expression, rightExpression.Expression);
            return this;
        }
    }
}