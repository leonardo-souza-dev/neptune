using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public interface ITransacaoService
    {
        Task<List<Transacao>> ObterTodas();
        Task<MesOld> ObterMes(DataMes mesTransacao, List<Conta> contas);
        Task<Meses> ObterMeses();
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
    }
}
