namespace AlmacenSC.Core.DTOs
{
    public class InventarioDto
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int StockActual { get; set; }

        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        public DateTime UltimaActualizacion { get; set; }
    }

}
