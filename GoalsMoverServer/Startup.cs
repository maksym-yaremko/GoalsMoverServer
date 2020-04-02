using GoalsMover.BLL.IServices;
using GoalsMover.BLL.Services;
using GoalsMover.DAL;
using GoalsMover.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using AutoMapper;
using SimpleInjector.Lifestyles;
using GoalsMover.BLL.Mapper;

namespace GoalsMover
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
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapping>();
            });

            InitializeServiceContainers(services);

            services.AddControllers();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<GoalsMoverDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options=>options.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeServiceContainers(IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
