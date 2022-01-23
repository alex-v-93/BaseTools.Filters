using System;

namespace BaseTools.Filters.Tests.Models
{
    public interface IOrderPosition
    {
        IProduct Product { get; }
        int Count { get; }
        double? Discount { get; }
        Guid CustomerId { get; }
        Guid? PersonalityManager { get; }
        DateTimeOffset? FormedAt { get; }
        DateTimeOffset CreateAt { get; }
    }
}
