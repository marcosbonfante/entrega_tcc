using System;
using SGM.Core.Messages;

namespace SGM.Solicitacao.API.Application.Events
{
    public class SolicitacaoAtualizadaEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid CidadaoId { get; private set; }
        public string Descricao { get; private set; }
        public string CodDepartamento { get; private set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEncerramento { get; set; }

        public SolicitacaoAtualizadaEvent(
            Guid id, string codDepartamento,string status, DateTime? dataEncerramento)
        {
            AggregateId = id;
            Id = id;
            CodDepartamento = codDepartamento;
            Status = status;
            DataEncerramento = dataEncerramento;
        }

    }
}