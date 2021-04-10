using System;
using FluentValidation;
using SGM.Core.Messages;

namespace SGM.OrdemServico.API.Application.Commands
{
    public class RegistrarOrdemCommand : Command
    {
        public Guid Id { get; private set; }
        public Guid IdSolicitante { get; set; }
        public Guid? IdSolicitacao { get; set; }
        public string CodDepartamento { get; private set; }
        public string Descricao { get; private set; }
        public string Solucao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataSolucao { get; set; }


        public RegistrarOrdemCommand(
            Guid id, Guid idSolicitante, Guid? idSolicitacao,
            string codDepartamento, string descricao, string solucao,
            DateTime dataCadastro, DateTime? dataSolucao)
        {
            AggregateId = id;
            Id = id;
            IdSolicitante = idSolicitante;
            IdSolicitacao = idSolicitacao;
            CodDepartamento = codDepartamento;
            Descricao = descricao;
            Solucao = solucao;
            DataCadastro = dataCadastro;
            DataSolucao = dataSolucao;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarOrdemValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarOrdemValidation : AbstractValidator<RegistrarOrdemCommand>
        {
            public RegistrarOrdemValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id da Ordem inválido");

                RuleFor(c => c.IdSolicitante)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cidadão inválido");

                RuleFor(c => c.Descricao)
                    .NotEmpty()
                    .WithMessage("A descrição da Ordem não foi informado");

                RuleFor(c => c.CodDepartamento)
                    .NotEmpty()
                    .WithMessage("O Departamento para esta Ordem informado não é válido.");

                RuleFor(c => c.DataCadastro)
                    .Must(DataAtualValida)
                    .WithMessage("A Data de Cadastro da Ordem informada não é válida.");
            }

            protected static bool DataAtualValida(DateTime dt)
            {
                // data deve ser atual
                return (dt > DateTime.Now.AddMinutes(-1) && dt < DateTime.Now.AddMinutes(1));
            }

        }

    }
}