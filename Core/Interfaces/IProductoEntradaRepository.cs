using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Interfaces
{
    public interface IProductoEntradaRepository
    {
        Task<IEnumerable<ProductoEntrada>> GetAllAsync();
        Task<ProductoEntrada?> GetByIdAsync(int id);
        Task AddAsync(ProductoEntrada entity);
        Task UpdateAsync(ProductoEntrada entity);
        Task DeleteAsync(int id);
    }
}
