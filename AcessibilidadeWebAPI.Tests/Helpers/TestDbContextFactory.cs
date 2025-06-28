using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;

namespace AcessibilidadeWebAPI.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        public static void ConfigureTestDatabase(IServiceCollection services)
        {
            // Remove todos os DbContext existentes
            var descriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<AcessibilidadeDbContext>)).ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            // Remove configurações de DbContext genéricas
            var genericDescriptors = services.Where(d => d.ServiceType == typeof(AcessibilidadeDbContext)).ToList();
            foreach (var descriptor in genericDescriptors)
            {
                services.Remove(descriptor);
            }

            // Adiciona o contexto de teste em memória (substituindo completamente)
            services.AddDbContext<AcessibilidadeDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging(); // Para debug de testes
            }, ServiceLifetime.Scoped);
        }
        public static AcessibilidadeDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AcessibilidadeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AcessibilidadeDbContext(options);
            return context;
        }

        public static AcessibilidadeDbContext CreateContextWithTestData()
        {
            var context = CreateInMemoryContext();
            SeedTestData(context);
            return context;
        }

        private static void SeedTestData(AcessibilidadeDbContext context)
        {
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    IdUsuario = 1,
                    Nome = "João Silva",
                    Email = "joao@teste.com",
                    Telefone = "11999999999",
                    Senha = "123456"
                },
                new Usuario
                {
                    IdUsuario = 2,
                    Nome = "Maria Santos",
                    Email = "maria@teste.com",
                    Telefone = "11888888888",
                    Senha = "654321"
                }
            };

            context.Set<Usuario>().AddRange(usuarios);
            context.SaveChanges();
        }
    }
} 