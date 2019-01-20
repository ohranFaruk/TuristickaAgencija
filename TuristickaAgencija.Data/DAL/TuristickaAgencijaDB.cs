using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Data.Models;



namespace TuristickaAgencija.Data.DAL
{
    public class TuristickaAgencijaDB: DbContext
    {
        public TuristickaAgencijaDB(DbContextOptions<TuristickaAgencijaDB> db):base(db)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PutovanjaGrupe>()
                .HasOne(z => z.Zaposlenik)
                .WithMany()
                .HasForeignKey(z => z.ZaposlenikId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Putovanje>()
                .HasOne(x => x.Grad)
                .WithMany()
                .HasForeignKey(x => x.GradId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Kontinent> Kontinenti { get; set; }
        public DbSet<Regija> Regije { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Grupa> Grupe { get; set; }
        public DbSet<Jezik> Jezici { get; set; }
        public DbSet<KontakPodaci> KontaktPodaci { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<KreditnaKartica> KreditneKartice { get; set; }
        public DbSet<Licenca> Licence { get; set; }
        public DbSet<Ponuda> Ponude { get; set; }
        public DbSet<PrevoznoSredstvo> PrevoznaSredstva { get; set; }
        public DbSet<PutovanjaGrupe> PutovanjaGrupe  { get; set; }
        public DbSet<Putovanje> Putovanja { get; set; }
        public DbSet<PutovanjeSmjestaj> PutvanjaSmjestaji { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Smjestaj> Smjestaji { get; set; }
        public DbSet<Stanje> Stanja { get; set; }
        public DbSet<StepenTurista> StepeniTurista { get; set; }
        public DbSet<StepenVodica> StepeniVodica { get; set; }
        public DbSet<VodicJezik> VodiciJezici { get; set; }
        public DbSet<Zaposlenik> Zaposlenici { get; set; }
        public DbSet<Turist> Turisti { get; set; }
        public DbSet<Status> Statusi { get; set; }
        public DbSet<Slika> Slike { get; set; }
        public DbSet<AutorizacijskiToken> AutoriacijskiTokeni { get; set; }
        public DbSet<Zaduzenje> Zaduzenja { get; set; }

        public DbSet<Zahtjev> Zahtjev { get; set; }





    }
}
