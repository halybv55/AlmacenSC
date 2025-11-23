using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using Microsoft.EntityFrameworkCore;

namespace AlmacenSC.Infraestructura.Repositorios
{
    public class ProductoEntradaRepository : IProductoEntradaRepository
    {
        private readonly AlmacenSCContext _context;

        public ProductoEntradaRepository(AlmacenSCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoEntrada>> GetAllAsync()
        {
            return await _context.ProductoEntrada
                .Include(x => x.Inventario)
                .ToListAsync();
        }

        public async Task<ProductoEntrada?> GetByIdAsync(int id)
        {
            return await _context.ProductoEntrada
                .Include(x => x.Inventario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ProductoEntrada entity)
        {
            await _context.ProductoEntrada.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductoEntrada entity)
        {
            _context.ProductoEntrada.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing != null)
            {
                _context.ProductoEntrada.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
