using GestionClientesMyM.Models;
using GestionClientesMyM.Services.Extras;
using GestionClientesMyM.Services.ProductosVersiones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionClientesMyM.Pages
{
    public class ExtraModel : PageModel
    {
        private ExtraService extraService;

        public ExtraModel(ExtraService extraService)
        {
            this.extraService = extraService;
        }

        [BindProperty]
        public List<Extra> Extras { get; set; } = new List<Extra>();
        [BindProperty]
        public Extra Extra { get; set; } = new Extra();
        public bool EsEdicionModal = true;

        public async Task OnGet()
        {
            Extras = await extraService.ObtenerTodo();
        }

        public async Task<IActionResult> OnPostSaveExtra()
        {
            if (Extra.Id == 0)
            {
                await extraService.Crear(Extra);
            }
            else
            {
                await extraService.Actualizar(Extra);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteExtra(int id)
        {
            var extra = await extraService.Eliminar(id);

            return RedirectToPage();
        }
    }
}
