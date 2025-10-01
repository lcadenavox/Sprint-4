using Sprint_4.Models;
using Sprint_4.Data;
using Microsoft.EntityFrameworkCore;

namespace Sprint_4.Services
{
    public class DepositoService
    {
        private readonly AppDbContext _context;
        public DepositoService(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Deposito> GetAll() => _context.Depositos.AsNoTracking();
        public async Task<Deposito?> GetByIdAsync(int id) => await _context.Depositos.FindAsync(id);
        public async Task<Deposito> AddAsync(Deposito deposito)
        {
            _context.Depositos.Add(deposito);
            await _context.SaveChangesAsync();
            return deposito;
        }
        public async Task<bool> UpdateAsync(int id, Deposito deposito)
        {
            var entity = await _context.Depositos.FindAsync(id);
            if (entity == null) return false;
            entity.Nome = deposito.Nome;
            entity.Endereco = deposito.Endereco;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Depositos.FindAsync(id);
            if (entity == null) return false;
            _context.Depositos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}