using BaseTools.Filters.Enum;

namespace BaseTools.Filters.Predicates.Typed
{
	public sealed class IntPredicate : PredicateBase<int?, IntPredicate>
	{
		public IntPredicate GreaterThan(int value, bool not = false)
		{
			Rules.Add(new Rule<int?>
			{
				Value = value,
				Operation = TypeOperation.GreaterThan,
				Not = not
			});
			return this;
		}

		public IntPredicate LessThan(int value, bool not = false)
		{
			Rules.Add(new Rule<int?>
			{
				Value = value,
				Operation = TypeOperation.LessThan,
				Not = not
			});
			return this;
		}
	}
}
