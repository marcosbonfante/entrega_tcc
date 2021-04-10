using System;
using SGM.Core.Messages;

namespace SGM.Cidadao.API.Application.Events
{
    public class CidadaoRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public CidadaoRegistradoEvent(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }
    }
}