using Neptune.Ui.Infra.Response;
using Neptune.Ui.Models;
using System.Text;

namespace Neptune.Ui.Services
{
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient HttpClient;

        public ApiHelper(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<ContasResponse?> ObterContas()
        {
            return await HttpClient.GetFromJsonAsync<ContasResponse?>("/api/conta");
        }

        public async Task<ObterMesResponse?> ObterMesResponse(int ano, int mes)
        {
            var recurso = $"/api/transacao/obter-mes?ano={ano}&mes={mes}";

            return await HttpClient.GetFromJsonAsync<ObterMesResponse?>(recurso);
        }
    }

    public interface IApiHelper
    {
        Task<ContasResponse?> ObterContas();
        Task<ObterMesResponse?> ObterMesResponse(int ano, int mes);
    }
}
