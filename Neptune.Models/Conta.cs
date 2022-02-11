using System;

namespace Neptune.Domain
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public decimal SaldoInicial { get; private set; }
        public bool Selecionada { get; private set; }

        public Conta(int id, string nome, decimal saldoInicial, bool selecionada)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
            Selecionada = selecionada;
        }

        public void SetarSelecionada(bool selecionada)
        {
            Selecionada = selecionada;
        }
    }
}
