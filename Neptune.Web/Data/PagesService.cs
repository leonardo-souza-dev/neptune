using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
//using Neptune.Domain;
using Neptune.Web.ViewModel;
using System;
using Neptune.Web.Data.Response;
using Neptune.Web.Data.Request;

namespace Neptune.Web.Data
{
    public class PagesService : IInteressado
    {
        public HttpClient HttpClient;
        public TransacoesMes TransacoesMes;

        public PagesService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task ObterTransacoesMesPage(int mes, int ano, List<int> contasId)
        {
            var transacoes = await ObterTransacoesSort();
            var transacoesModelMes = transacoes.Where(x => x.Data.Month == mes).ToList();
            var saldoMesAnterior = await ObterSaldoMesAnterior(mes, ano, contasId, transacoes);

            var todasContasModel = await ObterContasModel();

            TransacoesMes = new TransacoesMes(ano,
                                              mes,
                                              transacoesModelMes,
                                              saldoMesAnterior,
                                              todasContasModel.Where(x => contasId.Contains(x.Id)).ToList(),
                                              todasContasModel);

            TransacoesMes.Contas.ForEach(x => x.AdicionarInteressado(this));
        }

        public async Task<List<Conta>> ObterContas()
        {
            var contasModel = await HttpClient.GetFromJsonAsync<ContasResponse>("/api/conta");

            var contas = new List<Conta>();
            contasModel.ForEach(x => contas.Add(new Conta(x.Id, x.Nome, true)));

            return contas;
        }

        public async Task<Transacao> ObterTransacao(int id)
        {
            var transacaoDomain = await HttpClient.GetFromJsonAsync<TransacaoResponse>($"/api/transacao/{id}");

            return new Transacao(transacaoDomain);
        }

        public async Task<Transacao> EditarTransacao(int id, Transacao transacao)
        {
            var transacaoRequest = new TransacaoRequest(transacao.Id, 
                                                        transacao.Descricao, 
                                                        transacao.Valor, 
                                                        transacao.Data, 
                                                        transacao.ContaId);
            var response = await HttpClient.PutAsJsonAsync($"/api/transacao/{id}", transacaoRequest);
            var transacaoResponse = await response.Content.ReadFromJsonAsync<TransacaoResponse>();

            return new Transacao(transacaoResponse);
        }

        public async Task<Transacao> NovaTransacao(NovaTransacao novaTransacao)
        {
            var transacaoRequest = new TransacaoRequest(novaTransacao.Descricao,
                                                        novaTransacao.Valor, 
                                                        novaTransacao.Data,
                                                        novaTransacao.ContaId);

            var response = await HttpClient.PostAsJsonAsync("/api/transacao", transacaoRequest);
            var transacaoResponse = await response.Content.ReadFromJsonAsync<TransacaoResponse>();

            return new Transacao(transacaoResponse);
        }

        public async Task Atualizar()
        {
            var contas = TransacoesMes.Contas.Where(x => x.Ativo).Select(x => x.Id).ToList();

            await ObterTransacoesMesPage(TransacoesMes.Mes,
                                     TransacoesMes.Ano,
                                     contas);
        }






        private async Task<List<Conta>> ObterContasModel()
        {
            var contasResponse = await HttpClient.GetFromJsonAsync<ContasResponse>("/api/conta");
            var contas = new List<Conta>();
            contasResponse.ForEach(x => contas.Add(new Conta(x)));

            return contas;
        }

        private async Task<decimal> ObterSaldoMesAnterior(int mes, 
                                                          int ano, 
                                                          List<int> contasId, 
                                                          List<Transacao> transacoes)
        {
            var contas = new List<Conta>();
            foreach (var contaId in contasId)
            {
                var contaResponse = await HttpClient.GetFromJsonAsync<ContaResponse>($"/api/conta/{contaId}");
                contas.Add(new Conta(contaResponse));
            }
            var saldoInicialContas = contas.Sum(x => x.SaldoInicial);

            var transacoesMesPassadoPraTras = transacoes.Where(x => x.Data.Month < mes && x.Data.Year <= ano);
            var saldoMesAnterior = 
                saldoInicialContas - transacoesMesPassadoPraTras.Where(x => contasId.Contains(x.ContaId)).Sum(x => x.Valor);

            return saldoMesAnterior;
        }

        private async Task<List<Transacao>> ObterTransacoesSort()
        {
            var transacoesResponse = await HttpClient.GetFromJsonAsync<List<TransacaoResponse>>("/api/transacao");
            transacoesResponse.Sort((x, y) => x.Data.CompareTo(y.Data));

            var transacoes = new List<Transacao>();
            transacoesResponse.ForEach(transacaoResponse => transacoes.Add(new Transacao(transacaoResponse)));

            return transacoes;
        }
    }
}