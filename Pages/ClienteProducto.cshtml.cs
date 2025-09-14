using GestionClientesMyM.Services.ClientesProductos;
using GestionClientesMyM.Services.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionClientesMyM.Pages
{
    public class ClienteProductoModel : PageModel
    {
        private ClienteProductoService _clienteProductoService;

        public ClienteProductoModel(ClienteProductoService clienteProductoService)
        {
            _clienteProductoService = clienteProductoService;
        }

        [BindProperty]
        public List<ItemClienteProductoOutput> ClientesProductos { get; set; } = new List<ItemClienteProductoOutput>();

        public async Task<IActionResult> OnGet(int id)
        {
            ClientesProductos = await _clienteProductoService.Buscar(id);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteClienteProducto(int id)
        {
            var clienteProducto = await _clienteProductoService.Eliminar(id);

            return RedirectToPage("ClienteProducto", new { id = clienteProducto.ClienteId });
        }
    }
}
