
using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using MediatR;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<AzureMqttPushService>();

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IVoluntarioRepositorio, VoluntarioRepositorio>();
            builder.Services.AddScoped<IDeficienteRepositorio, DeficienteRepositorio>();
            builder.Services.AddScoped<ILocalRepositorio, LocalRepositorio>();

            string connectionString = builder.Configuration.GetSection("ConnectionStringOptions")["ConnectionString"];
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

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }
    }
}
