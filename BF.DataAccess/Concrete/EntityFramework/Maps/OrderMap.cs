using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(@"Orders");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.CustomerId);
            builder.Property(x=>x.Deadline).HasColumnName("deadline");
            builder.Property(x=>x.OrderDate).HasColumnName("order_date");
            builder.Property(x=>x.State).HasColumnName("state");

            builder.HasOne(x=>x.Customer).WithMany(x=>x.Orders).HasForeignKey(x => x.CustomerId);            
            builder.HasMany(x=>x.OrderItems).WithOne(x=>x.Order).HasForeignKey(x => x.OrderId);            
        }
    }
}