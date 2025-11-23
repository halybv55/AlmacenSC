using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Interfaces
{
    public interface IAlertaReabastecimientoRepository
    {
        Task<IEnumerable<AlertaReabastecimiento>> GetAllAsync();
        Task<AlertaReabastecimiento?> GetByIdAsync(int id);
        Task AddAsync(AlertaReabastecimiento entity);
        Task UpdateAsync(AlertaReabastecimiento entity);
        Task DeleteAsync(int id);
    }
}