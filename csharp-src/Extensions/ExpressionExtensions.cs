using System;
using System.Linq.Expressions;

namespace BaseTools.Filters.Extensions
{
	public static class ExpressionExtensions
	{
		public static string ToMemberName<TEntity, T>(this Expression<Func<TEntity, T>> propExpression)
		{
			var unaryExpression = propExpression.Body as UnaryExpression;
			var property = unaryExpression?.Operand ?? propExpression;
			var parameterName = propExpression.Parameters[0].Name;

			return property.ToString()
				.Replace($"{parameterName} => {parameterName}.", string.Empty)
				.Replace($"{parameterName}.", string.Empty);
		}
	}
}
