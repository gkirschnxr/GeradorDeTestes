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
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Serie)
            .IsRequired();

        builder.HasOne(m => m.Disciplina)
            .WithMany(d => d.Materias);

        builder.HasMany(m => m.Questoes)
            .WithOne(q => q.Materia);

        builder.HasMany(m => m.Testes)
            .WithOne(t => t.Materia);
    }
}
