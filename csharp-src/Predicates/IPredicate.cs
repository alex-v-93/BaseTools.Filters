using BaseTools.Filters.Enum;
using System.Collections.Generic;

namespace BaseTools.Filters.Predicates
{
	public interface IPredicate<T>
	{
		string PropertyPath { get; }
		IReadOnlyCollection<IRule<T>> Rules { get; }
		IReadOnlyCollection<(T[] value, bool not)> InRules { get; }
		Operation Operation { get; }
	}
}
