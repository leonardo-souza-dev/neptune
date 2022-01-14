using Neptune.Domain;

namespace Neptune.Web.Data.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Ativo { get; set; }

        public ContaModel(int id, string nome, decimal saldoInicial, bool ativo)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
            Ativo = ativo;
        }

        public Conta ToDomain()
        {
            return new Conta(Id, Nome, SaldoInicial);
        }
    }
}
