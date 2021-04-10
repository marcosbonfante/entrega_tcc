using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGM.WebApp.MVC.Models;

namespace SGM.WebApp.MVC.Services
{
    public interface IOrdemService
    {
        Task<IEnumerable<OrdemViewModel>> ObterTodos();
        Task<IEnumerable<OrdemViewModel>> ObterTodosSolicitacao(Guid IdSolicitacao);
        Task<OrdemViewModel> ObterPorId(Guid id);
        Task<ResponseResult> Adicionar(OrdemViewModel ordem);
        Task<ResponseResult> Atualizar(OrdemViewModel ordem);
    }

}