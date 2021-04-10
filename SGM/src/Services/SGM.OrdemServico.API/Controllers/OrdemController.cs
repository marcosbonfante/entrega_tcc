using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGM.Core.DomainObjects;
using SGM.OrdemServico.API.Models;
using SGM.Core.Mediator;
using SGM.Core.Messages.Integration;
using SGM.WebAPI.Core.Controllers;
using SGM.OrdemServico.API.Application.Commands;
using SGM.WebAPI.Core.Identidade;
using SGM.MessageBus;
using FluentValidation.Results;
using System.Linq;

namespace SGM.OrdemServico.API.Controllers
{
    [Authorize]
    public class OrdemController : MainController
    {

        private readonly IMediatorHandler _mediator;
        private readonly IOrdemRepository _ordemRepository;
        private readonly IMessageBus _bus;

        public OrdemController(
            IOrdemRepository ordemRepository,
            IMediatorHandler mediatorHandler,
            IMessageBus bus)
        {
            _mediator = mediatorHandler;
            _ordemRepository = ordemRepository;
            _bus = bus;
        }

        [HttpPost("ordem/registrar")]
        public async Task<ActionResult> Registrar(Ordem ordem)
        {
            if (!ModelState.IsValid) return CustomResponse();

            ordem.Id = Guid.NewGuid();
            ordem.DataCadastro = DateTime.Now;

            var result = await _mediator.EnviarComando(
                new RegistrarOrdemCommand(
                    ordem.Id, ordem.IdSolicitante, ordem.IdSolicitacao,
                    ordem.CodDepartamento, ordem.Descricao, ordem.Solucao,
                    ordem.DataCadastro, ordem.DataSolucao));

            if (result.IsValid)
            {

                var solicitacaoResult = await AtualizarSolicitacaoOrdem(ordem);

                if (!solicitacaoResult.ValidationResult.IsValid)
                {
                    _ordemRepository.Deletar(ordem);
                    return CustomResponse(solicitacaoResult.ValidationResult);
                }

                return CustomResponse();
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.ErrorMessage);
            }
            return CustomResponse();

        }

        [HttpPost("ordem/atualizar")]
        public async Task<ActionResult> Atualizar(Ordem ordem)
        {
            if (!ModelState.IsValid) return CustomResponse();

            ordem.DataSolucao = DateTime.Now;

            var result = await _mediator.EnviarComando(
                new AtualizarOrdemCommand(
                    ordem.Id, ordem.IdSolicitante, ordem.IdSolicitacao,
                    ordem.CodDepartamento, ordem.Descricao, ordem.Solucao,
                    ordem.DataCadastro, ordem.DataSolucao));

            if (result.IsValid)
            {

                var solicitacaoResult = await AtualizarSolicitacaoOrdem(ordem);

                if (!solicitacaoResult.ValidationResult.IsValid)
                {
                    _ordemRepository.Deletar(ordem);
                    return CustomResponse(solicitacaoResult.ValidationResult);
                }

                return CustomResponse();
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.ErrorMessage);
            }
            return CustomResponse();

        }


        [HttpGet("/ordem/solicitacao/{id}")]
        public async Task<IEnumerable<Ordem>> ObterTodosSolicitacao(Guid id)
        {
            return await _ordemRepository.ObterTodosSolicitacao(id);
        }


        [HttpGet("/ordem/")]
        public async Task<IEnumerable<Ordem>> ObterTodos()
        {
            return await _ordemRepository.ObterTodos();
        }

        [HttpGet("/ordem/{id}")]
        public async Task<Ordem> ObterPorId(Guid id)
        {
            return await _ordemRepository.ObterPorId(id);
        }










        private async Task<ResponseMessage> AtualizarSolicitacaoOrdem(Ordem ordem)
        {
            if (ordem.IdSolicitacao != null)
            {
                var status = StatusSolicitacao.Atendimento;
                var ordemExistente = await _ordemRepository.ObterTodosSolicitacao((Guid)ordem.IdSolicitacao);

                var l = (from x in ordemExistente where x.DataSolucao == null select x).ToList();
                if (l == null || l.Count == 0) { status = StatusSolicitacao.Encerrada; }

                var solicitacaoAtualizada = new SolicitacaoAtualizadaIntegrationEvent(
                    (Guid)ordem.IdSolicitacao, ordem.CodDepartamento, status);

                try
                {
                    return await _bus.RequestAsync<SolicitacaoAtualizadaIntegrationEvent, ResponseMessage>(solicitacaoAtualizada);
                }
                catch (Exception ex)
                {
                    AdicionarErroProcessamento(ex.Message);
                    return new ResponseMessage(new ValidationResult());
                }
            }
            else { return new ResponseMessage(new ValidationResult()); }
        }

    }
}
