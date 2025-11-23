using AlmacenSC.Core.DTOs;
using AlmacenSC.Core.Entities;
using AlmacenSC.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoSalidaController : ControllerBase
    {
        private readonly IProductoSalidaRepository _repoSalida;
        private readonly IInventarioRepository _repoInv;
        private readonly IAlertaReabastecimientoRepository _repoAlert;
        private readonly ICargaProductoRepository _repoCarga;

        public ProductoSalidaController(
            IProductoSalidaRepository repoSalida,
            IInventarioRepository repoInv,
            IAlertaReabastecimientoRepository repoAlert,
            ICargaProductoRepository repoCarga)
        {
            _repoSalida = repoSalida;
            _repoInv = repoInv;
            _repoAlert = repoAlert;
            _repoCarga = repoCarga;
        }

        [HttpPost("{solicitadoPor}")]
        public async Task<IActionResult> RegistrarSalida(string solicitadoPor, List<ProductoSalidaDto> productos)
        {
            // 🔥 Crear una sola carga
            var carga = new CargaProducto
            {
                SolicitadoPor = solicitadoPor,
                Detalles = new()
            };

            foreach (var dto in productos)
            {
                var inv = await _repoInv.GetByCodigoAsync(dto.Codigo);
                if (inv == null)
                    return NotFound($"Producto {dto.Codigo} no existe en inventario.");

                if (inv.StockActual < dto.CantidadSolicitada)
                    return BadRequest($"Stock insuficiente para {dto.Codigo}");

                // Registrar salida fisica
                var salida = new ProductoSalida
                {
                    ProductoEntradaId = inv.ProductoEntradaId,
                    Cantidad = dto.CantidadSolicitada,
                    SolicitadoPor = solicitadoPor
                };
                await _repoSalida.AddAsync(salida);

                // Descontar stock
                inv.StockActual -= dto.CantidadSolicitada;
                await _repoInv.UpdateAsync(inv);

                // Crear alerta si baja del minimo
                if (inv.StockActual <= inv.ProductoEntrada.StockMinimo)
                {
                    await _repoAlert.AddAsync(new AlertaReabastecimiento
                    {
                        InventarioId = inv.Id,
                        Mensaje = $"Stock bajo para {inv.ProductoEntrada.Nombre}"
                    });
                }

                // Agregar detalle a la carga
                carga.Detalles.Add(new CargaProductoDetalle
                {
                    ProductoEntradaId = inv.ProductoEntradaId,
                    Cantidad = dto.CantidadSolicitada
                });
            }

            // 🔥 Guardar la carga completa con TODOS los productos
            await _repoCarga.AddAsync(carga);

            return Ok("Salida registrada y carga generada correctamente.");
        }
    }
}
