using System;
using System.Net;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public required long MobileNumber { get; set; }
        public List<UserAddress>? Addresses { get; set; }
        public decimal Rating { get; set; }
    }
}
