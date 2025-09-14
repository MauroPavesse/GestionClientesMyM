using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Cliente> Clientes { get; set; }

        public async Task OnGetAsync()
        {
            Clientes = _context.Clientes
                .Include(c => c.ClientesProductos)
                    .ThenInclude(cp => cp.Producto)
                .ToList();
        }
    }
}
