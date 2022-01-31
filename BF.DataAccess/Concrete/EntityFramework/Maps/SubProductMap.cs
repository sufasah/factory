using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class SubProductMap : IEntityTypeConfiguration<SubProduct>
    {
        public void Configure(EntityTypeBuilder<SubProduct> builder)
        {
            builder.ToTable(@"SubProducts");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Amount).HasColumnName("amount");
            builder.Property(x=>x.ProductId);

            builder.HasOne(x=>x.Product).WithMany(x=>x.SubProducts).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.ProductId);
        }
    }
}