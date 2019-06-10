using API.Utils;
using Logic.Interfaces.Repositories;
using Logic.Interfaces.Services;
using Logic.Repositories;
using Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Logic;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StudentsDbContext>(options =>
                options.UseSqlServer(_configuration["ConnectionString"],
                    b => b.MigrationsAssembly(typeof(StudentsDbContext)
                        .GetTypeInfo().Assembly.GetName().Name)));
            services.AddMvc();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StudentsDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DbSeeder.SeedDb(context);
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
