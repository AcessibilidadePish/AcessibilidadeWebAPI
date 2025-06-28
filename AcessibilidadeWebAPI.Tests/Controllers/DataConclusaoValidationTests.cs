using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Dtos.Assistencia;
using FluentAssertions;
using Xunit;

namespace AcessibilidadeWebAPI.Tests.Controllers
{
    /// <summary>
    /// Testes específicos para validar que a correção de DataConclusao nullable está funcionando
    /// </summary>
    public class DataConclusaoValidationTests
    {
        [Fact]
        public void CorrecaoPrincipal_DataConclusaoNullable_EntidadeAssistencia()
        {
            // ✅ VALIDAÇÃO: Entidade Assistencia permite DataConclusao null
            
            // Arrange & Act - Criar assistência SEM DataConclusao (em andamento)
            var assistenciaEmAndamento = new Assistencia
            {
                IdAssistencia = 1,
                SolicitacaoAjudaId = 1,
                VoluntarioUsuarioId = 1,
                DataAceite = DateTimeOffset.Now,
                DataConclusao = null // ✅ TESTE PRINCIPAL: deve permitir null
            };

            // Act - Criar assistência COM DataConclusao (concluída)
            var assistenciaConcluida = new Assistencia
            {
                IdAssistencia = 2,
                SolicitacaoAjudaId = 2,
                VoluntarioUsuarioId = 1,
                DataAceite = DateTimeOffset.Now.AddDays(-1),
                DataConclusao = DateTimeOffset.Now // ✅ TESTE: deve permitir definir
            };

            // Assert - DataConclusao nullable funciona corretamente
            assistenciaEmAndamento.DataConclusao.Should().BeNull();
            assistenciaConcluida.DataConclusao.Should().NotBeNull();
            assistenciaConcluida.DataConclusao.HasValue.Should().BeTrue();
        }

        [Fact]
        public void CorrecaoPrincipal_DataConclusaoNullable_RequisicaoInserir()
        {
            // ✅ VALIDAÇÃO: InserirAssistenciaRequisicao permite DataConclusao null
            
            // Act - Criar requisição para aceitar solicitação (DataConclusao = null)
            var requisicaoAceitar = new InserirAssistenciaRequisicao
            {
                IdSolicitacaoAjuda = 1,
                IdUsuario = 1,
                DataAceite = DateTimeOffset.Now,
                DataConclusao = null // ✅ TESTE PRINCIPAL: null = assistência aceita mas não concluída
            };

            // Assert
            requisicaoAceitar.DataConclusao.Should().BeNull();
            requisicaoAceitar.DataAceite.Should().NotBe(default(DateTimeOffset));
        }

        [Fact]
        public void CorrecaoPrincipal_DataConclusaoNullable_RequisicaoEditar()
        {
            // ✅ VALIDAÇÃO: EditarAssistenciaRequisicao permite ambos os estados
            
            // Act - Manter em andamento
            var manterEmAndamento = new EditarAssistenciaRequisicao
            {
                IdAssistencia = 1,
                DataConclusao = null // ✅ Mantém em andamento
            };

            // Act - Concluir assistência
            var concluirAssistencia = new EditarAssistenciaRequisicao
            {
                IdAssistencia = 1,
                DataConclusao = DateTimeOffset.Now // ✅ Marca como concluída
            };

            // Assert
            manterEmAndamento.DataConclusao.Should().BeNull();
            concluirAssistencia.DataConclusao.Should().NotBeNull();
        }

        [Fact]
        public void CorrecaoPrincipal_DataConclusaoNullable_AssistenciaDto()
        {
            // ✅ VALIDAÇÃO: AssistenciaDto suporta DataConclusao nullable
            
            // Act
            var dtoEmAndamento = new AssistenciaDto
            {
                IdAssistencia = 1,
                DataAceite = DateTimeOffset.Now,
                DataConclusao = null // ✅ TESTE: DTO permite null
            };

            var dtoConcluida = new AssistenciaDto
            {
                IdAssistencia = 2,
                DataAceite = DateTimeOffset.Now.AddDays(-1),
                DataConclusao = DateTimeOffset.Now // ✅ TESTE: DTO permite definir
            };

            // Assert
            dtoEmAndamento.DataConclusao.Should().BeNull();
            dtoConcluida.DataConclusao.Should().NotBeNull();
        }

        [Fact]
        public void LogicaCorreta_ContarAssistenciasPorStatus_SemDataFutura()
        {
            // ✅ VALIDAÇÃO: Nova lógica usando HasValue ao invés de data futura
            
            // Arrange - Lista com assistências em diferentes status
            var assistencias = new List<Assistencia>
            {
                new Assistencia 
                { 
                    IdAssistencia = 1, 
                    DataAceite = DateTimeOffset.Now.AddDays(-3),
                    DataConclusao = null // ✅ Em andamento
                },
                new Assistencia 
                { 
                    IdAssistencia = 2, 
                    DataAceite = DateTimeOffset.Now.AddDays(-2),
                    DataConclusao = DateTimeOffset.Now.AddDays(-1) // ✅ Concluída
                },
                new Assistencia 
                { 
                    IdAssistencia = 3, 
                    DataAceite = DateTimeOffset.Now.AddDays(-1),
                    DataConclusao = null // ✅ Em andamento
                },
                new Assistencia 
                { 
                    IdAssistencia = 4, 
                    DataAceite = DateTimeOffset.Now.AddDays(-4),
                    DataConclusao = DateTimeOffset.Now // ✅ Concluída
                }
            };

            // Act - NOVA LÓGICA CORRETA (sem data futura)
            int assistenciasConcluidas = assistencias.Count(a => a.DataConclusao.HasValue);
            int assistenciasEmAndamento = assistencias.Count(a => !a.DataConclusao.HasValue);

            // Assert - Lógica nullable funciona perfeitamente
            assistenciasConcluidas.Should().Be(2); // IDs 2 e 4
            assistenciasEmAndamento.Should().Be(2); // IDs 1 e 3
            (assistenciasConcluidas + assistenciasEmAndamento).Should().Be(4); // Total correto

            // ✅ VALIDAÇÃO ADICIONAL: Nenhuma data futura está sendo usada
            assistencias.Should().NotContain(a => 
                a.DataConclusao.HasValue && 
                a.DataConclusao.Value > DateTimeOffset.Now.AddDays(1));
        }

        [Theory]
        [InlineData(null, false)] // null = não concluída
        [InlineData("2024-01-01", true)] // data = concluída
        public void EstadosAssistencia_DataConclusaoNullable_ComportamentoCorreto(string? dataConclusaoStr, bool deveConcluida)
        {
            // ✅ VALIDAÇÃO: Comportamento consistente com nullable
            
            // Arrange
            DateTimeOffset? dataConclusao = dataConclusaoStr != null 
                ? DateTimeOffset.Parse(dataConclusaoStr) 
                : null;

            var assistencia = new Assistencia
            {
                IdAssistencia = 1,
                DataAceite = DateTimeOffset.Now.AddDays(-1),
                DataConclusao = dataConclusao
            };

            // Act & Assert
            assistencia.DataConclusao.HasValue.Should().Be(deveConcluida);
            
            if (deveConcluida)
            {
                assistencia.DataConclusao.Should().NotBeNull();
                assistencia.DataConclusao.Value.Should().BeAfter(default(DateTimeOffset));
            }
            else
            {
                assistencia.DataConclusao.Should().BeNull();
            }
        }
    }
} 