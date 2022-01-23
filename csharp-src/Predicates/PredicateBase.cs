using BaseTools.Filters.Enum;
using System.Collections.Generic;

namespace BaseTools.Filters.Predicates
{
	public abstract class PredicateBase<TValue, TCurrentPredicate> : IPredicate<TValue>
		where TCurrentPredicate : PredicateBase<TValue, TCurrentPredicate>
	{
		private protected PredicateBase() { }

		IReadOnlyCollection<(TValue[] value, bool not)> IPredicate<TValue>.InRules => InRules;
		IReadOnlyCollection<IRule<TValue>> IPredicate<TValue>.Rules => Rules;

		public string PropertyPath { get; internal set; }
		public Operation Operation { get; private set; }
		protected List<IRule<TValue>> Rules { get; } = new List<IRule<TValue>>();
		protected List<(TValue[] value, bool not)> InRules { get; } = new List<(TValue[] value, bool not)>();

		public TCurrentPredicate Equal(TValue value, bool not = false)
		{
			Rules.Add(new Rule<TValue>
			{
				Value = value,
				Operation = TypeOperation.Equal,
				Not = not
			});
			return (TCurrentPredicate)this;
		}

		public TCurrentPredicate In(params TValue[] values)
		{
			InRules.Add((values, false));
			return (TCurrentPredicate)this;
		}

		public TCurrentPredicate NotIn(TValue[] values)
		{
			InRules.Add((values, true));
			return (TCurrentPredicate)this;
		}

		public TCurrentPredicate And()
		{
			Operation = Operation.And;
			return (TCurrentPredicate)this;
		}

		public TCurrentPredicate Or()
		{
			Operation = Operation.Or;
			return (TCurrentPredicate)this;
		}
	}
}
