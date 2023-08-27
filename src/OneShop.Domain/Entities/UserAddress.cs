using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public long UserId { get; set; }
        public string? HouseNumber { get; set; }
        public required string Address { get; set; }
        public required string Landmark { get; set; }
        public long PinCode { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public long AddressTypeId { get; set; } // Foreign key
        public LookupTable? AddressType { get; set; } // Home, Work Office, None
    }
}

