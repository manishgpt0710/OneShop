using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class Item : BaseEntity
    {
        public required long CatalogId { get; set; }
        public required Catalog CatalogInfo { get; set; }  // Navigation Property

        public string? Code { get; set; }
        public required string Name { get; set; }        
        public string? Description { get; set; }
        public List<ImageLocation>? Images { get; set; }

        public required long ItemTypeId { get; set; }
        public required LookupTable ItemType { get; set; }      // VEG, NONVEG

        public decimal OriginalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Rating { get; set; }
        public decimal ReadyTime { get; set; }
        public required int Position { get; set; }

        public required long StatusId { get; set; }
        public required LookupTable Status { get; set; }        // Available, Out of stock
    }
}
