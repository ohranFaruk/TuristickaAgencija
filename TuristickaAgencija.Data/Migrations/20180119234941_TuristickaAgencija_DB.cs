using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data.Migrations
{
    public partial class TuristickaAgencija_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupe",
                columns: table => new
                {
                    GrupaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxBrojTurista = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupe", x => x.GrupaId);
                });

            migrationBuilder.CreateTable(
                name: "Jezici",
                columns: table => new
                {
                    JezikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivJezika = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jezici", x => x.JezikId);
                });

            migrationBuilder.CreateTable(
                name: "Kontinenti",
                columns: table => new
                {
                    KontinentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontinenti", x => x.KontinentId);
                });

            migrationBuilder.CreateTable(
                name: "Ponude",
                columns: table => new
                {
                    PonudaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIzmjene = table.Column<DateTime>(nullable: false),
                    DatumPocetka = table.Column<DateTime>(nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    isAktivna = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponude", x => x.PonudaId);
                });

            migrationBuilder.CreateTable(
                name: "PrevoznaSredstva",
                columns: table => new
                {
                    PrevoznoSredstvoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevoznaSredstva", x => x.PrevoznoSredstvoId);
                });

            migrationBuilder.CreateTable(
                name: "Stanja",
                columns: table => new
                {
                    StanjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StanjeRezervacije = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanja", x => x.StanjeId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "StepeniTurista",
                columns: table => new
                {
                    StepenTuristaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Stepen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepeniTurista", x => x.StepenTuristaId);
                });

            migrationBuilder.CreateTable(
                name: "StepeniVodica",
                columns: table => new
                {
                    StepenVodicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Stepen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepeniVodica", x => x.StepenVodicaId);
                });

            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    DrzavaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KontinentId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.DrzavaId);
                    table.ForeignKey(
                        name: "FK_Drzave_Kontinenti_KontinentId",
                        column: x => x.KontinentId,
                        principalTable: "Kontinenti",
                        principalColumn: "KontinentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regije",
                columns: table => new
                {
                    RegijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrzavaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regije", x => x.RegijaId);
                    table.ForeignKey(
                        name: "FK_Regije_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    GradId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    RegijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.GradId);
                    table.ForeignKey(
                        name: "FK_Gradovi_Regije_RegijaId",
                        column: x => x.RegijaId,
                        principalTable: "Regije",
                        principalColumn: "RegijaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(maxLength: 70, nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    DatumZadnjePrijave = table.Column<DateTime>(nullable: false),
                    GradId = table.Column<int>(nullable: false),
                    Ime = table.Column<string>(maxLength: 30, nullable: false),
                    JMBG = table.Column<string>(maxLength: 13, nullable: false),
                    KorisnickoIme = table.Column<string>(maxLength: 70, nullable: false),
                    Lozinka = table.Column<string>(maxLength: 24, nullable: false),
                    Prezime = table.Column<string>(maxLength: 30, nullable: false),
                    Spol = table.Column<string>(maxLength: 1, nullable: false),
                    isAktivan = table.Column<bool>(nullable: false),
                    isPromjenoLozinku = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Korisnici_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Putovanja",
                columns: table => new
                {
                    PutovanjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<double>(nullable: false),
                    DatumIzmjene = table.Column<DateTime>(nullable: false),
                    DatumPolaska = table.Column<DateTime>(nullable: false),
                    DatumPovratka = table.Column<DateTime>(nullable: false),
                    GradId = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: false),
                    PonudaId = table.Column<int>(nullable: true),
                    Popust = table.Column<int>(nullable: true),
                    PrevoznoSredstvoId = table.Column<int>(nullable: false),
                    isAktivno = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putovanja", x => x.PutovanjeId);
                    table.ForeignKey(
                        name: "FK_Putovanja_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Putovanja_Ponude_PonudaId",
                        column: x => x.PonudaId,
                        principalTable: "Ponude",
                        principalColumn: "PonudaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Putovanja_PrevoznaSredstva_PrevoznoSredstvoId",
                        column: x => x.PrevoznoSredstvoId,
                        principalTable: "PrevoznaSredstva",
                        principalColumn: "PrevoznoSredstvoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Smjestaji",
                columns: table => new
                {
                    SmjestajId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojZvjezdica = table.Column<int>(nullable: false),
                    GradId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Opis = table.Column<string>(nullable: false),
                    StandardnaCijena = table.Column<double>(nullable: false),
                    WebStranica = table.Column<string>(maxLength: 100, nullable: false),
                    isAktivan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjestaji", x => x.SmjestajId);
                    table.ForeignKey(
                        name: "FK_Smjestaji_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KontaktPodaci",
                columns: table => new
                {
                    KontaktId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 80, nullable: true),
                    KorisnikId = table.Column<int>(nullable: false),
                    Telefon = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KontaktPodaci", x => x.KontaktId);
                    table.ForeignKey(
                        name: "FK_KontaktPodaci_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KreditneKartice",
                columns: table => new
                {
                    KarticaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojKartice = table.Column<string>(maxLength: 16, nullable: false),
                    GodinaIsteka = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    MjesecIsteka = table.Column<int>(nullable: false),
                    Tip = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KreditneKartice", x => x.KarticaId);
                    table.ForeignKey(
                        name: "FK_KreditneKartice_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turisti",
                columns: table => new
                {
                    TuristId = table.Column<int>(nullable: false),
                    Index = table.Column<string>(maxLength: 20, nullable: false),
                    StepenTuristaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turisti", x => x.TuristId);
                    table.ForeignKey(
                        name: "FK_Turisti_StepeniTurista_StepenTuristaId",
                        column: x => x.StepenTuristaId,
                        principalTable: "StepeniTurista",
                        principalColumn: "StepenTuristaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turisti_Korisnici_TuristId",
                        column: x => x.TuristId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenici",
                columns: table => new
                {
                    ZaposlenikId = table.Column<int>(nullable: false),
                    DatumZaposljavanja = table.Column<DateTime>(nullable: false),
                    IsVodic = table.Column<bool>(nullable: false),
                    MjeseciIskustva = table.Column<int>(nullable: false),
                    StepenVodicaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenici", x => x.ZaposlenikId);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_StepeniVodica_StepenVodicaId",
                        column: x => x.StepenVodicaId,
                        principalTable: "StepeniVodica",
                        principalColumn: "StepenVodicaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_Korisnici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PutvanjaSmjestaji",
                columns: table => new
                {
                    PutovanjeSmjestajId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PutovanjeId = table.Column<int>(nullable: false),
                    SmjestajId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PutvanjaSmjestaji", x => x.PutovanjeSmjestajId);
                    table.ForeignKey(
                        name: "FK_PutvanjaSmjestaji_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PutvanjaSmjestaji_Smjestaji_SmjestajId",
                        column: x => x.SmjestajId,
                        principalTable: "Smjestaji",
                        principalColumn: "SmjestajId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzmjenePutovanja",
                columns: table => new
                {
                    IzmjenaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIzmjene = table.Column<DateTime>(nullable: false),
                    PutvanjeId = table.Column<int>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzmjenePutovanja", x => x.IzmjenaId);
                    table.ForeignKey(
                        name: "FK_IzmjenePutovanja_Putovanja_PutvanjeId",
                        column: x => x.PutvanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzmjenePutovanja_Zaposlenici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licence",
                columns: table => new
                {
                    LicencaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumStjecanja = table.Column<DateTime>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    SerijskiBrojLicence = table.Column<string>(maxLength: 16, nullable: false),
                    VodicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licence", x => x.LicencaId);
                    table.ForeignKey(
                        name: "FK_Licence_Zaposlenici_VodicId",
                        column: x => x.VodicId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plate",
                columns: table => new
                {
                    PlataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIsplate = table.Column<DateTime>(nullable: true),
                    Isplaceno = table.Column<bool>(nullable: false),
                    Iznos = table.Column<double>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plate", x => x.PlataId);
                    table.ForeignKey(
                        name: "FK_Plate_Zaposlenici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PutovanjaGrupe",
                columns: table => new
                {
                    PutovanjeGrupeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrupaId = table.Column<int>(nullable: false),
                    PutovanjeId = table.Column<int>(nullable: false),
                    TuristId = table.Column<int>(nullable: false),
                    VodicId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PutovanjaGrupe", x => x.PutovanjeGrupeId);
                    table.ForeignKey(
                        name: "FK_PutovanjaGrupe_Grupe_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupe",
                        principalColumn: "GrupaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PutovanjaGrupe_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PutovanjaGrupe_Turisti_TuristId",
                        column: x => x.TuristId,
                        principalTable: "Turisti",
                        principalColumn: "TuristId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PutovanjaGrupe_Zaposlenici_VodicId",
                        column: x => x.VodicId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    RecenzijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKomentara = table.Column<DateTime>(nullable: false),
                    Komentar = table.Column<string>(maxLength: 500, nullable: true),
                    Ocjena = table.Column<int>(nullable: true),
                    PutovanjeId = table.Column<int>(nullable: false),
                    TuristId = table.Column<int>(nullable: false),
                    VodicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.RecenzijaId);
                    table.ForeignKey(
                        name: "FK_Recenzije_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzije_Turisti_TuristId",
                        column: x => x.TuristId,
                        principalTable: "Turisti",
                        principalColumn: "TuristId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzije_Zaposlenici_VodicId",
                        column: x => x.VodicId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VodiciJezici",
                columns: table => new
                {
                    VodicJezikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JezikId = table.Column<int>(nullable: false),
                    Stepen = table.Column<string>(maxLength: 5, nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VodiciJezici", x => x.VodicJezikId);
                    table.ForeignKey(
                        name: "FK_VodiciJezici_Jezici_JezikId",
                        column: x => x.JezikId,
                        principalTable: "Jezici",
                        principalColumn: "JezikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VodiciJezici_Zaposlenici_ZaposlenikId",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zahtjevi",
                columns: table => new
                {
                    ZahtjevId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumZahtjeva = table.Column<DateTime>(nullable: false),
                    Razlog = table.Column<string>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ZaposlenikRadnikId = table.Column<int>(nullable: true),
                    ZaposlenikVodicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtjevi", x => x.ZahtjevId);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Zaposlenici_ZaposlenikRadnikId",
                        column: x => x.ZaposlenikRadnikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Zaposlenici_ZaposlenikVodicId",
                        column: x => x.ZaposlenikVodicId,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    RezervacijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumRezervacije = table.Column<DateTime>(nullable: false),
                    PutovanjeId = table.Column<int>(nullable: false),
                    PutovanjeSmjestajId = table.Column<int>(nullable: true),
                    StanjeId = table.Column<int>(nullable: false),
                    TuristId = table.Column<int>(nullable: false),
                    UkupanIznos = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.RezervacijaId);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacije_PutvanjaSmjestaji_PutovanjeSmjestajId",
                        column: x => x.PutovanjeSmjestajId,
                        principalTable: "PutvanjaSmjestaji",
                        principalColumn: "PutovanjeSmjestajId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Stanja_StanjeId",
                        column: x => x.StanjeId,
                        principalTable: "Stanja",
                        principalColumn: "StanjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Turisti_TuristId",
                        column: x => x.TuristId,
                        principalTable: "Turisti",
                        principalColumn: "TuristId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drzave_KontinentId",
                table: "Drzave",
                column: "KontinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi_RegijaId",
                table: "Gradovi",
                column: "RegijaId");

            migrationBuilder.CreateIndex(
                name: "IX_IzmjenePutovanja_PutvanjeId",
                table: "IzmjenePutovanja",
                column: "PutvanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_IzmjenePutovanja_ZaposlenikId",
                table: "IzmjenePutovanja",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_KontaktPodaci_KorisnikId",
                table: "KontaktPodaci",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_GradId",
                table: "Korisnici",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_KreditneKartice_KorisnikId",
                table: "KreditneKartice",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Licence_VodicId",
                table: "Licence",
                column: "VodicId");

            migrationBuilder.CreateIndex(
                name: "IX_Plate_ZaposlenikId",
                table: "Plate",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_GradId",
                table: "Putovanja",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_PonudaId",
                table: "Putovanja",
                column: "PonudaId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_PrevoznoSredstvoId",
                table: "Putovanja",
                column: "PrevoznoSredstvoId");

            migrationBuilder.CreateIndex(
                name: "IX_PutovanjaGrupe_GrupaId",
                table: "PutovanjaGrupe",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_PutovanjaGrupe_PutovanjeId",
                table: "PutovanjaGrupe",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_PutovanjaGrupe_TuristId",
                table: "PutovanjaGrupe",
                column: "TuristId");

            migrationBuilder.CreateIndex(
                name: "IX_PutovanjaGrupe_VodicId",
                table: "PutovanjaGrupe",
                column: "VodicId");

            migrationBuilder.CreateIndex(
                name: "IX_PutvanjaSmjestaji_PutovanjeId",
                table: "PutvanjaSmjestaji",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_PutvanjaSmjestaji_SmjestajId",
                table: "PutvanjaSmjestaji",
                column: "SmjestajId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_PutovanjeId",
                table: "Recenzije",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_TuristId",
                table: "Recenzije",
                column: "TuristId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_VodicId",
                table: "Recenzije",
                column: "VodicId");

            migrationBuilder.CreateIndex(
                name: "IX_Regije_DrzavaId",
                table: "Regije",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_PutovanjeId",
                table: "Rezervacije",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_PutovanjeSmjestajId",
                table: "Rezervacije",
                column: "PutovanjeSmjestajId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_StanjeId",
                table: "Rezervacije",
                column: "StanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_TuristId",
                table: "Rezervacije",
                column: "TuristId");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaji_GradId",
                table: "Smjestaji",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Turisti_StepenTuristaId",
                table: "Turisti",
                column: "StepenTuristaId");

            migrationBuilder.CreateIndex(
                name: "IX_VodiciJezici_JezikId",
                table: "VodiciJezici",
                column: "JezikId");

            migrationBuilder.CreateIndex(
                name: "IX_VodiciJezici_ZaposlenikId",
                table: "VodiciJezici",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_StatusId",
                table: "Zahtjevi",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZaposlenikRadnikId",
                table: "Zahtjevi",
                column: "ZaposlenikRadnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZaposlenikVodicId",
                table: "Zahtjevi",
                column: "ZaposlenikVodicId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_StepenVodicaId",
                table: "Zaposlenici",
                column: "StepenVodicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IzmjenePutovanja");

            migrationBuilder.DropTable(
                name: "KontaktPodaci");

            migrationBuilder.DropTable(
                name: "KreditneKartice");

            migrationBuilder.DropTable(
                name: "Licence");

            migrationBuilder.DropTable(
                name: "Plate");

            migrationBuilder.DropTable(
                name: "PutovanjaGrupe");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "VodiciJezici");

            migrationBuilder.DropTable(
                name: "Zahtjevi");

            migrationBuilder.DropTable(
                name: "Grupe");

            migrationBuilder.DropTable(
                name: "PutvanjaSmjestaji");

            migrationBuilder.DropTable(
                name: "Stanja");

            migrationBuilder.DropTable(
                name: "Turisti");

            migrationBuilder.DropTable(
                name: "Jezici");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Zaposlenici");

            migrationBuilder.DropTable(
                name: "Putovanja");

            migrationBuilder.DropTable(
                name: "Smjestaji");

            migrationBuilder.DropTable(
                name: "StepeniTurista");

            migrationBuilder.DropTable(
                name: "StepeniVodica");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Ponude");

            migrationBuilder.DropTable(
                name: "PrevoznaSredstva");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Regije");

            migrationBuilder.DropTable(
                name: "Drzave");

            migrationBuilder.DropTable(
                name: "Kontinenti");
        }
    }
}
