using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SGM.Cidadao.API.Application.Events;
using SGM.Cidadao.API.Models;
using SGM.Core.Messages;

namespace SGM.Cidadao.API.Application.Commands
{
    public class CidadaoCommandHandler : CommandHandler,
        IRequestHandler<RegistrarCidadaoCommand, ValidationResult>,
        IRequestHandler<AdicionarEnderecoCommand, ValidationResult>
    {
        private readonly ICidadaoRepository _cidadaoRepository;

        public CidadaoCommandHandler(ICidadaoRepository cidadaoRepository)
        {
            _cidadaoRepository = cidadaoRepository;
        }

        #region Cidadão

        public async Task<ValidationResult> Handle(RegistrarCidadaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cidadao = new Models.Cidadao(message.Id, message.Nome, message.Email, message.Cpf);

            var cidadaoExistente = await _cidadaoRepository.ObterPorCpf(cidadao.Cpf.Numero);

            if (cidadaoExistente != null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _cidadaoRepository.Adicionar(cidadao);

            cidadao.AdicionarEvento(new CidadaoRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(_cidadaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var endereco = new Endereco(message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.Cep, message.Cidade, message.Estado, message.CidadaoId);
            _cidadaoRepository.AdicionarEndereco(endereco);

            return await PersistirDados(_cidadaoRepository.UnitOfWork);
        }

        #endregion


    }
}