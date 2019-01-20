using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class rezervacijeIzmjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datumRodjenjaPutnika",
                table: "Rezervacije",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Rezervacije",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "imePutnika",
                table: "Rezervacije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "kontaktTelefon",
                table: "Rezervacije",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "prezimePutnika",
                table: "Rezervacije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "zeljeIprimjedbe",
                table: "Rezervacije",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datumRodjenjaPutnika",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "imePutnika",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "kontaktTelefon",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "prezimePutnika",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "zeljeIprimjedbe",
                table: "Rezervacije");
        }
    }
}
