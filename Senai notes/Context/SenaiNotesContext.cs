﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Senai_notes.Models;

namespace Senai_notes.Context;

public partial class SenaiNotesContext : DbContext
{
    public SenaiNotesContext()
    {
    }

    private IConfiguration _configuration;
    public SenaiNotesContext(DbContextOptions<SenaiNotesContext> options, IConfiguration config)
        : base(options)
    {
            _configuration = config;
    }

    public virtual DbSet<AuditoriaGeral> AuditoriaGerals { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagNota> TagNotas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var con = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(con);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriaGeral>(entity =>
        {
            entity.HasKey(e => e.AuditoriaId).HasName("PK__Auditori__095694E3C579070E");

            entity.ToTable("AuditoriaGeral");

            entity.Property(e => e.AuditoriaId).HasColumnName("AuditoriaID");
            entity.Property(e => e.DataAcao).HasColumnType("datetime");
            entity.Property(e => e.NomeTabela)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TipoAcao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotaId).HasName("PK__Notas__EF36CC7AFEB90979");

            entity.ToTable(tb => tb.HasTrigger("trg_Audit_Notas"));

            entity.HasIndex(e => e.UserId, "idx_UserNotas");

            entity.Property(e => e.NotaId).HasColumnName("NotaID");
            entity.Property(e => e.DataAlteracao).HasColumnType("datetime");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Nota)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Notas_Usuarios");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4CCDE2558A");

            entity.ToTable(tb => tb.HasTrigger("trg_Audit_Tags"));

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Tags)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Tags_Usuarios");
        });

        modelBuilder.Entity<TagNota>(entity =>
        {
            entity.HasKey(e => e.TagNotaId).HasName("PK__TagNotas__C194BF2DA00B53F3");

            entity.Property(e => e.TagNotaId).HasColumnName("TagNotaID");
            entity.Property(e => e.NotaId).HasColumnName("NotaID");
            entity.Property(e => e.TagId).HasColumnName("TagID");

            entity.HasOne(d => d.Nota).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.NotaId)
                .HasConstraintName("FK__TagNotas__NotaID__56E8E7AB");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK__TagNotas__TagID__57DD0BE4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usuarios__1788CCAC3C77FAB3");

            entity.ToTable(tb => tb.HasTrigger("trg_Audit_User"));

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
