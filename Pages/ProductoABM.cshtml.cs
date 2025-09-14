using GestionClientesMyM.Models;
using GestionClientesMyM.Services.Productos;
using GestionClientesMyM.Services.ProductosVersiones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesMyM.Pages
{
    public class ProductoABMModel : PageModel
    {
        private ProductoService ProductoService;
        private ProductoVersionService ProductoVersionService;

        public ProductoABMModel(ProductoService productoService, ProductoVersionService productoVersionService)
        {
            ProductoService = productoService;
            ProductoVersionService = productoVersionService;
        }

        [BindProperty]
        public Producto Producto { get; set; } = new Producto();
        [BindProperty]
        public ItemProductoVersionInput ProductoVersion { get; set; } = new ItemProductoVersionInput();
        public bool EsEdicion => Producto?.Id > 0;
        public bool EsEdicionModal = true;

        public async Task<IActionResult> OnGet(int? id)
        {
            if(id == null)
            {
                Producto = new Producto();
            }
            else
            {
                Producto = await ProductoService.ObtenerPorId(id ?? 0);
                if (Producto == null)
                    return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Producto.Id > 0)
            {
                await ProductoService.Actualizar(Producto);
            }
            else
            {
                await ProductoService.Crear(Producto);
            }

            return RedirectToPage("Producto");
        }

        public async Task<IActionResult> OnPostSaveProductoVersion()
        {
            if (ProductoVersion.Id == 0)
            {
                await ProductoVersionService.Crear(ProductoVersion);
            }
            else
            {
                await ProductoVersionService.Actualizar(ProductoVersion);
            }

            return RedirectToPage(new { id = Producto.Id });
        }

        public async Task<IActionResult> OnPostDeleteProductoVersion(int id)
        {
            var productoVersion = await ProductoVersionService.Eliminar(id);

            return RedirectToPage(new { id = productoVersion.ProductoId });
        }
    }
}
