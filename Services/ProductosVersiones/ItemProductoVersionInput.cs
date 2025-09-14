namespace GestionClientesMyM.Services.ProductosVersiones
{
    public class ItemProductoVersionInput
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal CostoInicial { get; set; }
    }
}
