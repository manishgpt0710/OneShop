using System;
using System.Net;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class Vendor : BaseEntity
    {
        public required long UserId { get; set; }
        public required User User { get; set; }
        public string? Description { get; set; }
        public string? GSTIN { get; set; }
        public string? LicenseNumber { get; set; }
        public string? SiteUrl { get; set; }
        public long OpeningTime;
        public long ClosingTime;
        public List<Catalog>? Catalogs { get; set; }
        public List<ImageLocation>? Images { get; set; }
    }
}
