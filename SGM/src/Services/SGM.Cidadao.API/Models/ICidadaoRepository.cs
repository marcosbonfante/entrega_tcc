using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGM.Core.Data;

namespace SGM.Cidadao.API.Models
{
    public interface ICidadaoRepository : IRepository<Cidadao>
    {
        void Adicionar(Cidadao cliente);
        Task<IEnumerable<Cidadao>> ObterTodos();
        Task<Cidadao> ObterPorId(Guid id);
        Task<Cidadao> ObterPorCpf(string cpf);
        void AdicionarEndereco(Endereco endereco);
        Task<Endereco> ObterEnderecoPorId(Guid id);
    }
}