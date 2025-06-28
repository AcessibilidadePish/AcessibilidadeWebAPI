using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Dtos.Assistencia;
using FluentAssertions;
using Xunit;

namespace AcessibilidadeWebAPI.Tests.Controllers
{
    public class DataConclusaoTests
    {
        [Fact]
        public void AssistenciaRequisicao_DataConclusaoNullable_DevePermitirNull()
        {
            // Arrange & Act
            var requisicao = new InserirAssistenciaRequisicao
            {
                IdSolicitacaoAjuda = 1,
                IdUsuario = 1,
                DataAceite = DateTimeOffset.Now,
                DataConclusao = null // ✅ Deve permitir null
            };

            // Assert
            requisicao.DataConclusao.Should().BeNull();
            requisicao.DataAceite.Should().NotBe(default(DateTimeOffset));
        }

        [Fact]
        public void AssistenciaRequisicao_DataConclusaoComValor_DevePermitirDefinir()
        {
            // Arrange
            var dataAceite = DateTimeOffset.Now.AddDays(-1);
            var dataConclusao = DateTimeOffset.Now;

            // Act
            var requisicao = new InserirAssistenciaRequisicao
            {
                IdSolicitacaoAjuda = 1,
                IdUsuario = 1,
                DataAceite = dataAceite,
                DataConclusao = dataConclusao // ✅ Deve permitir definir
            };

            // Assert
            requisicao.DataConclusao.Should().NotBeNull();
            requisicao.DataConclusao.Should().Be(dataConclusao);
            requisicao.DataConclusao.Value.Should().BeAfter(requisicao.DataAceite);
        }

        [Fact]
        public void EditarAssistenciaRequisicao_DataConclusaoNullable_DevePermitirAmbos()
        {
            // Arrange & Act - Com DataConclusao null
            var requisicaoEmAndamento = new EditarAssistenciaRequisicao
            {
                IdAssistencia = 1,
                IdUsuario = 1,
                IdSolicitacaoAjuda = 1,
                DataAceite = DateTimeOffset.Now.AddDays(-1),
                DataConclusao = null // ✅ Em andamento
            };

            // Act - Com DataConclusao definida
            var requisicaoConcluida = new EditarAssistenciaRequisicao
            {
                IdAssistencia = 2,
                IdUsuario = 1,
                IdSolicitacaoAjuda = 2,
                DataAceite = DateTimeOffset.Now.AddDays(-2),
                DataConclusao = DateTimeOffset.Now // ✅ Concluída
            };

            // Assert
            requisicaoEmAndamento.DataConclusao.Should().BeNull();
            requisicaoConcluida.DataConclusao.Should().NotBeNull();
        }

        [Fact]
        public void AssistenciaDto_DataConclusaoNullable_DevePermitirNull()
        {
            // Arrange & Act
            var dto = new AssistenciaDto
            {
                IdAssistencia = 1,
                IdSolicitacaoAjuda = 1,
                IdUsuario = 1,
                DataAceite = DateTimeOffset.Now,
                DataConclusao = null // ✅ Deve permitir null
            };

            // Assert
            dto.DataConclusao.Should().BeNull();
        }

        [Theory]
        [InlineData(true)] // Assistência concluída
        [InlineData(false)] // Assistência em andamento
        public void ListaAssistencias_ContarPorStatus_DeveUsarDataConclusaoNullable(bool estaConcluida)
        {
            // Arrange
            var assistencias = new List<AssistenciaDto>
            {
                new AssistenciaDto
                {
                    IdAssistencia = 1,
                    DataAceite = DateTimeOffset.Now.AddDays(-2),
                    DataConclusao = estaConcluida ? DateTimeOffset.Now : null
                },
                new AssistenciaDto
                {
                    IdAssistencia = 2,
                    DataAceite = DateTimeOffset.Now.AddDays(-3),
                    DataConclusao = null // Sempre em andamento
                },
                new AssistenciaDto
                {
                    IdAssistencia = 3,
                    DataAceite = DateTimeOffset.Now.AddDays(-1),
                    DataConclusao = DateTimeOffset.Now.AddHours(-1) // Sempre concluída
                }
            };

            // Act
            int concluidas = assistencias.Count(a => a.DataConclusao.HasValue);
            int emAndamento = assistencias.Count(a => !a.DataConclusao.HasValue);

            // Assert
            if (estaConcluida)
            {
                concluidas.Should().Be(2); // #1 concluída + #3 sempre concluída
                emAndamento.Should().Be(1); // #2 sempre em andamento
            }
            else
            {
                concluidas.Should().Be(1); // #3 sempre concluída
                emAndamento.Should().Be(2); // #1 em andamento + #2 sempre em andamento
            }
            
            (concluidas + emAndamento).Should().Be(3); // Total sempre 3
        }

        [Fact]
        public void StatusSolicitacao_EnumValues_DevemEstarCorretos()
        {
            // Act & Assert - Verificar valores dos enums
            ((int)StatusSolicitacao.Pendente).Should().Be(1);
            ((int)StatusSolicitacao.Aceita).Should().Be(2);
            ((int)StatusSolicitacao.Recusada).Should().Be(3);
            ((int)StatusSolicitacao.Concluída).Should().Be(4);
        }

        [Fact]
        public void TipoDeficiencia_EnumValues_DevemEstarCorretos()
        {
            // Act & Assert - Verificar valores dos enums
            ((int)TipoDeficiencia.Fisica).Should().Be(1);
            ((int)TipoDeficiencia.Visual).Should().Be(2);
            ((int)TipoDeficiencia.Auditiva).Should().Be(3);
            ((int)TipoDeficiencia.Cognitiva).Should().Be(4);
            ((int)TipoDeficiencia.Multipla).Should().Be(5);
            ((int)TipoDeficiencia.Outra).Should().Be(6);
        }

        [Fact]
        public void CriarSolicitacaoRequest_PropriedadesCorretas_DevemExistir()
        {
            // Arrange & Act
            var request = new CriarSolicitacaoRequest
            {
                Descricao = "Teste de descrição",
                Latitude = -23.5505,
                Longitude = -46.6333,
                EnderecoReferencia = "Centro de São Paulo"
            };

            // Assert - Verificar se as propriedades corretas existem
            request.Descricao.Should().Be("Teste de descrição");
            request.Latitude.Should().Be(-23.5505);
            request.Longitude.Should().Be(-46.6333);
            request.EnderecoReferencia.Should().Be("Centro de São Paulo");
        }
    }
} 