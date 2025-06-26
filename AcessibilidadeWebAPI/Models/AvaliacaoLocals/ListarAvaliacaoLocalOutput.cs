using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Local;

namespace AcessibilidadeWebAPI.Models.AvaliacaoLocals
{
    public class ListarAvaliacaoLocalOutput
    {
        public IEnumerable<AvaliacaoLocalDto> ArrAvaliacaoLocal { get; set; }

    }
}
