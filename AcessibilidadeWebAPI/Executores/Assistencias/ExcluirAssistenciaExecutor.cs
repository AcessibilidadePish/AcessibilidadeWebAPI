using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Assistencias
{
    public class ExcluirAssistenciaExecutor : IRequestHandler<ExcluirAssistenciaRequisicao>
    {
        private readonly IAssistenciaRepositorio assistenciaRepositorio;

        public ExcluirAssistenciaExecutor(IAssistenciaRepositorio assistenciaRepositorio)
        {
            this.assistenciaRepositorio = assistenciaRepositorio;
        }
        public Task Handle(ExcluirAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Assistencia assistencia = assistenciaRepositorio.ObterPorId(request.IdAssistencia);

            assistenciaRepositorio.Deletar(assistencia);

            return Task.CompletedTask;
        }
    }
}
