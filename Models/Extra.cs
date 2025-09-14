using System.ComponentModel.DataAnnotations.Schema;

namespace GestionClientesMyM.Models
{
    // Esta clase viene a ser todo lo que se le puede agregar al SERVICIO, por ejemplo: dominio personalizado, etc
    public class Extra
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        public ICollection<ClienteProductoExtra> ClientesProductosExtras { get; set; } = new List<ClienteProductoExtra>();
    }
}
