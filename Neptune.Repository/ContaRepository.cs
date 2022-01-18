﻿using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain;
using System.Threading.Tasks;

namespace Neptune.Infra
{
    public class ContaRepository : IContaRepository
    {
        //TODO: trocar por base de dados real

        private readonly List<Conta> Contas = new()
        {
            new Conta(1, "Conta corrente", 50M),
            new Conta(2, "Poupanca", 1000M),
            new Conta(3, "Cartao de Credito", 0M)
        };

        public List<Conta> ObterTodas()
        {
            return Contas;
        }

        public Conta Obter(int id)
        {
            return Contas.FirstOrDefault(x => x.Id == id);
        }

        public Conta Criar(Conta conta)
        {
            var novaEntidade = new Conta(GetNextId(), conta.Nome, conta.SaldoInicial);

            Contas.Add(novaEntidade);

            return novaEntidade;
        }

        private int GetNextId()
        {
            return Contas.Max(x => x.Id) + 1;
        }
    }
}
