using GeradorDeTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
public class MapeadorMateriaEmOrm : IEntityTypeConfiguration<Materia>
{
    public void Configure(EntityTypeBuilder<Materia> builder) {
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(m => m.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Serie)
            .IsRequired();

        builder.Property(m => m.Disciplina)
            .IsRequired();
            //.WithMany(d => d.Materia)
    }
}
