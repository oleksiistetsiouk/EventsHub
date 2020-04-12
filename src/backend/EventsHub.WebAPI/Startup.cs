using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventsHub.Mobile.Models;
using EventsHub.BLL.Extensions;
using EventsHub.BLL.Services;
using EventsHub.BLL.Configurations;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EventsHub.BLL.Interfaces;
using FluentValidation.AspNetCore;

namespace EventsHub.Mobile.Web
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
            services.AddMvc()
                .AddFluentValidation(options =>
            {
                   options.RegisterValidatorsFromAssemblyContaining<Startup>();
                   options.ImplicitlyValidateChildProperties = true;
            });

            services.AddSqlServerDbContext(Configuration.GetConnectionString("SmarterAspConnectionString"));
            services.AddUnitOfWork();
            ConfigureAuth(services);
            //repositories
            services.AddSingleton<IItemRepository, ItemRepository>();
            //services
            services.AddTransient<ParserService>();
            services.AddTransient<CleanerService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITheatreService, TheatreService>();
            services.AddTransient<IFilmService, FilmService>();
            services.AddTransient<IConcertService, ConcertService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}