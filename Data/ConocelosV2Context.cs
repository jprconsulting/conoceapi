using System;
using System.Collections.Generic;
using conocelos_v3.Models;
using Microsoft.EntityFrameworkCore;

namespace conocelos_v3.Data;

public partial class ConocelosV2Context : DbContext
{
    public ConocelosV2Context()
    {
    }

    public ConocelosV2Context(DbContextOptions<ConocelosV2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Candidatura> Candidaturas { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RutaImagen> RutaImagens { get; set; }

    public virtual DbSet<TablaFormulario> TablaFormularios { get; set; }

    public virtual DbSet<TablaFormularioUsuario> TablaFormularioUsuarios { get; set; }

    public virtual DbSet<TipoCandidatura> TipoCandidaturas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioAutenticado> UsuarioAutenticados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.CandidatoId).HasName("PRIMARY");

            entity.ToTable("candidato");

            entity.HasIndex(e => e.CandidaturaId, "candidaturaId");

            entity.HasIndex(e => e.CargoId, "cargoId");

            entity.HasIndex(e => e.EstadoId, "estadoId");

            entity.HasIndex(e => e.GeneroId, "generoId");

            entity.Property(e => e.CandidatoId).HasColumnName("candidatoId");
            entity.Property(e => e.CandidaturaId).HasColumnName("candidaturaId");
            entity.Property(e => e.CargoId).HasColumnName("cargoId");
            entity.Property(e => e.DireccionCasaCampania)
                .HasMaxLength(500)
                .HasColumnName("direccionCasaCampania");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.EstadoId).HasColumnName("estadoId");
            entity.Property(e => e.Estatus)
                .HasColumnType("bit(1)")
                .HasColumnName("estatus");
            entity.Property(e => e.Facebook)
                .HasMaxLength(500)
                .HasColumnName("facebook");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Foto)
                .HasMaxLength(100)
                .HasColumnName("foto");
            entity.Property(e => e.GeneroId).HasColumnName("generoId");
            entity.Property(e => e.Instagram)
                .HasMaxLength(500)
                .HasColumnName("instagram");
            entity.Property(e => e.NombrePropietario)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("nombrePropietario");
            entity.Property(e => e.NombreSuplente)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("nombreSuplente");
            entity.Property(e => e.PaginaWeb)
                .HasMaxLength(500)
                .HasColumnName("paginaWeb");
            entity.Property(e => e.SobrenombrePropietario)
                .HasMaxLength(100)
                .HasColumnName("sobrenombrePropietario");
            entity.Property(e => e.TelefonoPublico)
                .HasMaxLength(50)
                .HasColumnName("telefonoPublico");
            entity.Property(e => e.Tiktok)
                .HasMaxLength(500)
                .HasColumnName("tiktok");
            entity.Property(e => e.Twitter)
                .HasMaxLength(500)
                .HasColumnName("twitter");

            //entity.HasOne(d => d.Candidatura).WithMany(p => p.Candidatos)
            //    .HasForeignKey(d => d.CandidaturaId)
            //    .HasConstraintName("candidato_ibfk_4");

            //entity.HasOne(d => d.Cargo).WithMany(p => p.Candidatos)
            //    .HasForeignKey(d => d.CargoId)
            //    .HasConstraintName("candidato_ibfk_1");

            //entity.HasOne(d => d.Estado).WithMany(p => p.Candidatos)
            //    .HasForeignKey(d => d.EstadoId)
            //    .HasConstraintName("candidato_ibfk_2");

            //entity.HasOne(d => d.Genero).WithMany(p => p.Candidatos)
            //    .HasForeignKey(d => d.GeneroId)
            //    .HasConstraintName("candidato_ibfk_3");
        });

        modelBuilder.Entity<Candidatura>(entity =>
        {
            entity.HasKey(e => e.CandidaturaId).HasName("PRIMARY");

            entity.ToTable("candidatura");

            entity.HasIndex(e => e.TipoCandidaturaId, "tipoCandidaturaId");

            entity.Property(e => e.CandidaturaId).HasColumnName("candidaturaId");
            entity.Property(e => e.Estatus)
                .HasColumnType("bit(1)")
                .HasColumnName("estatus");
            entity.Property(e => e.Logo)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("logo");
            entity.Property(e => e.NombreCandidatura)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("nombreCandidatura");
            entity.Property(e => e.TipoCandidaturaId).HasColumnName("tipoCandidaturaId");

            //entity.HasOne(d => d.TipoCandidatura).WithMany(p => p.Candidaturas)
            //    .HasForeignKey(d => d.TipoCandidaturaId)
            //    .HasConstraintName("candidatura_ibfk_1");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PRIMARY");

            entity.ToTable("cargo");

            entity.Property(e => e.CargoId).HasColumnName("cargoId");
            entity.Property(e => e.NombreCargo)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("nombreCargo");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PRIMARY");

            entity.ToTable("estado");

            entity.Property(e => e.EstadoId).HasColumnName("estadoId");
            entity.Property(e => e.NombreEstado)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("nombreEstado");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PRIMARY");

            entity.ToTable("genero");

            entity.Property(e => e.GeneroId).HasColumnName("generoId");
            entity.Property(e => e.NombreGenero)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nombreGenero");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.RolId).HasColumnName("rolId");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<RutaImagen>(entity =>
        {
            entity.HasKey(e => e.ImagenId).HasName("PRIMARY");

            entity.ToTable("ruta_imagen");

            entity.HasIndex(e => e.UsuarioId, "usuarioId");

            entity.Property(e => e.ImagenId).HasColumnName("imagenId");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

            //entity.HasOne(d => d.Usuario).WithMany(p => p.RutaImagens)
            //    .HasForeignKey(d => d.UsuarioId)
            //    .HasConstraintName("ruta_imagen_ibfk_1");
        });

        modelBuilder.Entity<TablaFormulario>(entity =>
        {
            entity.HasKey(e => e.FormularioId).HasName("PRIMARY");

            entity.ToTable("tabla_formulario");

            entity.Property(e => e.FormularioId).HasColumnName("formularioId");
            entity.Property(e => e.AuthProviderX509CertUrl)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("auth_provider_x509_cert_url");
            entity.Property(e => e.AuthUri)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("auth_uri");
            entity.Property(e => e.ClientEmail)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("client_email");
            entity.Property(e => e.ClientId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("client_id");
            entity.Property(e => e.ClientX509CertUrl)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("client_x509_cert_url");
            entity.Property(e => e.FormName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("formName");
            entity.Property(e => e.GoogleEditFormId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("googleEditFormId");
            entity.Property(e => e.GoogleFormId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("googleFormId");
            entity.Property(e => e.PrivateKey)
                .IsRequired()
                .HasColumnName("private_key");
            entity.Property(e => e.PrivateKeyId)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("private_key_id");
            entity.Property(e => e.ProjectId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("projectId");
            entity.Property(e => e.ProjectId1)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("project_id");
            entity.Property(e => e.SheetName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("sheetName");
            entity.Property(e => e.SpreadsheetId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("spreadsheetId");
            entity.Property(e => e.TokenUri)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("token_uri");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UniverseDomain)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("universe_domain");
        });

        modelBuilder.Entity<TablaFormularioUsuario>(entity =>
        {
            entity.HasKey(e => e.FormularioUsuarioId).HasName("PRIMARY");

            entity.ToTable("tabla_formulario_usuario");

            entity.HasIndex(e => e.UsuarioId, "usuarioId");

            entity.Property(e => e.FormularioUsuarioId)
                .ValueGeneratedOnAdd()
                .HasColumnName("formularioUsuarioId");
            entity.Property(e => e.FormularioId).HasColumnName("formularioId");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

            //entity.HasOne(d => d.FormularioUsuario).WithOne(p => p.TablaFormularioUsuario)
            //    .HasForeignKey<TablaFormularioUsuario>(d => d.FormularioUsuarioId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("tabla_formulario_usuario_ibfk_1");

            //entity.HasOne(d => d.Usuario).WithMany(p => p.TablaFormularioUsuarios)
            //    .HasForeignKey(d => d.UsuarioId)
            //    .HasConstraintName("tabla_formulario_usuario_ibfk_2");
        });

        modelBuilder.Entity<TipoCandidatura>(entity =>
        {
            entity.HasKey(e => e.TipoCandidaturaId).HasName("PRIMARY");

            entity.ToTable("tipo_candidatura");

            entity.Property(e => e.TipoCandidaturaId).HasColumnName("tipoCandidaturaId");
            entity.Property(e => e.NombreTipoCandidatura)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("nombreTipoCandidatura");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.RolId, "rolId");

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.Estatus)
                .HasColumnType("bit(1)")
                .HasColumnName("estatus");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("password");
            entity.Property(e => e.RolId).HasColumnName("rolId");

            //entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
            //    .HasForeignKey(d => d.RolId)
            //    .HasConstraintName("usuario_ibfk_1");
        });

        modelBuilder.Entity<UsuarioAutenticado>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PRIMARY");

            entity.ToTable("usuario_autenticado");

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .HasColumnName("clave");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .HasColumnName("nombreUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
