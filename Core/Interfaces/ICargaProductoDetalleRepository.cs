using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Interfaces
{
    public interface ICargaProductoDetalleRepository
    {
        Task<IEnumerable<CargaProductoDetalle>> GetByCargaIdAsync(int cargaId);
        Task AddAsync(CargaProductoDetalle entity);
        Task DeleteByCargaIdAsync(int cargaId);
    }
}
