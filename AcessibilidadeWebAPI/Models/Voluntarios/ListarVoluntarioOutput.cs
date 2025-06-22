using AcessibilidadeWebAPI.Dtos.Voluntario;

namespace AcessibilidadeWebAPI.Models.Voluntarios
{
    public class ListarVoluntarioOutput
    {
        public IEnumerable<VoluntarioDto> ArrVoluntario { get; set; }
    }
}
