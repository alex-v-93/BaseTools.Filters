using System.Collections.Generic;

namespace BaseTools.Filters.Common
{
	public interface IFilter : IFilterPredicate
	{
		IReadOnlyCollection<IFilterPredicate> AdditionalFilters { get; }
		IReadOnlyCollection<(string propertyPath, bool askending)> Sort { get; }
		int Offset { get; }
		int Limit { get; }
	}
}
