using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Neptune.Web.ViewModel;
using System;
using Neptune.Web.Data.Response;
using Neptune.Web.Data.Request;

namespace Neptune.Web.Data
{
    public class PagesService 
    {
        public HttpClient HttpClient;

        public PagesService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<TransacoesMes> ObterTransacoesMesPage(int mes, int ano)
        {
            var contas = await ObterContas();
            var contasId = contas.Select(contas => contas.Id).ToList();

            return await ObterTransacoesMesPage(mes, ano, contasId);
        }

        public async Task<TransacoesMes> ObterTransacoesMesPage(int mes, int ano, List<int> contasId)
        {
            var transacoesDesteMes = await ObterTodasAsTransacoesDesteMes_Old(ano, mes);

            var saldoMesAnterior = await ObterSaldoMesAnterior(mes, ano, contasId, transacoesDesteMes);

            var todasContasModel = await ObterContas();

            var transacoesMes = new TransacoesMes(ano,
                                              mes,
                                              transacoesDesteMes,
                                              saldoMesAnterior,
                                              todasContasModel.Where(x => contasId.Contains(x.Id)).ToList(),
                                              todasContasModel);
            return transacoesMes;
        }

        public async Task<List<Conta>> ObterContas()
        {
            var contasResponse = await HttpClient.GetFromJsonAsync<ContasResponse>("/api/conta");
            var contas = new List<Conta>();
            contasResponse.ForEach(x => contas.Add(new Conta(x.Id, x.Nome, true)));

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


        private async Task<List<Transacao>> ObterTodasAsTransacoesDesteMes_Old(int ano, int mes)
        {
            var transacoesResponse = await HttpClient.GetFromJsonAsync<List<TransacaoResponse>>("/api/transacao");
            transacoesResponse.Sort((x, y) => x.Data.CompareTo(y.Data));

            var transacoes = new List<Transacao>();
            transacoesResponse.ForEach(transacaoResponse => transacoes.Add(new Transacao(transacaoResponse)));

            var transacoesDesteMes = transacoes.Where(x => x.Data.Month == mes && x.Data.Year == ano).ToList();

            return transacoesDesteMes;
        }

        private async Task<List<Transacao>> ObterTodasAsTransacoesDesteMes()
        {
            var transacoesResponse = await HttpClient.GetFromJsonAsync<List<TransacaoResponse>>("/api/transacao");
            transacoesResponse.Sort((x, y) => x.Data.CompareTo(y.Data));

            var transacoes = new List<Transacao>();
            transacoesResponse.ForEach(transacaoResponse => transacoes.Add(new Transacao(transacaoResponse)));

            return transacoes;
        }
    }
}