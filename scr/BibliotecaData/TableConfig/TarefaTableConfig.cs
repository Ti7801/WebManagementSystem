using BibliotecaBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaData.TableConfig
{
    public class TarefaTableConfig : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefa");

            builder.HasKey(tarefa => tarefa.Id);

            builder.Property(tarefa => tarefa.Messagem)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(tarefa => tarefa.DataCriacao)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(tarefa => tarefa.DataConclusao)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(tarefa => tarefa.StatusTarefa)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
