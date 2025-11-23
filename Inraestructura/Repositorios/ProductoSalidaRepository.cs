using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using Microsoft.EntityFrameworkCore;

namespace AlmacenSC.Infraestructura.Repositorios
{
    public class ProductoSalidaRepository : IProductoSalidaRepository
    {
        private readonly AlmacenSCContext _context;

        public ProductoSalidaRepository(AlmacenSCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoSalida>> GetAllAsync()
        {
            return await _context.ProductoSalida
                .Include(x => x.ProductoEntrada)
                .ToListAsync();
        }

        public async Task<ProductoSalida?> GetByIdAsync(int id)
        {
            return await _context.ProductoSalida
                .Include(x => x.ProductoEntrada)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ProductoSalida entity)
        {
            await _context.ProductoSalida.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductoSalida entity)
        {
            _context.ProductoSalida.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing != null)
            {
                _context.ProductoSalida.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
