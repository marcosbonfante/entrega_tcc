using SGM.OrdemServico.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SGM.Core.Data;

namespace SGM.OrdemServico.API.Data.Repository
{
    public class OrdemRepository : IOrdemRepository
    {
        private readonly OrdemServicoContext _context;

        public OrdemRepository(OrdemServicoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Ordem>> ObterTodos()
        {
            return await _context.Ordens.AsNoTracking().ToListAsync();
        }

        public async Task<Ordem> ObterPorId(Guid id)
        {
            return await _context.Ordens.FindAsync(id);
        }
        public async Task<IEnumerable<Ordem>> ObterTodosSolicitacao(Guid idSolicitacao)
        {
            return await _context.Ordens
                     .Where(p => p.IdSolicitacao == idSolicitacao)
                     .OrderByDescending(p => p.DataCadastro)
                     .Select(p => p).AsNoTracking().ToListAsync();
        }
        public void Adicionar(Ordem ordem)
        {
            _context.Ordens.Add(ordem);
        }

        public void Atualizar(Ordem ordem)
        {
            _context.Ordens.Update(ordem);
        }

        public void Deletar(Ordem ordem)
        {
            _context.Ordens.Remove(ordem);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}