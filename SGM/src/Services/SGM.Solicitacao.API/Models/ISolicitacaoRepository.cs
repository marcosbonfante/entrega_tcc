using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGM.Core.Data;

namespace SGM.Solicitacao.API.Models
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        Task<IEnumerable<Solicitacao>> ObterTodos(string Status);
        Task<IEnumerable<Solicitacao>> ObterTodosCidadao(Guid CidadaoId, string Status);
        Task<Solicitacao> ObterPorId(Guid id);
        void Adicionar(Solicitacao solicitacao);
        void Atualizar(Solicitacao solicitacao);
    }
}