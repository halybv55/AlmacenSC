namespace AlmacenSC.Core.DTOs
{
    public class ProductoEntradaDto
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Cantidad { get; set; }

        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }

}
