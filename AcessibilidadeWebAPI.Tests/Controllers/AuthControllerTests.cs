using AcessibilidadeWebAPI.Controllers;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Repositorios.Dispositivos;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace AcessibilidadeWebAPI.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUsuarioRepositorio> _mockUsuarioRepositorio;
        private readonly Mock<IVoluntarioRepositorio> _mockVoluntarioRepositorio;
        private readonly Mock<IDeficienteRepositorio> _mockDeficienteRepositorio;
        private readonly Mock<IDispositivoRepositorio> _mockDispositivoRepositorio; 
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUsuarioRepositorio = new Mock<IUsuarioRepositorio>();
            _mockVoluntarioRepositorio = new Mock<IVoluntarioRepositorio>();
            _mockDeficienteRepositorio = new Mock<IDeficienteRepositorio>();
            _mockDispositivoRepositorio = new Mock<IDispositivoRepositorio>(); 
            _mockMapper = new Mock<IMapper>();
            
            // Configurar mock do IConfiguration para JWT
            var mockJwtSection = new Mock<IConfigurationSection>();
            mockJwtSection.Setup(x => x["SecretKey"]).Returns("MinhaChaveSecretaSuperSeguraParaJWT123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
            mockJwtSection.Setup(x => x["Issuer"]).Returns("AcessibilidadeWebAPI");
            mockJwtSection.Setup(x => x["Audience"]).Returns("AcessibilidadeWebAPI-Users");
            
            _mockConfiguration.Setup(x => x.GetSection("JwtSettings")).Returns(mockJwtSection.Object);
            
            // Configurar mock do AutoMapper
            _mockMapper.Setup(x => x.Map<UsuarioInfo>(It.IsAny<Usuario>()))
                .Returns((Usuario src) => new UsuarioInfo
                {
                    Id = src.IdUsuario,
                    Nome = src.Nome,
                    Email = src.Email,
                    Telefone = src.Telefone
                });

            _mockMapper.Setup(x => x.Map<VoluntarioInfo>(It.IsAny<Voluntario>()))
                .Returns((Voluntario src) => new VoluntarioInfo
                {
                    Disponivel = src.Disponivel,
                    Avaliacao = src.Avaliacao
                });

            _mockMapper.Setup(x => x.Map<DeficienteInfo>(It.IsAny<Deficiente>()))
                .Returns((Deficiente src) => new DeficienteInfo
                {
                    TipoDeficiencia = (int)src.TipoDeficiencia,
                    TipoDeficienciaDescricao = src.TipoDeficiencia.ToString()
                });

            // Configurar mocks dos repositórios para retornar listas vazias por padrão
            _mockVoluntarioRepositorio.Setup(x => x.Listar(It.IsAny<System.Linq.Expressions.Expression<Func<Voluntario, bool>>>(), It.IsAny<bool>()))
                .Returns(new List<Voluntario>().AsQueryable());
                
            _mockDeficienteRepositorio.Setup(x => x.Listar(It.IsAny<System.Linq.Expressions.Expression<Func<Deficiente, bool>>>(), It.IsAny<bool>()))
                .Returns(new List<Deficiente>().AsQueryable());

            _mockDispositivoRepositorio.Setup(x => x.Listar(It.IsAny<System.Linq.Expressions.Expression<Func<Dispositivo, bool>>>(), It.IsAny<bool>()))
                .Returns(new List<Dispositivo>().AsQueryable());

            _mockDispositivoRepositorio.Setup(x=>x.Inserir(It.IsAny<Dispositivo>()))
                .Returns((Dispositivo d) => d);

            _authController = new AuthController(_mockConfiguration.Object, _mockUsuarioRepositorio.Object, _mockVoluntarioRepositorio.Object, _mockDeficienteRepositorio.Object, _mockMapper.Object, _mockDispositivoRepositorio.Object);
        }

        [Fact]
        public async Task Login_ComCredenciaisValidas_DeveRetornarTokenJWT()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "joao@teste.com",
                Senha = "123456"
            };

            var usuario = new Usuario
            {
                IdUsuario = 1,
                Nome = "João Silva",
                Email = "joao@teste.com",
                Telefone = "11999999999",
                Senha = "123456"
            };

            _mockUsuarioRepositorio
                .Setup(x => x.ObterUsuarioPorEmail(loginRequest.Email))
                .ReturnsAsync(usuario);

            // Act
            var result = await _authController.Login(loginRequest);

            // Assert
            result.Should().BeOfType<ActionResult<LoginResponse>>();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var loginResponse = okResult.Value.Should().BeOfType<LoginResponse>().Subject;
            
            loginResponse.Token.Should().NotBeNullOrEmpty();
            loginResponse.Usuario.Should().NotBeNull();
            loginResponse.Usuario.Id.Should().Be(1);
            loginResponse.Usuario.Nome.Should().Be("João Silva");
            loginResponse.Usuario.Email.Should().Be("joao@teste.com");
        }

        [Fact]
        public async Task Login_ComCredenciaisInvalidas_DeveRetornarUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "inexistente@teste.com",
                Senha = "senhaErrada"
            };

            _mockUsuarioRepositorio
                .Setup(x => x.ObterUsuarioPorEmail(loginRequest.Email))
                .ReturnsAsync((Usuario)null);

            // Act
            var result = await _authController.Login(loginRequest);

            // Assert
            result.Result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Login_ComSenhaIncorreta_DeveRetornarUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "joao@teste.com",
                Senha = "senhaErrada"
            };

            var usuario = new Usuario
            {
                IdUsuario = 1,
                Nome = "João Silva",
                Email = "joao@teste.com",
                Telefone = "11999999999",
                Senha = "123456"
            };

            _mockUsuarioRepositorio
                .Setup(x => x.ObterUsuarioPorEmail(loginRequest.Email))
                .ReturnsAsync(usuario);

            // Act
            var result = await _authController.Login(loginRequest);

            // Assert
            result.Result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Register_ComDadosValidos_DeveRetornarTokenJWT()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Nome = "Carlos Oliveira",
                Email = "carlos@teste.com",
                Telefone = "11777777777",
                Senha = "senha123",
                TipoUsuario = TipoUsuario.Voluntario
            };

            _mockUsuarioRepositorio
                .Setup(x => x.ObterUsuarioPorEmail(registerRequest.Email))
                .ReturnsAsync((Usuario)null);

            var novoUsuario = new Usuario
            {
                IdUsuario = 3,
                Nome = registerRequest.Nome,
                Email = registerRequest.Email,
                Telefone = registerRequest.Telefone,
                Senha = registerRequest.Senha
            };

            _mockUsuarioRepositorio
                .Setup(x => x.InserirUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(novoUsuario);

            // Act
            var result = await _authController.Register(registerRequest);

            // Assert
            result.Should().BeOfType<ActionResult<LoginResponse>>();
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var loginResponse = okResult.Value.Should().BeOfType<LoginResponse>().Subject;
            
            loginResponse.Token.Should().NotBeNullOrEmpty();
            loginResponse.Usuario.Should().NotBeNull();
            loginResponse.Usuario.Nome.Should().Be("Carlos Oliveira");
            loginResponse.Usuario.Email.Should().Be("carlos@teste.com");
        }

        [Fact]
        public async Task Register_ComEmailJaExistente_DeveRetornarBadRequest()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Nome = "Carlos Oliveira",
                Email = "joao@teste.com", // Email já existente
                Telefone = "11777777777",
                Senha = "senha123",
                TipoUsuario = TipoUsuario.Voluntario
            };

            var usuarioExistente = new Usuario
            {
                IdUsuario = 1,
                Nome = "João Silva",
                Email = "joao@teste.com",
                Telefone = "11999999999",
                Senha = "123456"
            };

            _mockUsuarioRepositorio
                .Setup(x => x.ObterUsuarioPorEmail(registerRequest.Email))
                .ReturnsAsync(usuarioExistente);

            // Act
            var result = await _authController.Register(registerRequest);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
} 