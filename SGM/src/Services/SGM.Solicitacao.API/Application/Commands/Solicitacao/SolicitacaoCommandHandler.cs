using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SGM.Solicitacao.API.Application.Events;
using SGM.Solicitacao.API.Models;
using SGM.Core.Messages;

namespace SGM.Solicitacao.API.Application.Commands
{
    public class SolicitacaoCommandHandler : CommandHandler,
        IRequestHandler<RegistrarSolicitacaoCommand, ValidationResult>,
        IRequestHandler<AtualizarSolicitacaoCommand, ValidationResult>
    {
        private readonly ISolicitacaoRepository _solicitacaoRepository;

        public SolicitacaoCommandHandler(ISolicitacaoRepository solicitacaoRepository)
        {
            _solicitacaoRepository = solicitacaoRepository;
        }

        #region Solicitação

        public async Task<ValidationResult> Handle(RegistrarSolicitacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var solicitacao = new Models.Solicitacao(
                message.Id, message.IdSolicitante, message.CodDepartamento, message.Descricao,
                message.Status, message.DataCadastro, null);

            _solicitacaoRepository.Adicionar(solicitacao);

            solicitacao.AdicionarEvento(new SolicitacaoRegistradoEvent(message.Id, message.IdSolicitante, message.Descricao, message.CodDepartamento));

            return await PersistirDados(_solicitacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarSolicitacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var solicitacaoExistente = await _solicitacaoRepository.ObterPorId(message.Id);

            if (solicitacaoExistente != null)
            {
                solicitacaoExistente.Status = message.Status;
                solicitacaoExistente.CodDepartamento = message.CodDepartamento;
                solicitacaoExistente.DataEncerramento = message.DataEncerramento;

                _solicitacaoRepository.Atualizar(solicitacaoExistente);

                solicitacaoExistente.AdicionarEvento(
                    new SolicitacaoAtualizadaEvent(message.Id, message.CodDepartamento, message.Status, message.DataEncerramento));

                return await PersistirDados(_solicitacaoRepository.UnitOfWork);
            }
            else
            {
                AdicionarErro("Solicitação não localizada.");
                return ValidationResult;
            }
        }

        #endregion


    }
}