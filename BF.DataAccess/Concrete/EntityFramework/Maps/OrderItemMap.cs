using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(@"OrderItems");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Amount).HasColumnName("amount");
            builder.Property(x=>x.OrderId);
            builder.Property(x=>x.ProductId);

            builder.HasOne(x => x.Order).WithMany(x=>x.OrderItems).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Product).WithMany(x=>x.OrderItems).HasForeignKey(x => x.ProductId);

        }
    }
}