using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using Microsoft.EntityFrameworkCore;

namespace AlmacenSC.Infraestructura.Repositorios
{
    public class CargaProductoRepository : ICargaProductoRepository
    {
        private readonly AlmacenSCContext _context;

        public CargaProductoRepository(AlmacenSCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CargaProducto>> GetAllAsync()
        {
            return await _context.CargaProducto
                .Include(x => x.Detalles)
                .ThenInclude(d => d.ProductoEntrada)
                .ToListAsync();
        }

        public async Task<CargaProducto?> GetByIdAsync(int id)
        {
            return await _context.CargaProducto
                .Include(x => x.Detalles)
                .ThenInclude(d => d.ProductoEntrada)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(CargaProducto entity)
        {
            await _context.CargaProducto.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing != null)
            {
                _context.CargaProducto.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
