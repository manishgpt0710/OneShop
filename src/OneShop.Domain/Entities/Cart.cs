using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public List<CartItemDetail>? CartItemDetails { get; set; }

        public required long StatusId { get; set; }
        public required LookupTable Status { get; set; }        // Active, Closed
    }
}
