using Neptune.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Saldo
    {
        public List<SaldoConta> SaldoContas { get; set; } = new();
        public decimal Valor => SaldoContas.Sum(x => x.Valor);


        public Guid Guid { get; private set; } = Guid.NewGuid();


        public Saldo(List<SaldoConta> saldoContas)
        {
            SaldoContas = saldoContas;
        }

        public Saldo(Saldo saldo)
        {
            foreach (var item in saldo.SaldoContas)
            {
                SaldoContas.Add(new SaldoConta(item.Conta, item.Valor));
            }
        }

        public Saldo(List<Transacao> transacoes)
        {
            var contasTransacoes = transacoes.Select(t => t.Conta).ToList();
            
            foreach (var contaTransacao in contasTransacoes)
            {
                foreach (var transacao in transacoes)
                {
                    var saldoConta = new SaldoConta(contaTransacao, 0);

                    if (transacao.Conta.Id == contaTransacao.Id)
                    {
                        saldoConta.Adicionar(transacao.Valor);
                        SaldoContas.Add(saldoConta);
                    }
                }
            }
        }

        public Saldo Subtrair(Saldo saldo)
        {
            foreach(var thisItem in SaldoContas)
            {
                foreach (var item in saldo.SaldoContas)
                {
                    if (thisItem.Conta.Id == item.Conta.Id)
                    {
                        thisItem.Adicionar(item);
                    }
                }
            }

            return this;
        }

        public Saldo AdicionarValor(List<Transacao> transacoes)
        {
            for (int i = 0; i < SaldoContas.Count; i++)
            {
                for (int j = 0; j < transacoes.Count; j++)
                {
                    if (SaldoContas[i].Conta.Id == transacoes[j].Conta.Id)
                    {
                        SaldoContas[i].Adicionar(transacoes[j].Valor);
                    }
                }
            }

            return this;
        }

        public Saldo AdicionarValor(Transacao transacao)
        {
            for (int i = 0; i < SaldoContas.Count; i++)
            {
                if (SaldoContas[i].Conta.Id == transacao.Conta.Id)
                {
                    SaldoContas[i].Adicionar(transacao.Valor);
                }
            }            

            return this;
        }

        public Saldo Adicionar(Saldo saldoContas)
        {
            for (int i = 0; i < SaldoContas.Count; i++)
            {
                for (int j = 0; j < saldoContas.SaldoContas.Count; j++)
                {
                    var contaSaldoContas = saldoContas.SaldoContas[j];
                    if (SaldoContas[i].Conta.Id == contaSaldoContas.Conta.Id)
                    {
                        SaldoContas[i].Adicionar(contaSaldoContas.Valor);
                    }
                }
            }

            return this;
        }
    }
}
