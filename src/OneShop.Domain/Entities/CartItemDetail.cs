using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class CartItemDetail : BaseEntity
    {
        public required long CartId;
        public required Cart Cart { get; set; }  // Navigation Property

        public required long VendorId;
        public required Vendor Vendor { get; set; }  // Navigation Property

        public required long ItemId;
        public required Item Item { get; set; }  // Navigation Property

        public long Qty;
    }
}
