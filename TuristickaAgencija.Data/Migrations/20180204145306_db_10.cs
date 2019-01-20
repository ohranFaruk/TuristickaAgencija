using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class db_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IzmjenePutovanja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IzmjenePutovanja",
                columns: table => new
                {
                    IzmjenaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIzmjene = table.Column<DateTime>(nullable: false),
                    PutvanjeId = table.Column<int>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzmjenePutovanja", x => x.IzmjenaId);
                    table.ForeignKey(
                        name: "FK_IzmjenePutovanja_Putovanja_PutvanjeId",
                        column: x => x.PutvanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzmjenePutovanja_Zaposlenici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IzmjenePutovanja_PutvanjeId",
                table: "IzmjenePutovanja",
                column: "PutvanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_IzmjenePutovanja_ZaposlenikId",
                table: "IzmjenePutovanja",
                column: "ZaposlenikId");
        }
    }
}
