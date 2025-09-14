using GestionClientesMyM.Models;
using GestionClientesMyM.Services.ClientesProductos;
using GestionClientesMyM.Services.Pagos;
using GestionClientesMyM.Services.ProductosVersiones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionClientesMyM.Pages
{
    public class PagosModel : PageModel
    {
        private PagoService pagoService;
        private ClienteProductoService clienteProductoService;

        public PagosModel(PagoService pagoService, ClienteProductoService clienteProductoService)
        {
            this.pagoService = pagoService;
            this.clienteProductoService = clienteProductoService;
        }

        [BindProperty]
        public List<Pago> Pagos { get; set; } = new List<Pago>();
        [BindProperty]
        public Pago Pago { get; set; } = new Pago();
        public bool EsEdicionModal = true;

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Pagos = await pagoService.ObtenerPorClienteProductoId(id ?? 0);
            }
            Pago.ClienteProductoId = id ?? 0;

            return Page();
        }

        public async Task<IActionResult> OnPostSavePago()
        {
            if (Pago.Id == 0)
            {
                await pagoService.Crear(Pago);

                var clienteProducto = await clienteProductoService.ObtenerPorId(Pago.ClienteProductoId);
                clienteProducto.FechaProximoPago = clienteProducto.FechaProximoPago.AddMonths(1);
                await clienteProductoService.Actualizar(clienteProducto);
            }
            else
            {
                await pagoService.Actualizar(Pago);
            }

            return RedirectToPage(new { id = Pago.ClienteProductoId });
        }

        public async Task<IActionResult> OnPostDeletePago(int id)
        {
            var pago = await pagoService.Eliminar(id);

            var clienteProducto = await clienteProductoService.ObtenerPorId(pago.ClienteProductoId);
            clienteProducto.FechaProximoPago = clienteProducto.FechaProximoPago.AddMonths(-1);
            await clienteProductoService.Actualizar(clienteProducto);

            return RedirectToPage(new { id = pago.ClienteProductoId });
        }
    }
}
