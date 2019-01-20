using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class izmjenaZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_Putovanja_PutovanjeId",
                table: "Zahtjevi");

            migrationBuilder.DropIndex(
                name: "IX_Zahtjevi_PutovanjeId",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "PutovanjeId",
                table: "Zahtjevi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PutovanjeId",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_PutovanjeId",
                table: "Zahtjevi",
                column: "PutovanjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_Putovanja_PutovanjeId",
                table: "Zahtjevi",
                column: "PutovanjeId",
                principalTable: "Putovanja",
                principalColumn: "PutovanjeId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
