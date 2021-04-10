using System;
using FluentValidation;
using SGM.Core.Messages;

namespace SGM.Cidadao.API.Application.Commands
{
    public class RegistrarCidadaoCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegistrarCidadaoCommand(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarCidadaoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarCidadaoValidation : AbstractValidator<RegistrarCidadaoCommand>
        {
            public RegistrarCidadaoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cidadão inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do cidadão não foi informado");

                RuleFor(c => c.Cpf)
                    .Must(TerCpfValido)
                    .WithMessage("O CPF informado não é válido.");

                RuleFor(c => c.Email)
                    .Must(TerEmailValido)
                    .WithMessage("O e-mail informado não é válido.");
            }

            protected static bool TerCpfValido(string cpf)
            {
                return Core.DomainObjects.Cpf.Validar(cpf);
            }

            protected static bool TerEmailValido(string email)
            {
                return Core.DomainObjects.Email.Validar(email);
            }
        }
    }
}