namespace AcessibilidadeWebAPI.Models.SolicitacaoAjudas
{
    public class InserirSolicitacaoAjudaInput
    {
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public double? Latitude { get;  set; }
        public double? Longitude { get;  set; }
        public string EnderecoReferencia { get;  set; }
    }
}
