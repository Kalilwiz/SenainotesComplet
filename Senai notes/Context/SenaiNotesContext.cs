using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Senai_notes.Models;

namespace Senai_notes.Context;

public partial class SenaiNotesContext : DbContext
{
    public SenaiNotesContext()
    {
    }

    public SenaiNotesContext(DbContextOptions<SenaiNotesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagNota> TagNotas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NOTE24-S28\\SQLEXPRESS;Initial Catalog=SenaiNotes;User Id=sa;Password=Senai@134;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotaId).HasName("PK__Notas__EF36CC7A5668704B");

            entity.Property(e => e.NotaId).HasColumnName("NotaID");
            entity.Property(e => e.DataAlteracao).HasColumnType("datetime");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Texto).HasColumnType("text");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Nota)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notas__UserID__4BAC3F29");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4C1E75AEB5");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TagNota>(entity =>
        {
            entity.HasKey(e => e.TagNotaId).HasName("PK__TagNotas__C194BF2DCF8F58DE");

            entity.Property(e => e.TagNotaId).HasColumnName("TagNotaID");
            entity.Property(e => e.NotaId).HasColumnName("NotaID");
            entity.Property(e => e.TagId).HasColumnName("TagID");

            entity.HasOne(d => d.Nota).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.NotaId)
                .HasConstraintName("FK__TagNotas__NotaID__5070F446");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK__TagNotas__TagID__5165187F");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Usuarios__1788CCACC53BC142");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
