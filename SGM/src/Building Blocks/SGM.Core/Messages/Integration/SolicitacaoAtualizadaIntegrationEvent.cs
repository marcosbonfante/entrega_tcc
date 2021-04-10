using System;

namespace SGM.Core.Messages.Integration
{
    public class SolicitacaoAtualizadaIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        //public Guid IdSolicitante { get; private set; }
        public string CodDepartamento { get; private set; }
        //public string Descricao { get; private set; }
        public string Status { get; set; }
        //public DateTime DataCadastro { get; set; }
        public DateTime? DataEncerramento { get; set; }


        //    public SolicitacaoAtualizadaIntegrationEvent(
        //Guid id, Guid idSolicitante, string codDepartamento, string descricao,
        //string status, DateTime dataCadastro, DateTime? dataEncerramento)

        public SolicitacaoAtualizadaIntegrationEvent(Guid id, string codDepartamento, string status)
        {
            DateTime? dataEncerramento = null;

            if (status == SGM.Core.DomainObjects.StatusSolicitacao.Pendente) { dataEncerramento = DateTime.Now; };

            AggregateId = id;
            Id = id;
            //IdSolicitante = idSolicitante;
            CodDepartamento = codDepartamento;
            //Descricao = descricao;
            Status = status;
            //DataCadastro = dataCadastro;
            DataEncerramento = dataEncerramento;
        }
    }
}