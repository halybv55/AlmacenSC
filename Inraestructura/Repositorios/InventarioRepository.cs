using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using Microsoft.EntityFrameworkCore;

namespace AlmacenSC.Infraestructura.Repositorios
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly AlmacenSCContext _context;

        public InventarioRepository(AlmacenSCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventario>> GetAllAsync()
        {
            return await _context.Inventario
                .Include(x => x.ProductoEntrada)
                .ToListAsync();
        }

        public async Task<Inventario?> GetByIdAsync(int id)
        {
            return await _context.Inventario
                .Include(x => x.ProductoEntrada)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Inventario?> GetByCodigoAsync(string codigo)
        {
            return await _context.Inventario
                .Include(x => x.ProductoEntrada)
                .FirstOrDefaultAsync(x => x.ProductoEntrada.Codigo == codigo);
        }

        public async Task<IEnumerable<Inventario>> SearchByNombreAsync(string nombre)
        {
            return await _context.Inventario
                .Include(x => x.ProductoEntrada)
                .Where(x => x.ProductoEntrada.Nombre.ToLower()
                    .Contains(nombre.ToLower()))
                .ToListAsync();
        }

        public async Task AddAsync(Inventario entity)
        {
            await _context.Inventario.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Inventario entity)
        {
            _context.Inventario.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Inventario.FindAsync(id);

            if (entity == null)
                return false;

            _context.Inventario.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
