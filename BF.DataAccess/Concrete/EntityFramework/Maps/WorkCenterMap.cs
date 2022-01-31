using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class WorkCenterMap : IEntityTypeConfiguration<WorkCenter>
    {
        public void Configure(EntityTypeBuilder<WorkCenter> builder)
        {
            builder.ToTable(@"WorkCenters");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Active).HasColumnName("active");
            builder.Property(x=>x.Name).HasColumnName("name");
            
            builder.HasMany(x=>x.WorkCenterOperations).WithOne(x=>x.WorkCenter).HasForeignKey(x=>x.WorkCenterId);
            builder.HasMany(x=>x.Schedules).WithOne(x=>x.WorkCenter).HasForeignKey(x=>x.WorkcenterId);
        }
    }
}