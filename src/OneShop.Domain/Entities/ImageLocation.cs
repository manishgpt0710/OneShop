using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class ImageLocation : BaseEntity
    {
        public required long ParentId { get; set; }
        public required string ImageUrl { get; set; }
    }
}
