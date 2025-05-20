using BibliotecaBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaData.TableConfig
{
    public class UsuarioTableConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(usuario => usuario.Id);

            builder.Property(usuario => usuario.Nome)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(usuario => usuario.Nascimento)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(usuario => usuario.Telefone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(usuario => usuario.Celular)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(usuario => usuario.Email)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(usuario => usuario.Endereco)
              .IsRequired()
              .HasMaxLength(100);

            builder.Property(usuario => usuario.Foto)
                .IsRequired();

            builder.HasMany<Tarefa>()
                .WithOne()
                .HasForeignKey(tarefa => tarefa.GestorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany<Tarefa>()
                .WithOne()
                .HasForeignKey(tarefa => tarefa.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
