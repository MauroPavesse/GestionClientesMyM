namespace GestionClientesMyM.Models
{
    public class ClienteProductoExtra
    {
        public int Id { get; set; }
        public int ClienteProductoId { get; set; } 
        public ClienteProducto? ClienteProducto { get; set; }
        public int ExtraId { get; set; }
        public Extra? Extra { get; set; }
    }
}
