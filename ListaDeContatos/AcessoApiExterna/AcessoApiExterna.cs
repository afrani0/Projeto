
using System;
using System.Net.Http;

namespace AcessoApiExterna
{
    public class AppCLiente
    {
        HttpClient _factory;

        public AppCLiente()
        {
            this._factory = new HttpClient();
        }


        public async System.Threading.Tasks.Task<string> EstadoAsync()
        {
            var response = await _factory.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async System.Threading.Tasks.Task<string> CidadeAsync(string siglaEstado)
        {
            var response = await _factory.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados/" + siglaEstado+ "/municipios");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async System.Threading.Tasks.Task<string> CEPAsync(string cep)
        {
            var response = await _factory.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

    }
}
