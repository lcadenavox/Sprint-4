using Sprint_4.Models;
using Sprint_4.Data;
using Microsoft.EntityFrameworkCore;

namespace Sprint_4.Services
{
    public class MotoService
    {
        private readonly AppDbContext _context;
        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Moto> GetAll() => _context.Motos.AsNoTracking();
        public async Task<Moto?> GetByIdAsync(int id) => await _context.Motos.FindAsync(id);
        public async Task<Moto> AddAsync(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }
        public async Task<bool> UpdateAsync(int id, Moto moto)
        {
            var entity = await _context.Motos.FindAsync(id);
            if (entity == null) return false;
            entity.Marca = moto.Marca;
            entity.Modelo = moto.Modelo;
            entity.Ano = moto.Ano;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Motos.FindAsync(id);
            if (entity == null) return false;
            _context.Motos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}