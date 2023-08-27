using System;
using System.ComponentModel.DataAnnotations;

namespace OneShop.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTimeOffset Created { get; set; }

        //public string? CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        //public string? LastModifiedBy { get; set; }
    }
}
    
