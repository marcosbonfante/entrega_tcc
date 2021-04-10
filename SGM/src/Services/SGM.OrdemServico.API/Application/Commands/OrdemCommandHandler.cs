using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SGM.OrdemServico.API.Application.Events;
using SGM.OrdemServico.API.Models;
using SGM.Core.Messages;

namespace SGM.OrdemServico.API.Application.Commands
{
    public class OrdemCommandHandler : CommandHandler,
        IRequestHandler<RegistrarOrdemCommand, ValidationResult>,
        IRequestHandler<AtualizarOrdemCommand, ValidationResult>
    {
        private readonly IOrdemRepository _ordemRepository;

        public OrdemCommandHandler(IOrdemRepository ordemRepository)
        {
            _ordemRepository = ordemRepository;
        }

        #region Ordem

        public async Task<ValidationResult> Handle(RegistrarOrdemCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var ordem = new Ordem(
                message.Id, message.IdSolicitante, message.IdSolicitacao,
                message.CodDepartamento, message.Descricao, message.Solucao, message.DataCadastro, message.DataSolucao);

            _ordemRepository.Adicionar(ordem);

            ordem.AdicionarEvento(
                new OrdemRegistradaEvent(
                    message.Id, message.IdSolicitante, message.IdSolicitacao,
                    message.CodDepartamento, message.Descricao, message.Solucao,
                    message.DataCadastro, message.DataSolucao));

            return await PersistirDados(_ordemRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarOrdemCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var ordemExistente = await _ordemRepository.ObterPorId(message.Id);

            if (ordemExistente != null)
            {
                ordemExistente.Solucao = message.Solucao;
                ordemExistente.DataSolucao = message.DataSolucao;

                _ordemRepository.Atualizar(ordemExistente);

                ordemExistente.AdicionarEvento(
                    new OrdemAtualizadaEvent(
                        message.Id, message.IdSolicitante, message.IdSolicitacao,
                        message.CodDepartamento, message.Descricao, message.Solucao,
                        message.DataCadastro, message.DataSolucao));

                return await PersistirDados(_ordemRepository.UnitOfWork);
            }
            else
            {
                AdicionarErro("Ordem não localizada.");
                return ValidationResult;
            }
        }

        #endregion


    }
}