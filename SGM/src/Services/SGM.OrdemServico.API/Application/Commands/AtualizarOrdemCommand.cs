using System;
using FluentValidation;
using SGM.Core.Messages;

namespace SGM.OrdemServico.API.Application.Commands
{
    public class AtualizarOrdemCommand : Command
    {
        public Guid Id { get; private set; }
        public Guid IdSolicitante { get; set; }
        public Guid? IdSolicitacao { get; set; }
        public string CodDepartamento { get; private set; }
        public string Descricao { get; private set; }
        public string Solucao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataSolucao { get; set; }


        public AtualizarOrdemCommand(
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
            ValidationResult = new AtualizarOrdemValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarOrdemValidation : AbstractValidator<AtualizarOrdemCommand>
        {
            public AtualizarOrdemValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id da Ordem inválido");
                
                RuleFor(c => c.Solucao)
                    .NotEmpty()
                    .WithMessage("A Solução da Ordem não foi informado");

                RuleFor(c => c.DataSolucao)
                    .NotEmpty()
                    .WithMessage("A Data de Solução da Ordem informada não é válida.");
            }

        }
    
    }
}