using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using AlmacenSC.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 🔥 BASE DE DATOS (Railway o Local)
// =======================================
// Si existe DATABASE_URL → usar Railway
// Si no existe → usar appsettings.json (local)
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
                      ?? builder.Configuration.GetConnectionString("AlmacenSCContext");

builder.Services.AddDbContext<AlmacenSCContext>(options =>
    options.UseNpgsql(connectionString));

// =======================================
// 🔥 REGISTRO DE REPOSITORIOS
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
else
{
    // Railway NO usa HTTPS
    // NUNCA activar HttpsRedirection en producción
}

app.UseHttpsRedirection(); // Esto solo aplica en desarrollo
app.UseAuthorization();

app.MapControllers();

app.Run();
