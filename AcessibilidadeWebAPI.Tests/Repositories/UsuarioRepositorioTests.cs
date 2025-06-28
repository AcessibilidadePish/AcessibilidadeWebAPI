using Xunit;
using FluentAssertions;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Tests.Helpers;

namespace AcessibilidadeWebAPI.Tests.Repositories
{
    public class UsuarioRepositorioTests : IDisposable
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly AcessibilidadeWebAPI.BancoDados.AcessibilidadeDbContext _context;

        public UsuarioRepositorioTests()
        {
            _context = TestDbContextFactory.CreateContextWithTestData();
            _usuarioRepositorio = new UsuarioRepositorio(_context);
        }

        [Fact]
        public async Task ObterUsuario_ComIdValido_DeveRetornarUsuario()
        {
            // Arrange
            var idUsuario = 1;

            // Act
            var resultado = await _usuarioRepositorio.ObterUsuario(idUsuario);

            // Assert
            resultado.Should().NotBeNull();
            resultado.IdUsuario.Should().Be(idUsuario);
            resultado.Nome.Should().Be("João Silva");
            resultado.Email.Should().Be("joao@teste.com");
        }

        [Fact]
        public async Task ObterUsuario_ComIdInvalido_DeveRetornarNull()
        {
            // Arrange
            var idUsuario = 999;

            // Act
            var resultado = await _usuarioRepositorio.ObterUsuario(idUsuario);

            // Assert
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task ObterUsuarioPorEmail_ComEmailValido_DeveRetornarUsuario()
        {
            // Arrange
            var email = "joao@teste.com";

            // Act
            var resultado = await _usuarioRepositorio.ObterUsuarioPorEmail(email);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Email.Should().Be(email);
            resultado.Nome.Should().Be("João Silva");
        }

        [Fact]
        public async Task ObterUsuarioPorEmail_ComEmailInvalido_DeveRetornarNull()
        {
            // Arrange
            var email = "inexistente@teste.com";

            // Act
            var resultado = await _usuarioRepositorio.ObterUsuarioPorEmail(email);

            // Assert
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task InserirUsuario_ComDadosValidos_DeveInserirERetornarUsuario()
        {
            // Arrange
            var novoUsuario = new Usuario
            {
                Nome = "Pedro Costa",
                Email = "pedro@teste.com",
                Telefone = "11666666666",
                Senha = "pedro123"
            };

            // Act
            var resultado = await _usuarioRepositorio.InserirUsuario(novoUsuario);

            // Assert
            resultado.Should().NotBeNull();
            resultado.IdUsuario.Should().BeGreaterThan(0);
            resultado.Nome.Should().Be("Pedro Costa");
            resultado.Email.Should().Be("pedro@teste.com");

            // Verificar se foi inserido no banco
            var usuarioInserido = await _usuarioRepositorio.ObterUsuarioPorEmail("pedro@teste.com");
            usuarioInserido.Should().NotBeNull();
            usuarioInserido.Nome.Should().Be("Pedro Costa");
        }

        [Fact]
        public void Listar_DeveRetornarTodosUsuarios()
        {
            // Act
            var resultado = _usuarioRepositorio.Listar().ToList();

            // Assert
            resultado.Should().HaveCount(2);
            resultado.Should().Contain(u => u.Nome == "João Silva");
            resultado.Should().Contain(u => u.Nome == "Maria Santos");
        }

        [Fact]
        public void Listar_ComFiltro_DeveRetornarUsuariosFiltrados()
        {
            // Act
            var resultado = _usuarioRepositorio.Listar(u => u.Nome.Contains("João")).ToList();

            // Assert
            resultado.Should().HaveCount(1);
            resultado.First().Nome.Should().Be("João Silva");
        }

        [Fact]
        public void ObterPorId_ComIdValido_DeveRetornarUsuario()
        {
            // Arrange
            var idUsuario = 1;

            // Act
            var resultado = _usuarioRepositorio.ObterPorId(idUsuario);

            // Assert
            resultado.Should().NotBeNull();
            resultado.IdUsuario.Should().Be(idUsuario);
            resultado.Nome.Should().Be("João Silva");
        }

        [Fact]
        public void ObterPorId_ComIdInvalido_DeveRetornarNull()
        {
            // Arrange
            var idUsuario = 999;

            // Act
            var resultado = _usuarioRepositorio.ObterPorId(idUsuario);

            // Assert
            resultado.Should().BeNull();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
} 