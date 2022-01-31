using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(@"Products");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Amount).HasColumnName("amount");
            builder.Property(x=>x.IsSalable).HasColumnName("is_salable");
            builder.Property(x=>x.Name).HasColumnName("name");
            builder.Property(x=>x.Type).HasColumnName("type");

            builder.HasMany(x=>x.OrderItems).WithOne(x=>x.Product).HasForeignKey(x=>x.ProductId);
            builder.HasMany(x=>x.SubProducts).WithOne(x=>x.Product).HasForeignKey(x=>x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x=>x.Schedules).WithOne(x=>x.Product).HasForeignKey(x=>x.ProductId);
        }
    }
}