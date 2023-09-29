//using Conocelos.Now;
//using Conocelos.Services;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using conocelos_v3.Data;
using conocelos_v3.Servicios;
using conocelos_v3.Services;
using conocelos_v3.Controllers;

// using Microsoft.IdentityModel.Tokens;

namespace conocelos_v3
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

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            });
            services.AddControllers();

            //// Conexion de la base de datos en el flujo principal
            services.AddDbContext<ConocelosV2Context>(options =>
                options.UseMySql(Configuration.GetConnectionString("conexion"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #region Inyeccion de dependencias
            // Inyeccion de dependecias de los servicios 
            //services.AddScoped<IAutorizacionService, AutorizacionService>();
            // services.AddScoped<IConvertJson, ConvertJson>();
            services.AddTransient<Utilis>();
            #endregion

            #region Autenticación con los JWT
            //// Inicia la parte de la autenticacion con los JWT
            //var key = Configuration.GetValue<string>("JwtSettings:key");
            //var keyBytes = Encoding.ASCII.GetBytes(key);

            //services.AddAuthentication(config =>
            //{
            //    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(config =>
            //{
            //    config.RequireHttpsMetadata = false;
            //    config.SaveToken = true;
            //    config.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});
            #endregion


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            // Configure the HTTP request pipeline.
            //if (env.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
