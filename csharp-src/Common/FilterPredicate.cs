using BaseTools.Filters.Enum;
using BaseTools.Filters.Extensions;
using BaseTools.Filters.Predicates;
using BaseTools.Filters.Predicates.Typed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BaseTools.Filters.Common
{
	public class FilterPredicate<TEntity> : IFilterPredicate
	{
		private readonly List<IPredicate<DateTimeOffset?>> _dateTimeOffsetPredicates = new List<IPredicate<DateTimeOffset?>>();
		private readonly List<IPredicate<int?>> _intPredicates = new List<IPredicate<int?>>();
		private readonly List<IPredicate<string>> _stringPredicates = new List<IPredicate<string>>();
		private readonly List<IPredicate<Guid?>> _guidPredicates = new List<IPredicate<Guid?>>();
		private readonly List<IPredicate<bool?>> _boolPredicates = new List<IPredicate<bool?>>();

		IReadOnlyCollection<IPredicate<DateTimeOffset?>> IFilterPredicate.DateTimeOffsetPredicates => _dateTimeOffsetPredicates;
		IReadOnlyCollection<IPredicate<int?>> IFilterPredicate.IntPredicates => _intPredicates;
		IReadOnlyCollection<IPredicate<string>> IFilterPredicate.StringPredicates => _stringPredicates;
		IReadOnlyCollection<IPredicate<Guid?>> IFilterPredicate.GuidPredicates => _guidPredicates;
		IReadOnlyCollection<IPredicate<bool?>> IFilterPredicate.BoolPredicates => _boolPredicates;
		public Operation Operation { get; set; }

		public DateTimeOffsetPredicate For(Expression<Func<TEntity, DateTimeOffset>> propExpression)
		{
			var predicate = new DateTimeOffsetPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_dateTimeOffsetPredicates.Add(predicate);
			return predicate;
		}

		public DateTimeOffsetPredicate For(Expression<Func<TEntity, DateTimeOffset?>> propExpression)
		{
			var predicate = new DateTimeOffsetPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_dateTimeOffsetPredicates.Add(predicate);
			return predicate;
		}

		public IntPredicate For(Expression<Func<TEntity, int>> propExpression)
		{
			var predicate = new IntPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_intPredicates.Add(predicate);
			return predicate;
		}

		public IntPredicate For(Expression<Func<TEntity, int?>> propExpression)
		{
			var predicate = new IntPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_intPredicates.Add(predicate);
			return predicate;
		}

		public StringPredicate For(Expression<Func<TEntity, string>> propExpression)
		{
			var predicate = new StringPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_stringPredicates.Add(predicate);
			return predicate;
		}

		public GuidPredicate For(Expression<Func<TEntity, Guid>> propExpression)
		{
			var predicate = new GuidPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_guidPredicates.Add(predicate);
			return predicate;
		}

		public GuidPredicate For(Expression<Func<TEntity, Guid?>> propExpression)
		{
			var predicate = new GuidPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_guidPredicates.Add(predicate);
			return predicate;
		}

		public BoolPredicate For(Expression<Func<TEntity, bool>> propExpression)
		{
			var predicate = new BoolPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_boolPredicates.Add(predicate);
			return predicate;
		}

		public BoolPredicate For(Expression<Func<TEntity, bool?>> propExpression)
		{
			var predicate = new BoolPredicate
			{
				PropertyPath = propExpression.ToMemberName()
			};

			_boolPredicates.Add(predicate);
			return predicate;
		}

		public FilterPredicate<TEntity> And()
		{
			Operation = Operation.And;
			return this;
		}

		public FilterPredicate<TEntity> Or()
		{
			Operation = Operation.Or;
			return this;
		}
	}
}
