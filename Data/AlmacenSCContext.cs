using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlmacenSC.Core.Entities;

namespace AlmacenSC.Data
{
    public class AlmacenSCContext : DbContext
    {
        public AlmacenSCContext (DbContextOptions<AlmacenSCContext> options)
            : base(options)
        {
        }

        public DbSet<AlmacenSC.Core.Entities.AlertaReabastecimiento> AlertaReabastecimiento { get; set; } = default!;
        public DbSet<AlmacenSC.Core.Entities.CargaProducto> CargaProducto { get; set; } = default!;
        public DbSet<AlmacenSC.Core.Entities.CargaProductoDetalle> CargaProductoDetalle { get; set; } = default!;
        public DbSet<AlmacenSC.Core.Entities.Inventario> Inventario { get; set; } = default!;
        public DbSet<AlmacenSC.Core.Entities.ProductoEntrada> ProductoEntrada { get; set; } = default!;
        public DbSet<AlmacenSC.Core.Entities.ProductoSalida> ProductoSalida { get; set; } = default!;
    }
}
