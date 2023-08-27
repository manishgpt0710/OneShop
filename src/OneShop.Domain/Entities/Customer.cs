using System;
using System.Net;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public required long UserId { get; set; }
        public required User User { get; set; }

        public long CartId { get; set; }
        public Cart? Cart { get; set; }

        public List<Order>? Orders { get; set; }
        public List<ImageLocation>? Images { get; set; }
    }
}
