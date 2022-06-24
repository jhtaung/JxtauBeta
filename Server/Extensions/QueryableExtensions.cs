using System.Linq.Expressions;

namespace Server.Extensions
{
    public static class QueryableExtensions
    {
        public static Expression<Func<T, bool>> MakePropertiesPredicate<T, TValue>(
            TValue searchValue, 
            Expression<Func<TValue, TValue, bool>> pattern, 
            bool isOr
        ) {
            var parameter = Expression.Parameter(typeof(T), "e");
            var searchExpr = Expression.Constant(searchValue);
            var predicateBody = typeof(T).GetProperties()
                .Where(p => p.PropertyType == typeof(TValue))
                .Select(p =>
                    ExpressionReplacer.GetBody(
                        pattern, 
                        Expression.MakeMemberAccess(parameter, p), 
                        searchExpr
                    )
                )
                .Aggregate(isOr ? Expression.OrElse : Expression.AndAlso);
            return Expression.Lambda<Func<T, bool>>(predicateBody, parameter);
        }

        public static IQueryable<T> FilterByProperties<T, TValue>(
            this IQueryable<T> query, 
            TValue searchValue,
            Expression<Func<TValue, TValue, bool>> pattern, 
            bool isOr
        ) {
            return query.Where(MakePropertiesPredicate<T, TValue>(searchValue, pattern, isOr));
        }

        class ExpressionReplacer : ExpressionVisitor
        {
            readonly IDictionary<Expression, Expression> _replaceMap;

            public ExpressionReplacer(IDictionary<Expression, Expression> replaceMap)
            {
                _replaceMap = replaceMap ?? throw new ArgumentNullException(nameof(replaceMap));
            }

            public override Expression Visit(Expression? node)
            {
                if (node != null && _replaceMap.TryGetValue(node, out var replacement))
                {
                    return replacement;
                }

                return base.Visit(node)!;
            }

            public static Expression Replace(Expression expr, Expression toReplace, Expression toExpr)
            {
                return new ExpressionReplacer(new Dictionary<Expression, Expression> { { toReplace, toExpr } }).Visit(expr);
            }

            public static Expression Replace(Expression expr, IDictionary<Expression, Expression> replaceMap)
            {
                return new ExpressionReplacer(replaceMap).Visit(expr);
            }

            public static Expression GetBody(LambdaExpression lambda, params Expression[] toReplace)
            {
                if (lambda.Parameters.Count != toReplace.Length)
                    throw new InvalidOperationException();

                return new ExpressionReplacer(Enumerable.Range(0, lambda.Parameters.Count)
                    .ToDictionary(i => (Expression)lambda.Parameters[i], i => toReplace[i])).Visit(lambda.Body);
            }
        }
    }
}