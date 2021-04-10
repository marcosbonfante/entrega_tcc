using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SGM.WebApp.MVC.Extensions;
using SGM.WebApp.MVC.Models;

namespace SGM.WebApp.MVC.Services
{
    public class SolicitacaoService : Service, ISolicitacaoService
    {
        private readonly HttpClient _httpClient;

        public SolicitacaoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.SolicitacaoUrl);

            _httpClient = httpClient;
        }

        public async Task<SolicitacaoViewModel> Adicionar(SolicitacaoViewModel solicitacao)
        {

            var solicitavaoContent = ObterConteudo(solicitacao);
            var response = await _httpClient.PostAsync("/solicitacao/nova-solicitacao/", solicitavaoContent);

            if (!TratarErrosResponse(response))
            {
                solicitacao.ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response);
            }

            return solicitacao;
        }

        public async Task<SolicitacaoViewModel> Atualizar(SolicitacaoViewModel solicitacao)
        {
            var solicitavaoContent = ObterConteudo(solicitacao);
            var response = await _httpClient.PostAsync("/solicitacao/atualizar-solicitacao/", solicitavaoContent);

            if (!TratarErrosResponse(response))
            {
                solicitacao.ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response);
            }

            return solicitacao;
        }

        public async Task<IEnumerable<SolicitacaoViewModel>> ObterTodosCidadao(Guid CidadaoId,string status)
        {
            var response = await _httpClient.GetAsync($"/solicitacao/solicitacao-user/{CidadaoId}/{status}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<SolicitacaoViewModel>>(response);
        }

        public async Task<IEnumerable<SolicitacaoViewModel>> ObterTodos(string status)
        {
            var response = await _httpClient.GetAsync($"/solicitacao/solicitacao-geral/{status}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<SolicitacaoViewModel>>(response);
        }

        public async Task<SolicitacaoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/solicitacao/solicitacao/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<SolicitacaoViewModel>(response);
        }

    }
}