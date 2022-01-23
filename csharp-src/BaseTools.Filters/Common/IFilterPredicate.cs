using BaseTools.Filters.Enum;
using BaseTools.Filters.Predicates;
using System;
using System.Collections.Generic;

namespace BaseTools.Filters.Common
{
	public interface IFilterPredicate
	{
		IReadOnlyCollection<IPredicate<DateTimeOffset?>> DateTimeOffsetPredicates { get; }
		IReadOnlyCollection<IPredicate<int?>> IntPredicates { get; }
		IReadOnlyCollection<IPredicate<string>> StringPredicates { get; }
		IReadOnlyCollection<IPredicate<Guid?>> GuidPredicates { get; }
		IReadOnlyCollection<IPredicate<bool?>> BoolPredicates { get; }
		Operation Operation { get; }
	}
}
