namespace Neptune.Ui.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Ativo { get; set; }

        public Conta(int id, string nome, decimal saldoInicial, bool ativo)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
            Ativo = ativo;
        }
    }
}
