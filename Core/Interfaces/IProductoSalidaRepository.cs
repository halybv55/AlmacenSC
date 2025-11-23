using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Interfaces
{
    public interface IProductoSalidaRepository
    {
        Task<IEnumerable<ProductoSalida>> GetAllAsync();
        Task<ProductoSalida?> GetByIdAsync(int id);
        Task AddAsync(ProductoSalida entity);
        Task UpdateAsync(ProductoSalida entity);
        Task DeleteAsync(int id);
    }
}
