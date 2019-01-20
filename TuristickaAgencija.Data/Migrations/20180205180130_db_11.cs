using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class db_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Lozinka",
                table: "Korisnici",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Lozinka",
                table: "Korisnici",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
