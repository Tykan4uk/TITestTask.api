using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestTaskApi.Configurations;
using TestTaskApi.Data;
using TestTaskApi.DataProviders;
using TestTaskApi.DataProviders.Abstractions;
using TestTaskApi.Services;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            AppConfiguration = builder.Build();
        }

        public IConfiguration AppConfiguration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestTaskApi", Version = "v1" });
            });
            services.Configure<Config>(AppConfiguration);
            var connectionString = AppConfiguration["TestTaskApi:ConnectionString"];
            services.AddDbContext<TestTaskDbContext>(
                opts => opts.UseSqlServer(connectionString));
            services.AddTransient<IAccountProvider, AccountProvider>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IMessageProvider, MessageProvider>();
            services.AddTransient<IMessageService, MessageService>();
            var authConfig = AppConfiguration.GetSection("Auth");
            services.Configure<AuthConfig>(authConfig);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authConfig.Get<AuthConfig>().Issuer,

                        ValidateAudience = true,
                        ValidAudience = authConfig.Get<AuthConfig>().Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authConfig.Get<AuthConfig>().GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestTaskApi v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
