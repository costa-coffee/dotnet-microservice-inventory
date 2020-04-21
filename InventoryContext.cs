using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Inventory
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
        {
        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InventoryItem> InventoryItem { get; set; }
        public virtual DbSet<StoreInventoryItem> StoreInventoryItem { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=inventory;Username=postgres;Password=mysecretpassword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.ToTable("inventory_items");

                entity.HasIndex(e => e.Sku)
                    .HasName("uq:inventory_items.sku")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("sku");

                entity.Property(e => e.Updated).HasColumnName("updated");
            });

            modelBuilder.Entity<StoreInventoryItem>(entity =>
            {
                entity.ToTable("store_inventory_items");

                entity.HasIndex(e => new { e.Store, e.Item })
                    .HasName("uq:store_inventory_items.store+store_inventory_items.item")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Item).HasColumnName("item");

                entity.Property(e => e.Store).HasColumnName("store");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.StoreInventoryItems)
                    .HasForeignKey(d => d.Item)
                    .HasConstraintName("fk:store_inventory_items.item+inventory_items.id");

                entity.HasOne(d => d.StoreNavigation)
                    .WithMany(p => p.StoreInventoryItems)
                    .HasForeignKey(d => d.Store)
                    .HasConstraintName("fk:store_inventory_items.store+stores.id");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("stores");

                entity.HasIndex(e => e.Code)
                    .HasName("uq:stores.code")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Updated).HasColumnName("updated");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                E.Property("Created").CurrentValue = DateTime.Now;
                E.Property("Updated").CurrentValue = DateTime.Now;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("Updated").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
