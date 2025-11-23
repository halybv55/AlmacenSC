namespace AlmacenSC.Core.Entities
{
    public class CargaProducto
    {
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string SolicitadoPor { get; set; }

        public List<CargaProductoDetalle> Detalles { get; set; } = new();
    }

}
