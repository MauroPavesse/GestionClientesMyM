namespace GestionClientesMyM.Services.ClientesProductos
{
    public class ItemClienteProductoOutput
    {
        public int Id { get; set; }
        public string Producto { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public DateTime FechaInstalacion { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaProximaPago { get; set; }
        public List<ItemClienteProductoDetalleOutput> Detalles { get; set; } = new List<ItemClienteProductoDetalleOutput>();
    }
}
