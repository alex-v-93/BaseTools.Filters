using BaseTools.Filters.Extensions;
using BaseTools.Filters.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BaseTools.Filters.Common
{
	public sealed class Filter<TEntity> : FilterPredicate<TEntity>, IFilter
	{
		private readonly List<IFilterPredicate> _additionalFilters = new List<IFilterPredicate>();
		private readonly List<(string propertyPath, bool askending)> _sort = new List<(string propertyPath, bool askending)>();

		IReadOnlyCollection<IFilterPredicate> IFilter.AdditionalFilters => _additionalFilters;
		public int Offset { get; set; }
		public int Limit { get; set; }
		IReadOnlyCollection<(string propertyPath, bool askending)> IFilter.Sort => _sort;

		public new Filter<TEntity> And()
		{
			Operation = Operation.And;
			return this;
		}

		public new Filter<TEntity> Or()
		{
			Operation = Operation.Or;
			return this;
		}

		public Filter<TEntity> Skip(int count)
		{
			Offset = count;
			return this;
		}

		public Filter<TEntity> Take(int count)
		{
			Limit = count;
			return this;
		}

		public Filter<TEntity> SortByAsc<TProp>(Expression<Func<TEntity, TProp>> propExpression)
		{
			_sort.Add((propExpression.ToMemberName(), true));
			return this;
		}

		public Filter<TEntity> SortByDesc<TProp>(Expression<Func<TEntity, TProp>> propExpression)
		{
			_sort.Add((propExpression.ToMemberName(), false));
			return this;
		}

		public FilterPredicate<TEntity> Group()
		{
			var predicate = new FilterPredicate<TEntity>();
			_additionalFilters.Add(predicate);

			return predicate;
		}
	}
}
