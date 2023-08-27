using System;
using OneShop.Domain.Common;

namespace OneShop.Domain.Entities
{
    public class LookupTable : BaseEntity
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}

    