using System;

namespace BaseTools.Filters.Tests.Models
{
    public interface IProduct
    {
        Guid Identifier { get; }
        string Name { get; }
        int Price { get; }
        int? Order { get; }
        DateTimeOffset? LastUpdate { get; }
        DateTimeOffset CreateAt { get; }
        bool IsDeleted { get; }
    }
}
