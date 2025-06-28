using AcessibilidadeWebAPI.Resultados.Voluntario;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Voluntario
{
    public class EstatisticasPorRegiaoRequisicao : IRequest<EstatisticasPorRegiaoResultado>
    {
        // Por enquanto não precisa de parâmetros de entrada
        // No futuro pode incluir filtros como período, estado específico, etc.
    }
} 