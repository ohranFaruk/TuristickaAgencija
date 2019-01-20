using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_Status_StatusId",
                table: "Zahtjevi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statusi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statusi",
                table: "Statusi",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_Statusi_StatusId",
                table: "Zahtjevi",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_Statusi_StatusId",
                table: "Zahtjevi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statusi",
                table: "Statusi");

            migrationBuilder.RenameTable(
                name: "Statusi",
                newName: "Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_Status_StatusId",
                table: "Zahtjevi",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
