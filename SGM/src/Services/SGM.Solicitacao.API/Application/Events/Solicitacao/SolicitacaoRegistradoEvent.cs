using System;
using SGM.Core.Messages;

namespace SGM.Solicitacao.API.Application.Events
{
    public class SolicitacaoRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid CidadaoId { get; private set; }
        public string Descricao { get; private set; }
        public string CodDepartamento { get; private set; }

        public SolicitacaoRegistradoEvent(Guid id,Guid cidadaoId, string descricao, string codDepartamento)
        {
            AggregateId = id;
            Id = id;
            CidadaoId = cidadaoId;
            Descricao = descricao;
            CodDepartamento = codDepartamento;
        }

    }
}