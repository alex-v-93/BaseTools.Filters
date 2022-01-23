using BaseTools.Filters.Enum;

namespace BaseTools.Filters.Predicates
{
	public sealed class Rule<T> : IRule<T>
	{
		public T Value { get; set; }

		public TypeOperation Operation { get; set; }

		public bool Not { get; set; }
	}
}
