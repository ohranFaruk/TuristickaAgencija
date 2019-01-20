using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class DB_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PutovanjaGrupe_Zaposlenici_VodicId",
                table: "PutovanjaGrupe");

            migrationBuilder.RenameColumn(
                name: "VodicId",
                table: "PutovanjaGrupe",
                newName: "ZaposlenikId");

            migrationBuilder.RenameIndex(
                name: "IX_PutovanjaGrupe_VodicId",
                table: "PutovanjaGrupe",
                newName: "IX_PutovanjaGrupe_ZaposlenikId");

            migrationBuilder.AddForeignKey(
                name: "FK_PutovanjaGrupe_Zaposlenici_ZaposlenikId",
                table: "PutovanjaGrupe",
                column: "ZaposlenikId",
                principalTable: "Zaposlenici",
                principalColumn: "ZaposlenikId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PutovanjaGrupe_Zaposlenici_ZaposlenikId",
                table: "PutovanjaGrupe");

            migrationBuilder.RenameColumn(
                name: "ZaposlenikId",
                table: "PutovanjaGrupe",
                newName: "VodicId");

            migrationBuilder.RenameIndex(
                name: "IX_PutovanjaGrupe_ZaposlenikId",
                table: "PutovanjaGrupe",
                newName: "IX_PutovanjaGrupe_VodicId");

            migrationBuilder.AddForeignKey(
                name: "FK_PutovanjaGrupe_Zaposlenici_VodicId",
                table: "PutovanjaGrupe",
                column: "VodicId",
                principalTable: "Zaposlenici",
                principalColumn: "ZaposlenikId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
