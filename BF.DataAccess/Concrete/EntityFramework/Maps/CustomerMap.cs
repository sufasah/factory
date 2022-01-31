using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(@"Customers");
            builder.HasKey(x=>x.Id);
            builder.HasIndex(x=>x.Name).IsUnique();
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Name).HasColumnName("name");
            builder.Property(x=>x.Password).HasColumnName("password");

            builder.HasMany(x=>x.Orders).WithOne(x=>x.Customer).HasForeignKey(x=>x.CustomerId);
        }
    }
}