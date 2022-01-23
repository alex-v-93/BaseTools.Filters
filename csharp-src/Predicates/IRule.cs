using BaseTools.Filters.Enum;

namespace BaseTools.Filters.Predicates
{
	public interface IRule<T>
	{
		T Value { get; }
		TypeOperation Operation { get; }
		bool Not { get; }
	}
}
