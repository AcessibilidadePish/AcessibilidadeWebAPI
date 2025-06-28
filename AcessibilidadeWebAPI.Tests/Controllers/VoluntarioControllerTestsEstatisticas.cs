using AcessibilidadeWebAPI.Controllers;
using AcessibilidadeWebAPI.Models.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;

namespace AcessibilidadeWebAPI.Tests.Controllers
{
    public class VoluntarioControllerTestsEstatisticas
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly VoluntarioController _controller;

        public VoluntarioControllerTestsEstatisticas()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new VoluntarioController();
            
            // Configurar mediator via reflection já que não temos injeção pública
            var mediatorField = typeof(VoluntarioController).BaseType?
                .GetField("mediator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mediatorField?.SetValue(_controller, _mediatorMock.Object);
        }

        [Fact]
        public async Task EstatisticasPorRegiao_DeveRetornarEstatisticasCorretas()
        {
            // Arrange
            var estatisticasEsperadas = new EstatisticasPorRegiaoResultado
            {
                Estatisticas = new List<EstatisticaRegiaoDto>
                {
                    new EstatisticaRegiaoDto
                    {
                        Regiao = "Norte",
                        Quantidade = 5,
                        PercentualDisponivel = 80.0m,
                        AvaliacaoMedia = 4.5m
                    },
                    new EstatisticaRegiaoDto
                    {
                        Regiao = "Sul",
                        Quantidade = 3,
                        PercentualDisponivel = 66.67m,
                        AvaliacaoMedia = 4.2m
                    }
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EstatisticasPorRegiaoRequisicao>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(estatisticasEsperadas);

            // Act
            var resultado = await _controller.EstatisticasPorRegiao(CancellationToken.None);

            // Assert
            var okResult = resultado.Result.Should().BeOfType<OkObjectResult>().Subject;
            var output = okResult.Value.Should().BeOfType<EstatisticasPorRegiaoOutput>().Subject;
            
            output.Estatisticas.Should().HaveCount(2);
            output.Estatisticas.First().Regiao.Should().Be("Norte");
            output.Estatisticas.First().Quantidade.Should().Be(5);
            output.Estatisticas.First().PercentualDisponivel.Should().Be(80.0m);
        }

        [Fact]
        public async Task EstatisticasPorRegiao_QuandoNaoHaVoluntarios_DeveRetornarRegioesComZero()
        {
            // Arrange
            var estatisticasVazias = new EstatisticasPorRegiaoResultado
            {
                Estatisticas = new List<EstatisticaRegiaoDto>
                {
                    new EstatisticaRegiaoDto
                    {
                        Regiao = "Norte",
                        Quantidade = 0,
                        PercentualDisponivel = 0,
                        AvaliacaoMedia = 0
                    }
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EstatisticasPorRegiaoRequisicao>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(estatisticasVazias);

            // Act
            var resultado = await _controller.EstatisticasPorRegiao(CancellationToken.None);

            // Assert
            var okResult = resultado.Result.Should().BeOfType<OkObjectResult>().Subject;
            var output = okResult.Value.Should().BeOfType<EstatisticasPorRegiaoOutput>().Subject;
            
            output.Estatisticas.Should().HaveCount(1);
            output.Estatisticas.First().Quantidade.Should().Be(0);
        }

        [Fact]
        public async Task EstatisticasPorRegiao_DeveUsarCancellationToken()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EstatisticasPorRegiaoRequisicao>(), cancellationToken))
                .ReturnsAsync(new EstatisticasPorRegiaoResultado { Estatisticas = new List<EstatisticaRegiaoDto>() });

            // Act
            await _controller.EstatisticasPorRegiao(cancellationToken);

            // Assert
            _mediatorMock.Verify(
                m => m.Send(It.IsAny<EstatisticasPorRegiaoRequisicao>(), cancellationToken),
                Times.Once
            );
        }
    }
} 