using GestionClientesMyM.Data;
using GestionClientesMyM.Services.Clientes;
using GestionClientesMyM.Services.ClientesProductos;
using GestionClientesMyM.Services.ClientesProductosExtras;
using GestionClientesMyM.Services.Extras;
using GestionClientesMyM.Services.Pagos;
using GestionClientesMyM.Services.Productos;
using GestionClientesMyM.Services.ProductosVersiones;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ClienteProductoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProductoVersionService>();
builder.Services.AddScoped<ExtraService>();
builder.Services.AddScoped<ClienteProductoExtraService>();
builder.Services.AddScoped<PagoService>();

builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.Migrate();
    GestionClientesMyM.Data.DbInitializer.Seed(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();
app.MapControllers();

app.Run();
