using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSVentaja.Domain.Repository;
using RSVentaja.Domain.Service;
using RSVentaja.Repository.Insurers;
using RSVentaja.Repository.Policies;
using RSVentaja.Repository.User;

namespace RSVentaja
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddTransient<IPolicyRepository, PolicyRepository>(x => new PolicyRepository(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IPolicyService, PolicyService>();
            services.AddTransient<IInsurerRepository, InsurerRepository>(x => new InsurerRepository(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IInsurerService, InsurerService>();
            services.AddTransient<IUserRepository, UserRepository>(x => new UserRepository(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHsts();

        }
    }
}