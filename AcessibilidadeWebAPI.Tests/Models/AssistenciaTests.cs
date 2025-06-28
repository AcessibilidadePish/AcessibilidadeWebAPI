using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Models.Auth;
using FluentAssertions;
using Xunit;

namespace AcessibilidadeWebAPI.Tests.Models
{
    public class AssistenciaTests
    {
        [Fact]
        public void Assistencia_QuandoCriada_DevePermitirDataConclusaoNull()
        {
            // Arrange & Act
            var assistencia = new Assistencia
            {
                IdAssistencia = 1,
                SolicitacaoAjudaId = 1,
                VoluntarioUsuarioId = 1,
                DataAceite = DateTimeOffset.Now,
                DataConclusao = null // ✅ Permitir null
            };

            // Assert
            assistencia.DataConclusao.Should().BeNull();
            assistencia.DataAceite.Should().NotBe(default(DateTimeOffset));
        }

        [Fact]
        public void Assistencia_QuandoConcluida_DevePermitirDefinirDataConclusao()
        {
            // Arrange
            var assistencia = new Assistencia
            {
                IdAssistencia = 1,
                SolicitacaoAjudaId = 1,
                VoluntarioUsuarioId = 1,
                DataAceite = DateTimeOffset.Now.AddDays(-1),
                DataConclusao = null
            };

            var dataConclusao = DateTimeOffset.Now;

            // Act
            assistencia.DataConclusao = dataConclusao;

            // Assert
            assistencia.DataConclusao.Should().NotBeNull();
            assistencia.DataConclusao.Should().Be(dataConclusao);
            assistencia.DataConclusao.Value.Should().BeAfter(assistencia.DataAceite);
        }

        [Theory]
        [InlineData(true)] // Com DataConclusao = assistência concluída
        [InlineData(false)] // Com DataConclusao null = assistência em andamento
        public void Assistencia_ValidarStatusConclusao_DeveIdentificarCorretamente(bool estaConcluida)
        {
            // Arrange
            var assistencia = new Assistencia
            {
                IdAssistencia = 1,
                SolicitacaoAjudaId = 1,
                VoluntarioUsuarioId = 1,
                DataAceite = DateTimeOffset.Now.AddDays(-1),
                DataConclusao = estaConcluida ? DateTimeOffset.Now : null
            };

            // Act & Assert
            assistencia.DataConclusao.HasValue.Should().Be(estaConcluida);
            
            if (estaConcluida)
            {
                assistencia.DataConclusao.Should().NotBeNull();
            }
            else
            {
                assistencia.DataConclusao.Should().BeNull();
            }
        }

        [Fact]
        public void ListaAssistencias_ContarConcluidas_DeveUsarDataConclusaoNullable()
        {
            // Arrange
            var assistencias = new List<Assistencia>
            {
                new Assistencia { IdAssistencia = 1, DataConclusao = DateTimeOffset.Now }, // Concluída
                new Assistencia { IdAssistencia = 2, DataConclusao = null }, // Em andamento
                new Assistencia { IdAssistencia = 3, DataConclusao = DateTimeOffset.Now.AddDays(-1) }, // Concluída
                new Assistencia { IdAssistencia = 4, DataConclusao = null } // Em andamento
            };

            // Act
            int concluidas = assistencias.Count(a => a.DataConclusao.HasValue);
            int emAndamento = assistencias.Count(a => !a.DataConclusao.HasValue);

            // Assert
            concluidas.Should().Be(2);
            emAndamento.Should().Be(2);
            (concluidas + emAndamento).Should().Be(assistencias.Count);
        }
    }

    public class StatusSolicitacaoTests
    {
        [Theory]
        [InlineData(StatusSolicitacao.Pendente, 1)]
        [InlineData(StatusSolicitacao.Aceita, 2)]
        [InlineData(StatusSolicitacao.Recusada, 3)]
        [InlineData(StatusSolicitacao.Concluída, 4)]
        public void StatusSolicitacao_EnumValues_DevemTerValoresCorretos(StatusSolicitacao status, int valorEsperado)
        {
            // Act & Assert
            ((int)status).Should().Be(valorEsperado);
        }

        [Fact]
        public void StatusSolicitacao_FluxoCompleto_DeveProgredirCorretamente()
        {
            // Arrange & Act
            var statusInicial = StatusSolicitacao.Pendente;
            var statusAceita = StatusSolicitacao.Aceita;
            var statusFinal = StatusSolicitacao.Concluída;

            // Assert - Verificar progressão lógica
            ((int)statusInicial).Should().BeLessThan((int)statusAceita);
            ((int)statusAceita).Should().BeLessThan((int)statusFinal);
        }
    }

    public class TipoDeficienciaTests
    {
        [Theory]
        [InlineData(TipoDeficiencia.Fisica, 1)]
        [InlineData(TipoDeficiencia.Visual, 2)]
        [InlineData(TipoDeficiencia.Auditiva, 3)]
        [InlineData(TipoDeficiencia.Cognitiva, 4)]
        [InlineData(TipoDeficiencia.Multipla, 5)]
        [InlineData(TipoDeficiencia.Outra, 6)]
        public void TipoDeficiencia_EnumValues_DevemTerValoresCorretos(TipoDeficiencia tipo, int valorEsperado)
        {
            // Act & Assert
            ((int)tipo).Should().Be(valorEsperado);
        }

        [Fact]
        public void TipoDeficiencia_TodosOsTipos_DevemEstarDefinidos()
        {
            // Arrange
            var tiposEsperados = new[]
            {
                TipoDeficiencia.Fisica,
                TipoDeficiencia.Visual,
                TipoDeficiencia.Auditiva,
                TipoDeficiencia.Cognitiva,
                TipoDeficiencia.Multipla,
                TipoDeficiencia.Outra
            };

            // Act
            var valoresEnum = Enum.GetValues<TipoDeficiencia>();

            // Assert
            valoresEnum.Should().HaveCount(6);
            tiposEsperados.Should().BeEquivalentTo(valoresEnum);
        }
    }
} 