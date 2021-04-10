using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGM.Cidadao.API.Models;
using SGM.Core.Mediator;
using SGM.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGM.Cidadao.API.Controllers
{
    [Authorize]
    public class CidadaoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly ICidadaoRepository _cidadaoRepository;

        public CidadaoController(
            ICidadaoRepository cidadaoRepository,
            IMediatorHandler mediatorHandler)
        {
            _mediator = mediatorHandler;
            _cidadaoRepository = cidadaoRepository;
        }

        [HttpGet("/cidadao/ObterTodos")]
        public async Task<IEnumerable<Models.Cidadao>> ObterTodos()
        {
            return await _cidadaoRepository.ObterTodos();
        }

        [HttpGet("/cidadao/{id}")]
        public async Task<Models.Cidadao> ObterPorId(Guid id)
        {
            var c = await _cidadaoRepository.ObterPorId(id);
            return c;
        }


    }
}