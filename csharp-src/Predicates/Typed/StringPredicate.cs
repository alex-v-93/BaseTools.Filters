using BaseTools.Filters.Enum;

namespace BaseTools.Filters.Predicates.Typed
{
	public sealed class StringPredicate : PredicateBase<string, StringPredicate>
	{
		public StringPredicate Like(string value, bool not = false)
		{
			Rules.Add(new Rule<string>
			{
				Value = value,
				Operation = TypeOperation.Like,
				Not = not
			});
			return this;
		}
	}
}
