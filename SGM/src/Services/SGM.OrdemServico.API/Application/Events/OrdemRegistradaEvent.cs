using System;
using SGM.Core.Messages;

namespace SGM.OrdemServico.API.Application.Events
{
    public class OrdemRegistradaEvent : Event
    {
        public Guid Id { get; private set; }
        public Guid IdSolicitante { get; private set; }
        public Guid? IdSolicitacao { get; private set; }
        public string CodDepartamento { get; private set; }
        public string Descricao { get; private set; }
        public string Solucao { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataSolucao { get; private set; }


        public OrdemRegistradaEvent(
            Guid id, Guid idSolicitante, Guid? idSolicitacao,
            string codDepartamento, string descricao, string solucao,
            DateTime dataCadastro, DateTime? dataSolucao
            )
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