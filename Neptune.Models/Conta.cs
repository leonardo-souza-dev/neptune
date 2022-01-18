using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Domain
{
    public class Conta
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal SaldoInicial { get; private set; }

        private bool _selecionada = true;
        public bool Selecionada 
        { 
            get
            {
                return _selecionada;
            }
            set
            {
                _selecionada = value;
            }
        }

        public Conta(int id, string nome, decimal saldoInicial)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
        }
    }
}
