using Sprint_4.Models;
using Sprint_4.Data;
using Microsoft.EntityFrameworkCore;

namespace Sprint_4.Services
{
    public class MecanicoService
    {
        private readonly AppDbContext _context;
        public MecanicoService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Mecanico> GetAll() => _context.Mecanicos.AsNoTracking();
        public async Task<Mecanico?> GetByIdAsync(int id) => await _context.Mecanicos.FindAsync(id);
        public async Task<Mecanico> AddAsync(Mecanico mecanico)
        {
            _context.Mecanicos.Add(mecanico);
            await _context.SaveChangesAsync();
            return mecanico;
        }
        public async Task<bool> UpdateAsync(int id, Mecanico mecanico)
        {
            var entity = await _context.Mecanicos.FindAsync(id);
            if (entity == null) return false;
            entity.Nome = mecanico.Nome;
            entity.Especialidade = mecanico.Especialidade;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Mecanicos.FindAsync(id);
            if (entity == null) return false;
            _context.Mecanicos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}