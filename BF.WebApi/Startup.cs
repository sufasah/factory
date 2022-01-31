using System;
using System.Text.Json.Serialization;
using BF.DataAccess.Abstract.Repositories;
using BF.DataAccess.Concrete.EntityFramework;
using BF.DataAccess.Concrete.EntityFramework.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace BF.WebApi
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connstr = Configuration.GetConnectionString("MyFactoryMySQL");

            
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddDbContext<EFMyFactory>(options=> {
                options.UseMySql(connstr,ServerVersion.AutoDetect(connstr));
            });

            AddDependencies<EFMyFactory>(services);

            services.AddCors(options => {
                options.AddPolicy("all",builder => {
                    builder.WithOrigins("*")
                    .WithHeaders(HeaderNames.AccessControlAllowHeaders,"*")
                    .WithMethods(HeaderNames.AccessControlAllowMethods,"*");
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("all");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/api/v1/{controller}/{id?}");
            });
        }
        private void AddDependencies<TContext>(IServiceCollection s)
            where TContext:DbContext,new()
        {
            s.AddSingleton<ICustomerRepo,EFCustomerRepo<TContext>>();
            s.AddSingleton<IOperationRepo,EFOperationRepo<TContext>>();
            s.AddSingleton<IOrderRepo,EFOrderRepo<TContext>>();
            s.AddSingleton<IOrderItemRepo,EFOrderItemRepo<TContext>>();
            s.AddSingleton<IProductRepo,EFProductRepo<TContext>>();
            s.AddSingleton<ISubProductRepo,EFSubProductRepo<TContext>>();
            s.AddSingleton<IUserRepo,EFUserRepo<TContext>>();
            s.AddSingleton<IWorkCenterRepo,EFWorkCenterRepo<TContext>>();
            s.AddSingleton<IWorkCenterOperationRepo,EFWorkCenterOperationRepo<TContext>>();
            s.AddSingleton<IScheduleRepo,EFScheduleRepo<TContext>>();
        }
        
    }
}
