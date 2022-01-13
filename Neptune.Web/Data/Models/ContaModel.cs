namespace Neptune.Web.Data.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal SaldoInicial { get; set; }

        private bool _ativo;
        public bool Ativo
        {
            get
            { 
                return _ativo;
            }
            set
            {
                value = _ativo;                
            }
        }

        public ContaModel(int id, string nome, decimal saldoInicial, bool ativo)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
            Ativo = ativo;
        }
    }
}
