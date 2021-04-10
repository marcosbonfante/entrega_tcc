using System;
using SGM.Core.DomainObjects;

namespace SGM.OrdemServico.API.Models
{
    public class Ordem : Entity, IAggregateRoot
    {
        public Guid IdSolicitante { get; set; }
        public Guid? IdSolicitacao { get; set; }
        public string CodDepartamento { get; set; }
        public string Descricao { get; set; }
        public string Solucao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataSolucao { get; set; }

        protected Ordem() { }

        public Ordem(
            Guid id, Guid idSolicitante, Guid? idSolicitacao,
            string codDepartamento, string descricao, string solucao,
            DateTime dataCadastro, DateTime? dataSolucao)
        {
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