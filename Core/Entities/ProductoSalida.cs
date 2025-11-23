namespace AlmacenSC.Core.Entities
{
    public class ProductoSalida
    {
        public int Id { get; set; }

        public int ProductoEntradaId { get; set; }
        public ProductoEntrada ProductoEntrada { get; set; }

        public int Cantidad { get; set; }
        public DateTime FechaSalida { get; set; } = DateTime.UtcNow;

        public string? SolicitadoPor { get; set; }
    }

}
