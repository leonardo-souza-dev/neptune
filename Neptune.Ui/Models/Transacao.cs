namespace Neptune.Ui.Models
{
    public class Transacao
    {
        public Transacao(int id, string descricao, string categoria, decimal valor, DateTime? data, int contaId)
        {
            Id = id;
            Descricao = descricao;
            Categoria = categoria;
            Valor = valor;
            Data = data;
            ContaId = contaId;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Data { get; set; }
        public int ContaId { get; set; }
    }
}
