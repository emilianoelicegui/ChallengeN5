using System.Linq.Expressions;

namespace ChallengeN5.Repositories.UnitOfWork
{
    public static class QueryExtension
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                    Expression<Func<T, bool>> expr2)
        {
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(expr1.Body, expr2.Body), param);
            }
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expr1.Body,
                    Expression.Invoke(expr2, param)), param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            InvocationExpression invocationExpression = Expression.Invoke((Expression)expression2, expression1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>((Expression)Expression.OrElse(expression1.Body,
                (Expression)invocationExpression),
                (IEnumerable<ParameterExpression>)expression1.Parameters);
        }
    }
}
