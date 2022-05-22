using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AgendaApp.Models
{
    public partial class AgendaAppContext : DbContext
    {


        public AgendaAppContext() : base()
        {
        }

        public AgendaAppContext(DbContextOptions<AgendaAppContext> options): base(options)
        {
        }

        public virtual DbSet<Gebruiker> Agendagebruikers { get; set; }
        public virtual DbSet<Doel> Doelen { get; set; }
        public virtual DbSet<Timer> Timers { get; set; }

        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=Project2G07.mssql.somee.com;Initial Catalog=Project2G07;User ID=P2PG07_SQLLogin_1;Password=j35u7wyp55");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<Gebruiker>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__AGENDA__C0E4B7081BEAABFF");

                entity.ToTable("AGENDAGEBRUIKER");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GEBRUIKERSNAAM");

                entity.Property(e => e.Gebruikernr).HasColumnName("GEBRUIKERSNR");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WACHTWOORD");

                entity.Ignore(e => e.ConcurrencyStamp);
                entity.Ignore(e => e.Email);
                entity.Ignore(e => e.EmailConfirmed);
                entity.Ignore(e => e.NormalizedEmail);
                entity.Ignore(e => e.Id);
                entity.Ignore(e => e.SecurityStamp);
                entity.Ignore(e => e.PhoneNumber);
                entity.Ignore(e => e.PhoneNumberConfirmed);
                entity.Ignore(e => e.TwoFactorEnabled);
            });

            modelBuilder.Entity<Doel>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AGENDA__C0E4B7081BEAABFG");

                entity.ToTable("DOEL");

                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("numeric(19, 0)");

                entity.Property(e => e.Naam)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DOELNAAM");

                entity.Property(e => e.Kleur).HasColumnName("KLEUR");

                entity.Property(e => e.Dates)
                    .HasColumnName("DATES");

                entity.Property(e => e.GebruikerNaam)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GEBRUIKERNAAM");


            });
            modelBuilder.Entity<Timer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AGENDA__C0E4B7081BAABYG");

                entity.ToTable("TIMER");

                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("numeric(19, 0)");

                entity.Property(e => e.Naam)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TIMERNAAM");

                entity.Property(e => e.Start).HasColumnName("START");

                entity.Property(e => e.End)
                    .HasColumnName("END");
                entity.Property(e => e.Time)
                    .HasColumnName("TIME");

                entity.Property(e => e.GebruikerNaam)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GEBRUIKERNAAM");


            });
            
        }
            

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
