using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AlmacenSC.Data;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Infraestructura.Repositorios;



var url = Environment.GetEnvironmentVariable("DATABASE");
Console.WriteLine($"La cadena de coneccion esta: {url}");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AlmacenSCContext>(options =>
    options.UseNpgsql(url));

builder.WebHost.UseUrls("http://0.0.0.0.8080");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsLibre", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// =======================================
// 🔥 REGISTRO DE REPOSITORIOS (IMPORTANTE)
// =======================================
builder.Services.AddScoped<IProductoEntradaRepository, ProductoEntradaRepository>();
builder.Services.AddScoped<IProductoSalidaRepository, ProductoSalidaRepository>();
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<ICargaProductoRepository, CargaProductoRepository>();
builder.Services.AddScoped<ICargaProductoDetalleRepository, CargaProductoDetalleRepository>();
// agrega aquí cualquier otro repositorio que tengas
// =======================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AlmacenSCContext>();
    dbContext.Database.Migrate();
}

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsLibre");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();