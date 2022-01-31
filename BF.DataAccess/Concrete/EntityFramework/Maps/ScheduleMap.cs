using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class ScheduleMap : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable(@"Schedules");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.Start).HasColumnName("start");
            builder.Property(x=>x.End).HasColumnName("end");
            builder.Property(x=>x.ProductId);
            builder.Property(x=>x.WorkcenterId);

            builder.HasOne(x=>x.Product).WithMany(x=>x.Schedules).HasForeignKey(x=>x.ProductId);
            builder.HasOne(x=>x.WorkCenter).WithMany(x=>x.Schedules).HasForeignKey(x=>x.WorkcenterId);
        }
    }
}