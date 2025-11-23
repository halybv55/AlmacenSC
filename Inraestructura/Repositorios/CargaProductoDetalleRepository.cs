using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using Microsoft.EntityFrameworkCore;

namespace AlmacenSC.Infraestructura.Repositorios
{
    public class CargaProductoDetalleRepository : ICargaProductoDetalleRepository
    {
        private readonly AlmacenSCContext _context;

        public CargaProductoDetalleRepository(AlmacenSCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CargaProductoDetalle>> GetByCargaIdAsync(int cargaId)
        {
            return await _context.CargaProductoDetalle
                .Include(x => x.ProductoEntrada)
                .Where(x => x.CargaProductoId == cargaId)
                .ToListAsync();
        }

        public async Task AddAsync(CargaProductoDetalle entity)
        {
            await _context.CargaProductoDetalle.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByCargaIdAsync(int cargaId)
        {
            var detalles = await _context.CargaProductoDetalle
                .Where(x => x.CargaProductoId == cargaId)
                .ToListAsync();

            _context.CargaProductoDetalle.RemoveRange(detalles);
            await _context.SaveChangesAsync();
        }
    }
}
