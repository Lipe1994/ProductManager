using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManager.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "CNPJ", "Description", "IsRemoved" },
                values: new object[] { 1, "11068167000453", "Acer", false });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "CNPJ", "Description", "IsRemoved" },
                values: new object[] { 2, "72381189000110", "Dell", false });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "CNPJ", "Description", "IsRemoved" },
                values: new object[] { 3, "00623904000173", "Apple", false });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Description", "ExpirationDate", "IsActive", "IsRemoved", "ManufacturingDate", "ProviderId" },
                values: new object[,]
                {
                    { 1, "ACER Notebook Gamer Nitro", new DateTime(2028, 1, 4, 4, 24, 36, 46, DateTimeKind.Local).AddTicks(8400), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(2570), 1 },
                    { 2, "ACER Notebook Nitro 5 AN515-44-R4KA", new DateTime(2027, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(6790), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(6960), 1 },
                    { 3, "ACER Notebook Gamer Nitro 5 AN515-55-79X0", new DateTime(2029, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7180), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7190), 1 },
                    { 4, "ACER AN517-52-50RS", new DateTime(2029, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7200), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7210), 1 },
                    { 5, "Notebook Dell Inspiron 5402", new DateTime(2028, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7220), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7220), 2 },
                    { 6, "DELL Notebook Gamer G15-i1000-D20P", new DateTime(2028, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7240), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(7250), 2 },
                    { 7, "Notebook Dell Inspiron i15-i1100-A70S 15.6", new DateTime(2028, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8010), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8020), 2 },
                    { 8, "Notebook Apple MacBook Air (de 13 polegadas)", new DateTime(2031, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8030), true, false, new DateTime(2021, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8040), 3 },
                    { 9, "Notebook Apple MacBook PRO (de 13 polegadas)", new DateTime(2031, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8050), true, false, new DateTime(2021, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8050), 3 },
                    { 10, "Notebook Apple MacBook Air de 13 polegadas: Chip M2", new DateTime(2031, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8070), true, false, new DateTime(2022, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8070), 3 },
                    { 11, "Notebook Apple MacBook Air de 13 polegadas Intel", new DateTime(2026, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8080), true, false, new DateTime(2020, 1, 4, 4, 24, 36, 71, DateTimeKind.Local).AddTicks(8090), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ProviderId",
                table: "Produtos",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
