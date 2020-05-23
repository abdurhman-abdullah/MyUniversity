using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using university.Services;
using university.Services.IRepository;
using university.Services.Repository;

namespace university
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.NullValueHandling =
                    Newtonsoft.Json.NullValueHandling.Ignore;
                });
            var connectionString = Configuration["connectionString:universityDbConnectionString"];
            services.AddDbContext<UniversityDbContext>(u => u.UseSqlServer(connectionString));
            services.AddScoped<ISectionRepository, TheSectionRepository>();
            services.AddScoped<IDepartmentDirectorsRepository, DepartmentDirectorsRepository>();
            services.AddScoped<ISupervisorRepository, SupervisorRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
            services.AddScoped<ITeachersRepository, TeachersRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env , UniversityDbContext context)
        {
            //    if (env.IsDevelopment())
            //    {
            //        app.UseDeveloperExceptionPage();
            //    }
            //    else
            //    {
            //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //        app.UseHsts();
            //    }

            //    app.UseHttpsRedirection();
            //    app.UseMvc();
            //
            //context.SeedDataContext();
            //if(context == null)
            //{
            //    throw new ArgumentNullException(nameof(context));
            //}
            app.UseMvc();
        }
    }
}
