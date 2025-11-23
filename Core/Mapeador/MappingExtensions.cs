using AlmacenSC.Core.DTOs;
using AlmacenSC.Core.Entities;

namespace AlmacenSC.Core.Mapeadores
{
    public static class MappingExtensions
    {
        // ======================================================
        // PRODUCTO ENTRADA
        // ======================================================
        public static ProductoEntradaDto ToProductoEntradaDto(this ProductoEntrada p)
            => new ProductoEntradaDto
            {
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Cantidad = p.Cantidad,
                StockMinimo = p.StockMinimo,
                StockMaximo = p.StockMaximo,
                FechaEntrada = p.FechaEntrada,
                FechaVencimiento = p.FechaVencimiento
            };

        public static ProductoEntrada ToProductoEntrada(this ProductoEntradaDto dto)
            => new ProductoEntrada
            {
                Nombre = dto.Nombre,
                Codigo = dto.Codigo,
                Cantidad = dto.Cantidad,
                StockMinimo = dto.StockMinimo,
                StockMaximo = dto.StockMaximo,
                FechaEntrada = dto.FechaEntrada,
                FechaVencimiento = dto.FechaVencimiento
            };


        // ======================================================
        // INVENTARIO
        // ======================================================
        public static InventarioDto ToInventarioDto(this Inventario inv)
            => new InventarioDto
            {
                Codigo = inv.ProductoEntrada.Codigo,
                Nombre = inv.ProductoEntrada.Nombre,
                StockActual = inv.StockActual,
                StockMinimo = inv.ProductoEntrada.StockMinimo,
                StockMaximo = inv.ProductoEntrada.StockMaximo,
                UltimaActualizacion = DateTime.UtcNow
            };


        // ======================================================
        // PRODUCTO SALIDA
        // ======================================================
        public static ProductoSalidaDto ToProductoSalidaDto(this ProductoSalida s)
            => new ProductoSalidaDto
            {
                Codigo = s.ProductoEntrada.Codigo,
                Nombre = s.ProductoEntrada.Nombre,
                CantidadSolicitada = s.Cantidad,
                FechaSalida = s.FechaSalida
            };


        // ======================================================
        // ALERTA REABASTECIMIENTO
        // ======================================================
        public static AlertaReabastecimientoDto ToAlertaDto(this AlertaReabastecimiento a)
            => new AlertaReabastecimientoDto
            {
                Codigo = a.Inventario.ProductoEntrada.Codigo,
                Nombre = a.Inventario.ProductoEntrada.Nombre,
                StockActual = a.Inventario.StockActual,
                StockMinimo = a.Inventario.ProductoEntrada.StockMinimo,
                Estado = a.Atendida ? "Atendida" : "Pendiente",
                FechaNotificacion = a.FechaAlerta
            };


        // ======================================================
        // CARGA PRODUCTO DETALLE → DTO
        // ======================================================
        public static CargaProductoDto ToCargaProductoDto(this CargaProductoDetalle d)
            => new CargaProductoDto
            {
                Codigo = d.ProductoEntrada.Codigo,
                Nombre = d.ProductoEntrada.Nombre,
                Cantidad = d.Cantidad
            };


        // ======================================================
        // Helpers para listas
        // ======================================================
        public static IEnumerable<ProductoEntradaDto> ToProductoEntradaDtos(this IEnumerable<ProductoEntrada> items)
            => items.Select(i => i.ToProductoEntradaDto());

        public static IEnumerable<InventarioDto> ToInventarioDtos(this IEnumerable<Inventario> items)
            => items.Select(i => i.ToInventarioDto());

        public static IEnumerable<ProductoSalidaDto> ToProductoSalidaDtos(this IEnumerable<ProductoSalida> items)
            => items.Select(i => i.ToProductoSalidaDto());

        public static IEnumerable<AlertaReabastecimientoDto> ToAlertaDtos(this IEnumerable<AlertaReabastecimiento> items)
            => items.Select(i => i.ToAlertaDto());

        public static IEnumerable<CargaProductoDto> ToCargaProductoDtos(this IEnumerable<CargaProductoDetalle> items)
            => items.Select(i => i.ToCargaProductoDto());
    }
}
