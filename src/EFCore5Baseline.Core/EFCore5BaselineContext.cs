using System;
using EFCore5Baseline.Common.Extensions;
using EFCore5Baseline.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EFCore5Baseline.Core
{
    public class EFCore5BaselineContext : DbContext
    {
        public EFCore5BaselineContext(DbContextOptions<EFCore5BaselineContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Required function to set the fields CreatedAt and ModifiedAt on all functions, that doesn't use the .asNoTracked() (readOnly) chaining on Queries.
        /// </summary>
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;

                if (entry.Entity is IEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entity.ModifiedAt = now;
                            break;

                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.ModifiedAt = now;
                            break;
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enable snake_case for tables, columns, keys, constraints and indexes
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }
        }
    }
}