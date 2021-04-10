using System;
using SGM.Core.DomainObjects;

namespace SGM.Solicitacao.API.Models
{
    public class Solicitacao : Entity, IAggregateRoot
    {
        public Guid IdSolicitante { get; set; }
        public string CodDepartamento { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEncerramento { get; set; }

        protected Solicitacao() { }

        public Solicitacao(Guid id, Guid cidadaoId, string codDepartamento, string descricao, string status, DateTime dataCadastro, DateTime? dataEncerramento)
        {
            Id = id;
            IdSolicitante = cidadaoId;
            CodDepartamento = codDepartamento;
            Descricao = descricao;
            Status = status;
            DataCadastro = dataCadastro;
            DataEncerramento = dataEncerramento;
        }

    }
}