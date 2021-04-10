using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SGM.WebApp.MVC.Extensions;
using SGM.WebApp.MVC.Models;

namespace SGM.WebApp.MVC.Services
{
    public class CidadaoService : Service, ICidadaoService
    {
        private readonly HttpClient _httpClient;

        public CidadaoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CidadaoUrl);

            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CidadaoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/cidadao/ObterTodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<CidadaoViewModel>>(response);
        }

        public async Task<CidadaoViewModel> ObterPorId(Guid id)
        {

            var response = await _httpClient.GetAsync($"/cidadao/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CidadaoViewModel>(response);
        }

    }
}