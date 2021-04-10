using SGM.Solicitacao.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGM.Core.Data;
using System.Linq;

namespace SGM.Solicitacao.API.Data.Repository
{
    public class SolicitacaoRepository : ISolicitacaoRepository
    {
        private readonly SolicitacaoContext _context;

        public SolicitacaoRepository(SolicitacaoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Models.Solicitacao>> ObterTodos(string Status)
        {
            if (string.IsNullOrEmpty(Status))
            {
                return await _context.Solicitacoes.AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Solicitacoes
                        .Where(p => p.Status == Status)
                        .OrderByDescending(p => p.DataCadastro)
                        .Select(p => p).AsNoTracking().ToListAsync();
            }
        }

        public async Task<IEnumerable<Models.Solicitacao>> ObterTodosCidadao(Guid CidadaoId, string Status)
        {
            if (string.IsNullOrEmpty(Status))
            {
                return await _context.Solicitacoes
                        .Where(p => p.IdSolicitante == CidadaoId)
                        .OrderByDescending(p => p.DataCadastro)
                        .Select(p => p).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Solicitacoes
                        .Where(p => p.IdSolicitante == CidadaoId && p.Status == Status)
                        .OrderByDescending(p => p.DataCadastro)
                        .Select(p => p).AsNoTracking().ToListAsync();
            }
        }

        public async Task<Models.Solicitacao> ObterPorId(Guid id)
        {
            return await _context.Solicitacoes.FindAsync(id);
        }

        public void Adicionar(Models.Solicitacao solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
        }

        public void Atualizar(Models.Solicitacao solicitacao)
        {
            _context.Solicitacoes.Update(solicitacao);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}