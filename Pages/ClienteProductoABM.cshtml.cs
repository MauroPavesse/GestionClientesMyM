using GestionClientesMyM.Models;
using GestionClientesMyM.Services.Clientes;
using GestionClientesMyM.Services.ClientesProductos;
using GestionClientesMyM.Services.ClientesProductosExtras;
using GestionClientesMyM.Services.Extras;
using GestionClientesMyM.Services.Productos;
using GestionClientesMyM.Services.ProductosVersiones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionClientesMyM.Pages
{
    public class ClienteProductoABMModel : PageModel
    {
        private ProductoVersionService productoVersionService;
        private ProductoService productoService;
        private ClienteService clienteService;
        private ClienteProductoService clienteProductoService;
        private ExtraService extraService;
        private ClienteProductoExtraService clienteProductoExtraService;

        public ClienteProductoABMModel(ProductoVersionService productoVersionService, ProductoService productoService, ClienteService clienteService, ClienteProductoService clienteProductoService, ExtraService extraService, ClienteProductoExtraService clienteProductoExtraService)
        {
            this.productoVersionService = productoVersionService;
            this.productoService = productoService;
            this.clienteService = clienteService;
            this.clienteProductoService = clienteProductoService;
            this.extraService = extraService;
            this.clienteProductoExtraService = clienteProductoExtraService;
        }

        [BindProperty]
        public ClienteProducto ClienteProducto { get; set; } = new ClienteProducto();
        public bool EsEdicion => ClienteProducto?.Id > 0;
        public SelectList Clientes { get; set; } = null!;
        public SelectList Productos { get; set; } = null!;
        public SelectList Versiones { get; set; } = null!;
        public SelectList Extras { get; set; } = null!;
        [BindProperty]
        public List<int> ExtrasSeleccionadosIds { get; set; } = new();

        public async Task<IActionResult> OnGet(int? id)
        {
            var clientes = await clienteService.ObtenerDatosCombo();
            var ProductosDatos = await productoService.ObtenerDatosCombo();
            var VersionesDatos = await productoVersionService.ObtenerDatosCombo();
            var ExtrasDatos = await extraService.ObtenerDatosCombo();

            Clientes = new SelectList(clientes, "Id", "Empresa");
            Productos = new SelectList(ProductosDatos, "Id", "Nombre");
            Versiones = new SelectList(VersionesDatos, "Id", "Nombre");
            Extras = new SelectList(ExtrasDatos, "Id", "Nombre");

            if (id == null)
            {
                ClienteProducto = new ClienteProducto();
                ClienteProducto.FechaInstalacion = DateTime.Today;
                ClienteProducto.FechaProximoPago = DateTime.Today;
            }
            else
            {
                ClienteProducto = await clienteProductoService.ObtenerPorId(id ?? 0);
                if (ClienteProducto == null)
                    return NotFound();
                ExtrasSeleccionadosIds = ClienteProducto.ClientesProductosExtras.Select(x => x.ExtraId).ToList();
            }

            return Page();
        }

        public async Task<JsonResult> OnGetVersionesPorProducto(int productoId)
        {
            var versiones = await productoVersionService.ObtenerDatosCombo(productoId);
            return new JsonResult(versiones.Select(v => new { v.Id, v.Nombre }));
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage("Producto");


            decimal importeTotal = 0;
            var ProductosDatos = await productoService.ObtenerTodo();
            var VersionesDatos = ProductosDatos.SelectMany(x => x.ProductosVersiones).ToList();
            var ExtrasDatos = await extraService.ObtenerTodo();
            importeTotal += ProductosDatos.First(x => x.Id == ClienteProducto.ProductoId).PrecioInicial;
            importeTotal += VersionesDatos.First(x => x.Id == ClienteProducto.ProductoVersionId).CostoInicial;
            importeTotal += ExtrasSeleccionadosIds.Any() ? ExtrasDatos.Where(x => ExtrasSeleccionadosIds.Contains(x.Id)).Sum(x => x.Valor) : 0;
            ClienteProducto.Importe = importeTotal;
            ClienteProducto nuevo;
            if (ClienteProducto.Id > 0)
            {
                nuevo = await clienteProductoService.Actualizar(ClienteProducto);
            }
            else
            {
                nuevo = await clienteProductoService.Crear(ClienteProducto);
            }

            var todosLosExtras = await clienteProductoExtraService.ObtenerPorClienteProductoId(nuevo.Id);
            foreach (var extraId in ExtrasSeleccionadosIds)
            {
                var extraEncontrado = todosLosExtras.FirstOrDefault(x => x.ExtraId == extraId);
                if(extraEncontrado == null)
                {
                    var nuevoClienteProductoExtra = await clienteProductoExtraService.Crear(new ClienteProductoExtra()
                    {
                        ClienteProductoId = nuevo.Id,
                        ExtraId = extraId
                    });
                    todosLosExtras.Add(nuevoClienteProductoExtra);
                }
            }

            var listaAEliminar = todosLosExtras.Where(x => !ExtrasSeleccionadosIds.Contains(x.ExtraId)).ToList();
            foreach(var eliminar in listaAEliminar)
            {
                await clienteProductoExtraService.Eliminar(eliminar.Id);
            }

            return RedirectToPage("ClienteProducto", new { id = ClienteProducto.ClienteId });
        }
    }
}
