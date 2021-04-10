using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGM.Core.Data;

namespace SGM.OrdemServico.API.Models
{
    public interface IOrdemRepository : IRepository<Ordem>
    {
        Task<IEnumerable<Ordem>> ObterTodos();
        Task<Ordem> ObterPorId(Guid id);
        Task<IEnumerable<Ordem>> ObterTodosSolicitacao(Guid idSolicitacao);
        void Adicionar(Ordem ordem);
        void Atualizar(Ordem ordem);
        void Deletar(Ordem ordem);

    }
}