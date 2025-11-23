namespace AlmacenSC.Core.Entities
{
    public class CargaProductoDetalle
    {
        public int Id { get; set; }

        public int CargaProductoId { get; set; }
        public CargaProducto CargaProducto { get; set; }

        public int ProductoEntradaId { get; set; }
        public ProductoEntrada ProductoEntrada { get; set; }

        public int Cantidad { get; set; }
    }

}
