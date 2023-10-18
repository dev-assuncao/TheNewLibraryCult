using LibraryCult.Identity.API.Data;
using LibraryCult.Identity.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq.Expressions;

namespace LibraryCult.Identity.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {

            builder.Services.AddDbContext<LivraryContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection"));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<LivraryContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<LivraryContext>();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowRequests", builder => builder.WithOrigins("http://localhost:4200/")
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
                .AllowAnyHeader());
                             
            });

            var appSettingsSection = builder.Configuration.GetSection("JWT");
            builder.Services.Configure<JWT>(appSettingsSection);

            var appSettings = appSettingsSection.Get<JWT>();

            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            //var key = 

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appSettings.Issuer,
                    ValidAudience = appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return builder;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseCors(opt => {
                opt.AllowAnyMethod();
                opt.SetIsOriginAllowed(origin => true);
                opt.AllowCredentials();
                opt.AllowAnyHeader();
            });

            app.UseCors("AllowRequests");
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
