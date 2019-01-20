using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class db_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plate");

            migrationBuilder.DropTable(
                name: "Zahtjevi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plate",
                columns: table => new
                {
                    PlataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIsplate = table.Column<DateTime>(nullable: true),
                    Isplaceno = table.Column<bool>(nullable: false),
                    Iznos = table.Column<double>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plate", x => x.PlataId);
                    table.ForeignKey(
                        name: "FK_Plate_Zaposlenici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zahtjevi",
                columns: table => new
                {
                    ZahtjevId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumZahtjeva = table.Column<DateTime>(nullable: false),
                    Razlog = table.Column<string>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ZaposlenikRadnikId = table.Column<int>(nullable: true),
                    ZaposlenikVodicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtjevi", x => x.ZahtjevId);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Statusi_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statusi",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Zaposlenici_ZaposlenikRadnikId",
                        column: x => x.ZaposlenikRadnikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Zaposlenici_ZaposlenikVodicId",
                        column: x => x.ZaposlenikVodicId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plate_ZaposlenikId",
                table: "Plate",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_StatusId",
                table: "Zahtjevi",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZaposlenikRadnikId",
                table: "Zahtjevi",
                column: "ZaposlenikRadnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZaposlenikVodicId",
                table: "Zahtjevi",
                column: "ZaposlenikVodicId");
        }
    }
}
