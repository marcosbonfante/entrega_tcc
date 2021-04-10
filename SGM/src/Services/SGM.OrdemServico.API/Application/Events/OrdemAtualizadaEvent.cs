using System;
using SGM.Core.Messages;

namespace SGM.OrdemServico.API.Application.Events
{
    public class OrdemAtualizadaEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid IdSolicitante { get; set; }
        public Guid? IdSolicitacao { get; set; }
        public string CodDepartamento { get; private set; }
        public string Descricao { get; private set; }
        public string Solucao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataSolucao { get; set; }

        public OrdemAtualizadaEvent(
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

    }
}