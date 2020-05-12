using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dta.Marketplace.Api.Shared;
using Dta.Marketplace.Api.Web.Handlers;
using Dta.Marketplace.Api.Services.EF;
using Dta.Marketplace.Api.Services.Redis;
using Dta.Marketplace.Api.Business.Mapping;

namespace Dta.Marketplace.Api.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        private readonly string _devOrigins = "_devOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services
                .AddAuthentication(Schemes.UserAuthenticationHandler)
                .AddScheme<AuthenticationSchemeOptions, UserAuthenticationHandler>(Schemes.UserAuthenticationHandler, null)
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(Schemes.ApiKeyAuthenticationHandler, null);

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<DigitalMarketplaceContext>(options => {
                        options.UseNpgsql(appSettings.MarketplaceConnectionString);
                    });

            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();

            services.AddAutoMapper(typeof(AutoMapping));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();

            // global cors policy
            // app.UseCors(x => x
            //     .AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());

            app.UseCors(_devOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
