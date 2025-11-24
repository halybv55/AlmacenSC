using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using AlmacenSC.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 🔥 BASE DE DATOS (Railway → DATABASE_URL)
// =======================================
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
// 🔥 CONTROLLERS + JSON FIX
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
// 🔥 SWAGGER SIEMPRE EN RAILWAY
// =======================================
app.UseSwagger();
app.UseSwaggerUI();

// ❌ NO uses HTTPS EN RAILWAY
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
