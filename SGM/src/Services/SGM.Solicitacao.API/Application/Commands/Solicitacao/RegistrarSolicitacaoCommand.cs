using System;
using FluentValidation;
using SGM.Core.Messages;

namespace SGM.Solicitacao.API.Application.Commands
{
    public class RegistrarSolicitacaoCommand : Command
    {
        public Guid Id { get; private set; }
        public Guid IdSolicitante { get; private set; }
        public string CodDepartamento { get; private set; }
        public string Descricao { get; private set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEncerramento { get; set; }

        public RegistrarSolicitacaoCommand(Guid id, Guid idSolicitante, string descricao, string codDepartamento, string status, DateTime dataCadastro)
        {
            AggregateId = id;
            Id = id;
            IdSolicitante = idSolicitante;
            Descricao = descricao;
            CodDepartamento = codDepartamento;
            Status = status;
            DataCadastro = dataCadastro;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarSolicitacaoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarSolicitacaoValidation : AbstractValidator<RegistrarSolicitacaoCommand>
        {
            public RegistrarSolicitacaoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id da solicitação inválido");

                RuleFor(c => c.IdSolicitante)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cidadão inválido");

                RuleFor(c => c.Descricao)
                    .NotEmpty()
                    .WithMessage("A descrição da solicitação não foi informado");

                RuleFor(c => c.CodDepartamento)
                    .NotEmpty()
                    .WithMessage("O Departamento informado não é válido.");

                RuleFor(c => c.Status)
                    .NotEmpty()
                    .WithMessage("O Status informado não é válido.");

                RuleFor(c => c.DataCadastro)
                    .Must(DataAtualValida)
                    .WithMessage("A Data de Cadastro informada não é válida.");
            }

            protected static bool DataAtualValida(DateTime dt)
            {
                // data deve ser atual
                return (dt > DateTime.Now.AddMinutes(-1) && dt < DateTime.Now.AddMinutes(1));
            }

        }
    }
}