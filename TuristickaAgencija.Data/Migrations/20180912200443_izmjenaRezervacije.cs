using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class izmjenaRezervacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "prezimePutnika",
                table: "Rezervacije",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "imePutnika",
                table: "Rezervacije",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "prezimePutnika",
                table: "Rezervacije",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "imePutnika",
                table: "Rezervacije",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
