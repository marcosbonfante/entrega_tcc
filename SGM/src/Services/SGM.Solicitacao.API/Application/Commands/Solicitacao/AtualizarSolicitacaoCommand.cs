using System;
using FluentValidation;
using SGM.Core.Messages;

namespace SGM.Solicitacao.API.Application.Commands
{
    public class AtualizarSolicitacaoCommand : Command
    {
        public Guid Id { get; private set; }
        public Guid IdSolicitante { get; private set; }
        public string CodDepartamento { get; private set; }
        public string Descricao { get; private set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEncerramento { get; set; }

        public AtualizarSolicitacaoCommand(
            Guid id,Guid idSolicitante, string codDepartamento, string descricao,
            string status, DateTime dataCadastro, DateTime? dataEncerramento)
        {
            AggregateId = id;
            Id = id;
            IdSolicitante = idSolicitante;
            CodDepartamento = codDepartamento;
            Descricao = descricao;
            Status = status;
            DataCadastro = dataCadastro;
            DataEncerramento = dataEncerramento;
        }

        public AtualizarSolicitacaoCommand(Guid id, string codDepartamento,string status)
        {
            AggregateId = id;
            Id = id;
            CodDepartamento = codDepartamento;
            Status = status;
        }


        public override bool EhValido()
        {
            ValidationResult = new AtualizarSolicitacaoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarSolicitacaoValidation : AbstractValidator<AtualizarSolicitacaoCommand>
        {
            public AtualizarSolicitacaoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id da solicitação inválido");

                RuleFor(c => c.CodDepartamento)
                    .NotEmpty()
                    .WithMessage("O Departamento informado não é válido.");

                RuleFor(c => c.Status)
                    .NotEmpty()
                    .WithMessage("O Status informado não é válido.");
            }

        }
    
    }
}