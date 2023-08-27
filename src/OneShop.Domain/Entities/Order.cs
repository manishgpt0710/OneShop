using System;
using System.Net;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public required long CustomerId { get; set; }
        public required Customer Customer { get; set; }  // Navigation Property

        public required DateTime OrderDate { get; set; }

        public required long CartId { get; set; }
        public required Cart Cart { get; set; }  // Navigation Property

        public required UserAddress DeliveryAddress { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }

        public long StatusId { get; set; } // Foreign key
        public required LookupTable Status { get; set; }    // Pending, Paid, Delivered, Cancelled, Failed
    }
}

