using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AcessibilidadeWebAPI.BancoDados.DesignTimeContextFactory
{
    public class AcessibilidadeDesignTimeContextFactory : IDesignTimeDbContextFactory<AcessibilidadeDbContext>
    {
        public AcessibilidadeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AcessibilidadeDbContext>();
            if (args is not null && args.Length > 0)
                builder.UseSqlServer(args[0]);
            else
            {
                // Se o ambiente não for definido, assume-se originalmente que o ambiente é o de desenvolvimento (se nao foi trocado usando o comando abaixo)
                // Utilize o $env:ASPNETCORE_ENVIRONMENT='NomeAmbiente' para especificar o ambiente em que deve ser rodado (no console de gerenciador)
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG
                if (!environment.Contains("Development"))
                    throw new Exception("CUIDADO A MIGRATION IRA RODAR FORA DO AMBIENTE DE DEV");
#endif

                string path = Directory.GetCurrentDirectory();

                IConfigurationBuilder configBuilder =
                    new ConfigurationBuilder()
                        .SetBasePath(path)
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{environment}.json", true)
                        .AddEnvironmentVariables();

                IConfigurationRoot configuration = configBuilder.Build();

                string connectionString = configuration.GetSection("ConnectionStringOptions")["ConnectionString"];

                builder.UseSqlServer(connectionString);
            }

            return new AcessibilidadeDbContext(builder.Options);
        }
    }
}
