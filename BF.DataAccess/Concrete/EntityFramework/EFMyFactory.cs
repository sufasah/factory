using BF.DataAccess.Concrete.EntityFramework.Maps;
using BF.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BF.DataAccess.Concrete.EntityFramework
{
    public class EFMyFactory:DbContext
    {

        public EFMyFactory()
        {
            
        }
        public EFMyFactory(DbContextOptions options):base(options){
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new WorkCenterMap());
            builder.ApplyConfiguration(new OperationMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderItemMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new SubProductMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new WorkCenterOperationMap());
            builder.ApplyConfiguration(new ScheduleMap());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connstr = "Server=localhost;Port=3306;Database=myfactory;Uid=root;Pwd=123;";
            optionsBuilder.UseMySql(connstr,ServerVersion.AutoDetect(connstr));
        }
        
        public DbSet<Customer> Customers;
        public DbSet<Operation> Operations;
        public DbSet<Order> Orders;
        public DbSet<OrderItem> OrderItems;
        public DbSet<Product> Products;
        public DbSet<SubProduct> SubProducts;
        public DbSet<User> Users;
        public DbSet<WorkCenter> WorkCenters;
        public DbSet<WorkCenterOperation> WorkCenterOperations;
        public DbSet<Schedule> Schedules;


    }
}