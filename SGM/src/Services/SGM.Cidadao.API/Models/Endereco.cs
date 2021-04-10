using System;
using SGM.Core.DomainObjects;

namespace SGM.Cidadao.API.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid CidadaoId { get; private set; }

        // EF Relation
        public Cidadao Cidadao { get; protected set; }

        public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid cidadaoId)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            CidadaoId = cidadaoId;
        }

        // EF Constructor
        protected Endereco() { }
    }
}