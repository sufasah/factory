using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BF.DataAccess.Concrete.EntityFramework.Maps
{
    public class WorkCenterOperationMap : IEntityTypeConfiguration<WorkCenterOperation>
    {
        public void Configure(EntityTypeBuilder<WorkCenterOperation> builder)
        {
            builder.ToTable(@"WorkCenterOperations");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasColumnName("id").UseMySqlIdentityColumn<int>();
            builder.Property(x=>x.OperationId);
            builder.Property(x=>x.Speed).HasColumnName("speed");
            builder.Property(x=>x.WorkCenterId);

            builder.HasOne(x=>x.WorkCenter).WithMany(x=>x.WorkCenterOperations).HasForeignKey(x => x.WorkCenterId);
            builder.HasOne(x=>x.Operation).WithMany(x=>x.WorkCenterOperations).HasForeignKey(x => x.OperationId);
        }
    }
}