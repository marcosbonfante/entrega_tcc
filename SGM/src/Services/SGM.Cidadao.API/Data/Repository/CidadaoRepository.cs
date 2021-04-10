using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGM.Cidadao.API.Models;
using SGM.Core.Data;

namespace SGM.Cidadao.API.Data.Repository
{
    public class CidadaoRepository : ICidadaoRepository
    {
        private readonly CidadaoContext _context;

       public CidadaoRepository(CidadaoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Models.Cidadao cliente)
        {
            _context.Cidadaos.Add(cliente);
        }

        public async Task<IEnumerable<Models.Cidadao>> ObterTodos()
        {
            return await _context.Cidadaos.AsNoTracking().ToListAsync();
        }

        public async Task<Models.Cidadao> ObterPorId(Guid id)
        {
            return await _context.Cidadaos.FindAsync(id);
        }

        public Task<Models.Cidadao> ObterPorCpf(string cpf)
        {
            return _context.Cidadaos.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public async Task<Endereco> ObterEnderecoPorId(Guid id)
        {
            return await _context.Enderecos.FirstOrDefaultAsync(e => e.CidadaoId == id);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}