namespace AcessibilidadeWebAPI.Models.Assistencias
{
    public class EditarAssistenciaInput
    {
        public int IdAssistencia { get; set; }
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public DateTimeOffset DataAceite { get; set; }
        public DateTimeOffset DataConclusao { get; set; }
    }
}
