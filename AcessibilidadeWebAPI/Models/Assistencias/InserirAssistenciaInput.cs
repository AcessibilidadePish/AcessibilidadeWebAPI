namespace AcessibilidadeWebAPI.Models.Assistencias
{
    public class InserirAssistenciaInput
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public DateTimeOffset DataAceite { get; set; }
        public DateTimeOffset DataConclusao { get; set; }
    }
}
