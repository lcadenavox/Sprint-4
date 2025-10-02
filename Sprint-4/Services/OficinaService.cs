using Sprint_4.Models;
using Sprint_4.Data;
using Microsoft.EntityFrameworkCore;

namespace Sprint_4.Services
{
    public class OficinaService
    {
        private readonly AppDbContext _context;
        public OficinaService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Oficina> GetAll() => _context.Oficinas.AsNoTracking();
        public async Task<Oficina?> GetByIdAsync(int id) => await _context.Oficinas.FindAsync(id);
        public async Task<Oficina> AddAsync(Oficina oficina)
        {
            _context.Oficinas.Add(oficina);
            await _context.SaveChangesAsync();
            return oficina;
        }
        public async Task<bool> UpdateAsync(int id, Oficina oficina)
        {
            var entity = await _context.Oficinas.FindAsync(id);
            if (entity == null) return false;
            entity.Nome = oficina.Nome;
            entity.Endereco = oficina.Endereco;
            entity.Telefone = oficina.Telefone;
            entity.Especialidades = oficina.Especialidades;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Oficinas.FindAsync(id);
            if (entity == null) return false;
            _context.Oficinas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}