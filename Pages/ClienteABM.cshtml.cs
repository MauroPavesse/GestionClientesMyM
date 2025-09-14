using GestionClientesMyM.Data;
using GestionClientesMyM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionClientesMyM.Pages
{
    public class ClienteModel : PageModel
    {
        private readonly AppDbContext _context;

        public ClienteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; } = new Cliente();
        public bool EsEdicion => Cliente?.Id > 0;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                Cliente = new Cliente();
            }
            else
            {
                Cliente = _context.Clientes.Find(id);
                if (Cliente == null)
                    return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Cliente.Id > 0)
            {
                _context.Clientes.Update(Cliente);
            }
            else
            {
                _context.Clientes.Add(Cliente);
            }

            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
