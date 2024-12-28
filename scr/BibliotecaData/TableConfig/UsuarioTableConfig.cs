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
                .HasMaxLength(30);

            builder.Property(usuario => usuario.Nascimento)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(usuario => usuario.Telefone)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(usuario => usuario.Email)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(usuario => usuario.Endereco)
              .IsRequired()
              .HasMaxLength(15);

            builder.Property(usuario => usuario.DataCriacao)
              .IsRequired()
              .HasMaxLength(15);

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
