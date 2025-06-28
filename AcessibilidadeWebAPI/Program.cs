
using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using MediatR;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AcessibilidadeWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Insira o token JWT desta forma: Bearer {seu token}"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddScoped<AzureMqttPushService>();

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IVoluntarioRepositorio, VoluntarioRepositorio>();
            builder.Services.AddScoped<IDeficienteRepositorio, DeficienteRepositorio>();
            builder.Services.AddScoped<ILocalRepositorio, LocalRepositorio>();
            builder.Services.AddScoped<IAvaliacaoLocalRepositorio, AvaliacaoLocalRepositorio>();
            builder.Services.AddScoped<ISolicitacaoAjudaRepositorio, SolicitacaoAjudaRepositorio>();
            builder.Services.AddScoped<IAssistenciaRepositorio, AssistenciaRepositorio>();


            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? builder.Configuration.GetSection("ConnectionStringOptions")["ConnectionString"];
            builder.Services.AddDbContext<AcessibilidadeDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, sqlServerOptions =>
                    {
                        int timououtEmSegundos = 60 * 6;
                        sqlServerOptions.CommandTimeout(timououtEmSegundos);
                    });
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            Assembly entryAssembly = Assembly.Load("AcessibilidadeWebAPI");
            Assembly[] assemblies = (new[] { entryAssembly, Assembly.GetExecutingAssembly() }).Distinct().ToArray();
            builder.Services.AddAutoMapper(assemblies);

            // Configuração JWT - compatível com ambos os formatos (JwtSettings ou Jwt)
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Exists() 
                ? builder.Configuration.GetSection("JwtSettings") 
                : builder.Configuration.GetSection("Jwt");
            
            var secretKey = jwtSettings["SecretKey"] ?? jwtSettings["Key"] ?? "defaultSecretKeyForDevelopment123!";
            var issuer = jwtSettings["Issuer"] ?? "https://localhost:7139";
            var audience = jwtSettings["Audience"] ?? "https://localhost:7139";

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();

            WebApplication app = builder.Build();

            // Aplicar migrações automaticamente (útil para produção)
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AcessibilidadeDbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Erro ao aplicar migrações do banco de dados");
                }
            }

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AcessibilidadeWebAPI v1");
                c.RoutePrefix = app.Environment.IsDevelopment() ? "swagger" : string.Empty; // Em produção, Swagger na raiz
            });

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Log de inicialização
            var startupLogger = app.Services.GetRequiredService<ILogger<Program>>();
            startupLogger.LogInformation($"Aplicação iniciada no ambiente: {app.Environment.EnvironmentName}");
            startupLogger.LogInformation($"URLs: {string.Join(", ", builder.WebHost.GetSetting(WebHostDefaults.ServerUrlsKey)?.Split(';') ?? new[] { "N/A" })}");

            app.Run();


        }
    }
}
