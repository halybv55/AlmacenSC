using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Interfaces
{
    public interface ICargaProductoRepository
    {
        Task<IEnumerable<CargaProducto>> GetAllAsync();
        Task<CargaProducto?> GetByIdAsync(int id);
        Task AddAsync(CargaProducto entity);
        Task DeleteAsync(int id);
    }
}
