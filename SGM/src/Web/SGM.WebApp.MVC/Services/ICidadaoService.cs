using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGM.WebApp.MVC.Models;

namespace SGM.WebApp.MVC.Services
{
    public interface ICidadaoService
    {
        Task<IEnumerable<CidadaoViewModel>> ObterTodos();
        Task<CidadaoViewModel> ObterPorId(Guid id);
    }

}