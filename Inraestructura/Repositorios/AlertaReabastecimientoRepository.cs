using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using Microsoft.EntityFrameworkCore;

namespace AlmacenSC.Infraestructura.Repositorios
{
    public class AlertaReabastecimientoRepository : IAlertaReabastecimientoRepository
    {
        private readonly AlmacenSCContext _context;

        public AlertaReabastecimientoRepository(AlmacenSCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlertaReabastecimiento>> GetAllAsync()
        {
            return await _context.AlertaReabastecimiento
                .Include(x => x.Inventario)
                .ThenInclude(i => i.ProductoEntrada)
                .ToListAsync();
        }

        public async Task<AlertaReabastecimiento?> GetByIdAsync(int id)
        {
            return await _context.AlertaReabastecimiento
                .Include(x => x.Inventario)
                .ThenInclude(i => i.ProductoEntrada)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(AlertaReabastecimiento entity)
        {
            await _context.AlertaReabastecimiento.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AlertaReabastecimiento entity)
        {
            _context.AlertaReabastecimiento.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing != null)
            {
                _context.AlertaReabastecimiento.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
