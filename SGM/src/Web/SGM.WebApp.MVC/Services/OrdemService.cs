using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SGM.WebApp.MVC.Extensions;
using SGM.WebApp.MVC.Models;

namespace SGM.WebApp.MVC.Services
{
    public class OrdemService : Service, IOrdemService
    {
        private readonly HttpClient _httpClient;

        public OrdemService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.OrdemUrl);

            _httpClient = httpClient;
        }

        public async Task<OrdemViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/ordem/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<OrdemViewModel>(response);
        }

        public async Task<IEnumerable<OrdemViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/ordem/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<OrdemViewModel>>(response);
        }

        public async Task<IEnumerable<OrdemViewModel>> ObterTodosSolicitacao(Guid id)
        {
            var response = await _httpClient.GetAsync($"/ordem/solicitacao/{id}");
                                              
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<OrdemViewModel>>(response);
        }

        public async Task<ResponseResult> Adicionar(OrdemViewModel ordem)
        {
            var ordemContent = ObterConteudo(ordem);
            
            var response = await _httpClient.PostAsync("/ordem/registrar/", ordemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Atualizar(OrdemViewModel ordem)
        {
            var ordemContent = ObterConteudo(ordem);

            var response = await _httpClient.PostAsync("/ordem/atualizar/", ordemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

    }
}