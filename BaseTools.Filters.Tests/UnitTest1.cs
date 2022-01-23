using BaseTools.Filters.Common;
using BaseTools.Filters.Enum;
using BaseTools.Filters.Tests.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace BaseTools.Filters.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void FilterPredicteTest()
		{
			var dt = DateTimeOffset.UtcNow - TimeSpan.FromDays(365);
			var filter = new Filter<IOrderPosition>();

			filter.Take(20)
				.Skip(40)
				.SortByAsc(order => order.Product.CreateAt);

			filter.Or()
				.For(order => order.Count)
				.LessThan(10)
				.Equal(10)
				.GreaterThan(0);
			var group = filter.Group().And();
			group.For(order => order.Product.IsDeleted)
				.Equal(false);
			group.For(order => order.Product.LastUpdate)
				.Equal(null, true)
				.GreaterThan(dt);
			// Filter = { Count < 10 OR Count == 10 OR
			// Count > 0 OR
			// (Product.IsDeleted == false AND (Product.LastUpdate != null OR Product.LastUpdate > dt)) }

			Assert.IsTrue(((IFilter)filter).Sort.Any(s => s.propertyPath == "Product.CreateAt" && s.askending == true));
			Assert.AreEqual(((IFilter)filter).Sort.Count, 1);
			Assert.AreEqual(filter.Operation, Operation.Or);
			Assert.AreEqual(((IFilter)filter).AdditionalFilters.Count, 1);
			var gChec = ((IFilter)filter).AdditionalFilters.First();
			Assert.AreEqual(gChec.Operation, Operation.And);
			Assert.AreEqual(gChec.BoolPredicates.Count, 1);
			Assert.IsTrue(gChec.BoolPredicates.Any(p => p.PropertyPath == "Product.IsDeleted"));
			Assert.IsTrue(gChec.DateTimeOffsetPredicates.Any(p => p.PropertyPath == "Product.LastUpdate"));
		}

		[Test]
		public void DateTimeOffsetTest()
		{

			var dt1 = DateTimeOffset.UtcNow - TimeSpan.FromDays(1);
			var dt2 = DateTimeOffset.UtcNow - TimeSpan.FromDays(2);
			var dt3 = DateTimeOffset.UtcNow - TimeSpan.FromDays(3);
			var filter = new Filter<IOrderPosition>();
			// Product.CreateAt < (Now - 3 days)
			filter.And()
				.For(order => order.Product.CreateAt)
				.LessThan(dt3);

			// (Product.CreateAt < (Now - 3 days)) AND (FormedAt != null)
			filter.For(order => order.FormedAt)
				.Equal(null, true);

			// (Product.CreateAt < (Now - 3 days)) AND
			// (FormedAt != null) AND
			// (FormedAt > (Now - 2 days) OR FormedAt == (Now - 2 days))
			filter.For(order => order.FormedAt)
				.GreaterThan(dt2)
				.Equal(dt2);

			filter.SortByDesc(order => order.Product.Price);


			Assert.AreEqual(((IFilterPredicate)filter).DateTimeOffsetPredicates.Count, 3);
			Assert.IsTrue(((IFilterPredicate)filter).DateTimeOffsetPredicates
				.Any(p => p.PropertyPath == "Product.CreateAt" && p.Rules.Any(r => r.Value == dt3)));
			Assert.IsTrue(((IFilterPredicate)filter).DateTimeOffsetPredicates
				.Any(p => p.PropertyPath == "FormedAt" && p.Rules.Any(r => r.Value == null)));
			Assert.IsTrue(((IFilterPredicate)filter).DateTimeOffsetPredicates
				.Any(p => p.PropertyPath == "FormedAt" && p.Rules.Count(r => r.Value == dt2 && 
				(r.Operation == TypeOperation.Equal || r.Operation == TypeOperation.GreaterThan)) == 2));
			Assert.IsTrue(((IFilter)filter).Sort.Any(s => s.propertyPath == "Product.Price" && s.askending == false));
			Assert.AreEqual(((IFilter)filter).Sort.Count, 1);
			Assert.AreEqual(filter.Operation, Operation.And);
			Assert.Pass();
		}

		[Test]
		public void StringTest()
		{
			var s1 = "Oculus";
			var s2 = "Iphone";

			var filter = new Filter<IOrderPosition>();
			// Product.Name like "Oculus" OR Product.Name like "Iphone" OR Product.Name in ("Oculus Rift", "Iphone XR", "Iphone 13")  
			filter.And()
				.For(order => order.Product.Name)
				.Like(s1)
				.Like(s2)
				.In("Oculus Rift", "Iphone XR", "Iphone 13");

			// (Product.Name like "Oculus" OR Product.Name like "Iphone" OR Product.Name in ("Oculus Rift", "Iphone XR", "Iphone 13")) AND 
			// (PersonalityManager != null)
			filter.For(order => order.PersonalityManager)
				.Equal(null, true);


			filter.SortByDesc(order => order.Product.Price);


			Assert.AreEqual(((IFilterPredicate)filter).StringPredicates.Count, 1);
			Assert.AreEqual(((IFilterPredicate)filter).GuidPredicates.Count, 1);
			Assert.IsTrue(((IFilterPredicate)filter).StringPredicates
				.Any(p => p.PropertyPath == "Product.Name" && p.Rules.Where(r => r.Value == s1 || r.Value == s2).Count() == 2));
			Assert.IsTrue(((IFilterPredicate)filter).GuidPredicates
				.Any(p => p.PropertyPath == "PersonalityManager" && p.Rules.Any(r => r.Value == null && r.Not)));
			Assert.IsTrue(((IFilter)filter).Sort.Any(s => s.propertyPath == "Product.Price" && s.askending == false));
			Assert.AreEqual(((IFilter)filter).Sort.Count, 1);
			Assert.AreEqual(filter.Operation, Operation.And);
		}
	}
}