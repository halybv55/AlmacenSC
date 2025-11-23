using Microsoft.AspNetCore.Mvc;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Core.Entities;
using AlmacenSC.Core.DTOs;

namespace AlmacenSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoEntradaController : ControllerBase
    {
        private readonly IProductoEntradaRepository _repoEntrada;
        private readonly IInventarioRepository _repoInv;

        public ProductoEntradaController(
            IProductoEntradaRepository repoEntrada,
            IInventarioRepository repoInv)
        {
            _repoEntrada = repoEntrada;
            _repoInv = repoInv;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoEntradaDto dto)
        {
            var entity = new ProductoEntrada
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Cantidad = dto.Cantidad,
                StockMinimo = dto.StockMinimo,
                StockMaximo = dto.StockMaximo,
                FechaEntrada = dto.FechaEntrada,
                FechaVencimiento = dto.FechaVencimiento
            };

            await _repoEntrada.AddAsync(entity);

            // 🔥 CREAR/ACTUALIZAR INVENTARIO AUTOMÁTICAMENTE
            var existente = await _repoInv.GetByCodigoAsync(dto.Codigo);

            if (existente == null)
            {
                await _repoInv.AddAsync(new Inventario
                {
                    ProductoEntradaId = entity.Id,
                    StockActual = dto.Cantidad
                });
            }
            else
            {
                existente.StockActual += dto.Cantidad;
                await _repoInv.UpdateAsync(existente);
            }

            return Ok("Entrada registrada y stock actualizado.");
        }
    }
}
