using AcessibilidadeWebAPI.Controllers;
using AcessibilidadeWebAPI.Models.AvaliacaoLocals;
using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;

namespace AcessibilidadeWebAPI.Tests.Controllers
{
    public class AvaliacaoLocalControllerTestsCompletas
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AvaliacaoLocalController _controller;

        public AvaliacaoLocalControllerTestsCompletas()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AvaliacaoLocalController();
            
            // Configurar mediator via reflection
            var mediatorField = typeof(AvaliacaoLocalController).BaseType?
                .GetField("mediator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mediatorField?.SetValue(_controller, _mediatorMock.Object);
        }

        [Fact]
        public async Task ListarAvaliacoesCompletas_DeveRetornarAvaliacoesComInformacoes()
        {
            // Arrange
            var avaliacoesEsperadas = new ListarAvaliacaoCompletaResultado
            {
                AvaliacoesCompletas = new List<AvaliacaoLocalCompletaDto>
                {
                    new AvaliacaoLocalCompletaDto
                    {
                        Id = 1,
                        LocalId = 1,
                        DispositivoId = 1,
                        Acessivel = true,
                        Observacoes = "Local bem acessível",
                        Timestamp = DateTime.Now,
                        Local = new LocalDto
                        {
                            IdLocal = 1,
                            Descricao = "Shopping Center",
                            Latitude = -23.5505,
                            Longitude = -46.6333,
                            AvaliacaoAcessibilidade = 8
                        },
                        Usuario = new UsuarioDto
                        {
                            IdUsuario = 1,
                            Nome = "João Silva",
                            Email = "joao@email.com",
                            Telefone = "11999999999"
                        },
                        Dispositivo = new DispositivoDto
                        {
                            Id = 1,
                            NumeroSerie = "DEV123456",
                            DataRegistro = DateTime.Now.AddDays(-30),
                            UsuarioProprietarioId = 1
                        }
                    }
                },
                Total = 1,
                PaginaAtual = 1,
                TamanhoPagina = 50,
                TemProximaPagina = false
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ListarAvaliacaoCompletaRequisicao>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(avaliacoesEsperadas);

            // Act
            var resultado = await _controller.ListarAvaliacoesCompletas();

            // Assert
            var okResult = resultado.Should().BeOfType<ObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            
            var output = okResult.Value.Should().BeOfType<ListarAvaliacaoCompletaOutput>().Subject;
            output.AvaliacoesCompletas.Should().HaveCount(1);
            output.Total.Should().Be(1);
            
            var avaliacao = output.AvaliacoesCompletas.First();
            avaliacao.Acessivel.Should().BeTrue();
            avaliacao.Local.Should().NotBeNull();
            avaliacao.Usuario.Should().NotBeNull();
            avaliacao.Dispositivo.Should().NotBeNull();
        }

        [Fact]
        public async Task ListarAvaliacoesCompletas_ComFiltros_DevePassarParametrosCorretos()
        {
            // Arrange
            var localId = 1;
            var usuarioId = 2;
            var acessivel = true;
            var dataInicio = DateTime.Now.AddDays(-7);
            var dataFim = DateTime.Now;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ListarAvaliacaoCompletaRequisicao>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ListarAvaliacaoCompletaResultado 
                { 
                    AvaliacoesCompletas = new List<AvaliacaoLocalCompletaDto>(),
                    Total = 0,
                    PaginaAtual = 1,
                    TamanhoPagina = 20,
                    TemProximaPagina = false
                });

            // Act
            await _controller.ListarAvaliacoesCompletas(1, 20, localId, usuarioId, acessivel, dataInicio, dataFim);

            // Assert
            _mediatorMock.Verify(
                m => m.Send(It.Is<ListarAvaliacaoCompletaRequisicao>(r => 
                    r.LocalId == localId &&
                    r.UsuarioId == usuarioId &&
                    r.Acessivel == acessivel &&
                    r.DataInicio == dataInicio &&
                    r.DataFim == dataFim &&
                    r.Pagina == 1 &&
                    r.TamanhoPagina == 20
                ), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task ListarAvaliacoesCompletas_ComPaginacao_DeveRetornarInformacoesPaginacao()
        {
            // Arrange
            var resultadoEsperado = new ListarAvaliacaoCompletaResultado
            {
                AvaliacoesCompletas = new List<AvaliacaoLocalCompletaDto>(),
                Total = 150,
                PaginaAtual = 2,
                TamanhoPagina = 25,
                TemProximaPagina = true
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ListarAvaliacaoCompletaRequisicao>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoEsperado);

            // Act
            var resultado = await _controller.ListarAvaliacoesCompletas(2, 25);

            // Assert
            var okResult = resultado.Should().BeOfType<ObjectResult>().Subject;
            var output = okResult.Value.Should().BeOfType<ListarAvaliacaoCompletaOutput>().Subject;
            
            output.Total.Should().Be(150);
            output.PaginaAtual.Should().Be(2);
            output.TamanhoPagina.Should().Be(25);
            output.TemProximaPagina.Should().BeTrue();
        }
    }
} 