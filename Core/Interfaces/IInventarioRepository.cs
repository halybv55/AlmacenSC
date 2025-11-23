using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Interfaces
{
    public interface IInventarioRepository
    {
        Task<IEnumerable<Inventario>> GetAllAsync();
        Task<Inventario?> GetByIdAsync(int id);
        Task<Inventario?> GetByCodigoAsync(string codigo);

        Task<IEnumerable<Inventario>> SearchByNombreAsync(string nombre);

        Task AddAsync(Inventario entity);
        Task UpdateAsync(Inventario entity);

        Task<bool> DeleteAsync(int id); // ← EL BUENO
    }
}
