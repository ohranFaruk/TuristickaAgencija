using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class dodana_ponuda_na_sliku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PonudaId",
                table: "Slike",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slike_PonudaId",
                table: "Slike",
                column: "PonudaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slike_Ponude_PonudaId",
                table: "Slike",
                column: "PonudaId",
                principalTable: "Ponude",
                principalColumn: "PonudaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slike_Ponude_PonudaId",
                table: "Slike");

            migrationBuilder.DropIndex(
                name: "IX_Slike_PonudaId",
                table: "Slike");

            migrationBuilder.DropColumn(
                name: "PonudaId",
                table: "Slike");
        }
    }
}
