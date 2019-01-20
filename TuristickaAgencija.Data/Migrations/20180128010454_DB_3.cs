using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class DB_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Putovanja_PutovanjeId",
                table: "Recenzije");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Turisti_TuristId",
                table: "Recenzije");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Zaposlenici_VodicId",
                table: "Recenzije");

            migrationBuilder.DropIndex(
                name: "IX_Recenzije_PutovanjeId",
                table: "Recenzije");

            migrationBuilder.DropIndex(
                name: "IX_Recenzije_TuristId",
                table: "Recenzije");

            migrationBuilder.DropColumn(
                name: "PutovanjeId",
                table: "Recenzije");

            migrationBuilder.DropColumn(
                name: "TuristId",
                table: "Recenzije");

            migrationBuilder.RenameColumn(
                name: "VodicId",
                table: "Recenzije",
                newName: "RezervacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_Recenzije_VodicId",
                table: "Recenzije",
                newName: "IX_Recenzije_RezervacijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Rezervacije_RezervacijaId",
                table: "Recenzije",
                column: "RezervacijaId",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Rezervacije_RezervacijaId",
                table: "Recenzije");

            migrationBuilder.RenameColumn(
                name: "RezervacijaId",
                table: "Recenzije",
                newName: "VodicId");

            migrationBuilder.RenameIndex(
                name: "IX_Recenzije_RezervacijaId",
                table: "Recenzije",
                newName: "IX_Recenzije_VodicId");

            migrationBuilder.AddColumn<int>(
                name: "PutovanjeId",
                table: "Recenzije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TuristId",
                table: "Recenzije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_PutovanjeId",
                table: "Recenzije",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_TuristId",
                table: "Recenzije",
                column: "TuristId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Putovanja_PutovanjeId",
                table: "Recenzije",
                column: "PutovanjeId",
                principalTable: "Putovanja",
                principalColumn: "PutovanjeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Turisti_TuristId",
                table: "Recenzije",
                column: "TuristId",
                principalTable: "Turisti",
                principalColumn: "TuristId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Zaposlenici_VodicId",
                table: "Recenzije",
                column: "VodicId",
                principalTable: "Zaposlenici",
                principalColumn: "ZaposlenikId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
