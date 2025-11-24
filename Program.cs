using AlmacenSC.Core.Interfaces;
using AlmacenSC.Data;
using AlmacenSC.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// 🔥 PUERTO PARA RAILWAY
// =======================================
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// =======================================
// 🔥 BASE DE DATOS (Railway o Local)
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
// 🔥 CONTROLLERS
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
// 🔥 SWAGGER SIEMPRE ACTIVO EN RAILWAY
// =======================================
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

// =======================================
// 🔥 MIGRACIONES AUTOMÁTICAS (CLAVE PARA CREAR TABLAS)
// =======================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AlmacenSCContext>();
    db.Database.Migrate();   // 👈 CREA LAS TABLAS EN RAILWAY
}

app.Run();
