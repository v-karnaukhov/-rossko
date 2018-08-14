using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PermutationsService.Services.Abstract;
using PermutationsService.Web.DataAccess;
using PermutationsService.Web.DataAccess.Abstract;
using PermutationsService.Web.DataAccess.Concrete;

namespace PermutationsService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();

            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            services.AddEntityFrameworkSqlServer().AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            //services.AddScoped<DbContext, DataContext>();
            var connStrs = new Dictionary<string, string>
            {
                {"DB1", connectionString}
            };
            DbContextFactory.SetConnectionString(connStrs);
            services.AddTransient<IPermutationsService, Web.Services.Concrete.PermutationsService>();
            services.AddTransient<IPermutationsRepository, PermutationsRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);

            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
