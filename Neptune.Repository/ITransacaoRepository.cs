using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface ITransacaoRepository
    {
        Task<List<Transacao>> ObterTodas();
        Transacao Obter(int id);
        Task<List<Transacao>> ObterMesContas(int ano, int mes, List<int> contaIds);
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
    }
}
