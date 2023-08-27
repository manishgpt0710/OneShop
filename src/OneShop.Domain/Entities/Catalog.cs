using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class Catalog : BaseEntity
    {
        public required long VendorId { get; set; }
        public required Vendor Vendor { get; set; }  // Navigation Property

        public required string Name { get; set; }
        public required int Position  { get; set; }
        public List<Item>? Items { get; set; }
    }
}
