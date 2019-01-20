using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class DB_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PutovanjaGrupe_Putovanja_PutovanjeId",
                table: "PutovanjaGrupe");

            migrationBuilder.DropForeignKey(
                name: "FK_PutovanjaGrupe_Turisti_TuristId",
                table: "PutovanjaGrupe");

            migrationBuilder.DropIndex(
                name: "IX_PutovanjaGrupe_PutovanjeId",
                table: "PutovanjaGrupe");

            migrationBuilder.DropColumn(
                name: "PutovanjeId",
                table: "PutovanjaGrupe");

            migrationBuilder.RenameColumn(
                name: "TuristId",
                table: "PutovanjaGrupe",
                newName: "RezervacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_PutovanjaGrupe_TuristId",
                table: "PutovanjaGrupe",
                newName: "IX_PutovanjaGrupe_RezervacijaId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumPutovanja",
                table: "PutovanjaGrupe",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_PutovanjaGrupe_Rezervacije_RezervacijaId",
                table: "PutovanjaGrupe",
                column: "RezervacijaId",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PutovanjaGrupe_Rezervacije_RezervacijaId",
                table: "PutovanjaGrupe");

            migrationBuilder.DropColumn(
                name: "DatumPutovanja",
                table: "PutovanjaGrupe");

            migrationBuilder.RenameColumn(
                name: "RezervacijaId",
                table: "PutovanjaGrupe",
                newName: "TuristId");

            migrationBuilder.RenameIndex(
                name: "IX_PutovanjaGrupe_RezervacijaId",
                table: "PutovanjaGrupe",
                newName: "IX_PutovanjaGrupe_TuristId");

            migrationBuilder.AddColumn<int>(
                name: "PutovanjeId",
                table: "PutovanjaGrupe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PutovanjaGrupe_PutovanjeId",
                table: "PutovanjaGrupe",
                column: "PutovanjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PutovanjaGrupe_Putovanja_PutovanjeId",
                table: "PutovanjaGrupe",
                column: "PutovanjeId",
                principalTable: "Putovanja",
                principalColumn: "PutovanjeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PutovanjaGrupe_Turisti_TuristId",
                table: "PutovanjaGrupe",
                column: "TuristId",
                principalTable: "Turisti",
                principalColumn: "TuristId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
