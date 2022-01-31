using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(@"Users");
            builder.HasKey(x=>x.Id);
            builder.HasIndex(x=>x.Name).IsUnique();
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Name).HasColumnName("name");
            builder.Property(x=>x.Password).HasColumnName("password");
        }
    }
}