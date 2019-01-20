using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class migracije567 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datumPotvrde",
                table: "Zahtjevi",
                newName: "datumPotvrdjivanja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datumPotvrdjivanja",
                table: "Zahtjevi",
                newName: "datumPotvrde");
        }
    }
}
