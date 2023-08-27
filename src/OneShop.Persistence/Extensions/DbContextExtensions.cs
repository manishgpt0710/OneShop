using System;
using Microsoft.EntityFrameworkCore;

namespace OneShop.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        public const string Created = "Created";
        public const string LastModified = "LastModified";

        public static void UpdateEntityTimestamps(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property(Created).CurrentValue.Equals(default(DateTimeOffset)))
                    {
                        entry.Property(Created).CurrentValue = DateTimeOffset.UtcNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(LastModified).CurrentValue = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}

