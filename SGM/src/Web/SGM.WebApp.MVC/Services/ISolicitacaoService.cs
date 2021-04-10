using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGM.WebApp.MVC.Models;

namespace SGM.WebApp.MVC.Services
{
    public interface ISolicitacaoService
    {
        Task<SolicitacaoViewModel> Adicionar(SolicitacaoViewModel model);
        Task<SolicitacaoViewModel> Atualizar(SolicitacaoViewModel model);
        Task<IEnumerable<SolicitacaoViewModel>> ObterTodosCidadao(Guid CidadaoId,string status);
        Task<IEnumerable<SolicitacaoViewModel>> ObterTodos(string status);
        Task<SolicitacaoViewModel> ObterPorId(Guid id);
    }

}