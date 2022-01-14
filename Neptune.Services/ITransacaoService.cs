using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public interface ITransacaoService
    {
        Task<List<Transacao>> ObterTodas();
        Task<Mes> ObterMes(MesTransacao mesTransacao, List<Conta> contas);
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
    }
}
