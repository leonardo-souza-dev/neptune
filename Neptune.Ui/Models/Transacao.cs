using Neptune.Ui.Infra.Response;

namespace Neptune.Ui.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int ContaId { get; set; }
        public string NomeConta { get; set; }        
    }
}
