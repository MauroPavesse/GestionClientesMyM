namespace GestionClientesMyM.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Persona { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
        public string Pagina { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Localidad { get; set; } = string.Empty;

        public ICollection<ClienteProducto> ClientesProductos { get; set; } = new List<ClienteProducto>();
    }
}
