using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AlmacenSC.Data;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 🔥 BASE DE DATOS
// =======================================
builder.Services.AddDbContext<AlmacenSCContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("AlmacenSCContext")
        ?? throw new InvalidOperationException("Connection string 'AlmacenSCContext' not found.")
    ));


// =======================================
// 🔥 REGISTRO DE REPOSITORIOS
// (Todos los que aparecen en tus controladores)
// =======================================
builder.Services.AddScoped<IProductoEntradaRepository, ProductoEntradaRepository>();
builder.Services.AddScoped<IProductoSalidaRepository, ProductoSalidaRepository>();
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<ICargaProductoRepository, CargaProductoRepository>();
builder.Services.AddScoped<ICargaProductoDetalleRepository, CargaProductoDetalleRepository>();
builder.Services.AddScoped<IAlertaReabastecimientoRepository, AlertaReabastecimientoRepository>();


// =======================================
// 🔥 CONTROLLERS + JSON CYCLES FIX
// =======================================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// =======================================
// 🔥 MIDDLEWARE
// =======================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
