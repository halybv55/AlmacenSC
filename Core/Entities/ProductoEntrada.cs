namespace AlmacenSC.Core.Entities
{
    public class ProductoEntrada
    {
        public int Id { get; set; }

        // Datos del producto registrado
        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public int Cantidad { get; set; }

        // Configuración de stock
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        // Fechas
        public DateTime FechaEntrada { get; set; } = DateTime.UtcNow;
        public DateTime? FechaVencimiento { get; set; }

        // Relación con inventario
        public Inventario Inventario { get; set; }
    }

}
