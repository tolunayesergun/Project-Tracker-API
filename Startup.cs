using EntryTracker.Services.Abstract;
using EntryTracker.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectTracker_API.DataAccess.DataAccessLayers.Abstract;
using ProjectTracker_API.DataAccess.DataAccessLayers.Concrete;
using ProjectTracker_API.Hubs;
using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Services.Abstract;
using ProjectTracker_API.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectTracker_API_API
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
            var databaseSettings = Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
            Environment.SetEnvironmentVariable("Db_DatabaseSource", databaseSettings.DbSource);
            Environment.SetEnvironmentVariable("Db_DatabaseUserName", databaseSettings.UserName);
            Environment.SetEnvironmentVariable("Db_DatabasePassword", databaseSettings.Password);
            var AppSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            Environment.SetEnvironmentVariable("Token", AppSettings.Token);

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddAutoMapper(typeof(Program));
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IUserDAL, UserDAL>();
            services.AddSingleton<IProjectDAL, ProjectDAL>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<IEntryDAL, EntryDAL>();
            services.AddSingleton<IEntryService, EntryService>();
            services.AddSignalR();
            services.AddSwaggerGen();
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => { builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true).AllowCredentials(); }));
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API"

                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Appsettings:Token").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();


            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("", context =>
                {
                    context.Response.Redirect("./swagger/index.html", permanent: false);
                    return Task.FromResult(0);
                });
                endpoints.MapHub<Notification>("/notifyhub");
            });


        }
    }
}
