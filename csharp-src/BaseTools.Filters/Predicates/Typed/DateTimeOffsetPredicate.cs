using BaseTools.Filters.Enum;
using System;

namespace BaseTools.Filters.Predicates.Typed
{
	public sealed class DateTimeOffsetPredicate : PredicateBase<DateTimeOffset?, DateTimeOffsetPredicate>
	{
		public DateTimeOffsetPredicate GreaterThan(DateTimeOffset value, bool not = false)
		{
			Rules.Add(new Rule<DateTimeOffset?>
			{
				Value = value,
				Operation = TypeOperation.GreaterThan,
				Not = not
			});
			return this;
		}

		public DateTimeOffsetPredicate LessThan(DateTimeOffset value, bool not = false)
		{
			Rules.Add(new Rule<DateTimeOffset?>
			{
				Value = value,
				Operation = TypeOperation.LessThan,
				Not = not
			});
			return this;
		}
	}
}
