using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AlmacenSC.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargaProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SolicitadoPor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargaProducto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoEntrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    StockMinimo = table.Column<int>(type: "integer", nullable: false),
                    StockMaximo = table.Column<int>(type: "integer", nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoEntrada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargaProductoDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CargaProductoId = table.Column<int>(type: "integer", nullable: false),
                    ProductoEntradaId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargaProductoDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargaProductoDetalle_CargaProducto_CargaProductoId",
                        column: x => x.CargaProductoId,
                        principalTable: "CargaProducto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargaProductoDetalle_ProductoEntrada_ProductoEntradaId",
                        column: x => x.ProductoEntradaId,
                        principalTable: "ProductoEntrada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductoEntradaId = table.Column<int>(type: "integer", nullable: false),
                    StockActual = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventario_ProductoEntrada_ProductoEntradaId",
                        column: x => x.ProductoEntradaId,
                        principalTable: "ProductoEntrada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoSalida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductoEntradaId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SolicitadoPor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoSalida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoSalida_ProductoEntrada_ProductoEntradaId",
                        column: x => x.ProductoEntradaId,
                        principalTable: "ProductoEntrada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertaReabastecimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventarioId = table.Column<int>(type: "integer", nullable: false),
                    FechaAlerta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Atendida = table.Column<bool>(type: "boolean", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertaReabastecimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertaReabastecimiento_Inventario_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertaReabastecimiento_InventarioId",
                table: "AlertaReabastecimiento",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CargaProductoDetalle_CargaProductoId",
                table: "CargaProductoDetalle",
                column: "CargaProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargaProductoDetalle_ProductoEntradaId",
                table: "CargaProductoDetalle",
                column: "ProductoEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ProductoEntradaId",
                table: "Inventario",
                column: "ProductoEntradaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSalida_ProductoEntradaId",
                table: "ProductoSalida",
                column: "ProductoEntradaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertaReabastecimiento");

            migrationBuilder.DropTable(
                name: "CargaProductoDetalle");

            migrationBuilder.DropTable(
                name: "ProductoSalida");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "CargaProducto");

            migrationBuilder.DropTable(
                name: "ProductoEntrada");
        }
    }
}
