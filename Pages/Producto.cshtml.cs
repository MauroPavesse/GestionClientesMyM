using GestionClientesMyM.Models;
using GestionClientesMyM.Services.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionClientesMyM.Pages
{
    public class ProductoModel : PageModel
    {
        private ProductoService productoService;

        public ProductoModel(ProductoService productoService)
        {
            this.productoService = productoService;
        }

        [BindProperty]
        public List<Producto> Productos { get; set; } = new List<Producto>();

        public async Task OnGet()
        {
            Productos = await productoService.ObtenerTodo();
        }

        public async Task<IActionResult> OnPostDeleteProducto(int id)
        {
            var producto = await productoService.Eliminar(id);

            return RedirectToPage();
        }
    }
}
