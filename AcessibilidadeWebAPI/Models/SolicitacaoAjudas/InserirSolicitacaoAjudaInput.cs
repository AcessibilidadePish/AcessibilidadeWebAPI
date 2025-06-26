namespace AcessibilidadeWebAPI.Models.SolicitacaoAjudas
{
    public class InserirSolicitacaoAjudaInput
    {
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTimeOffset DataSolicitacao { get; set; }
    }
}
