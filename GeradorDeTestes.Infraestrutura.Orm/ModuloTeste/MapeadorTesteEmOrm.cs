using GeradorDeTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
public class MapeadorTesteEmOrm : IEntityTypeConfiguration<Teste>
{
    public void Configure(EntityTypeBuilder<Teste> builder) {
        builder.Property(t => t.Id)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(t => t.Titulo)
                .HasMaxLength(200)
                .IsRequired();

        builder.HasOne(t => t.Disciplina)
                .WithMany(d => d.Testes)
                .IsRequired();

        builder.HasOne(t => t.Materia)
                .WithMany(m => m.Testes)
                .IsRequired(false);

        builder.Property(t => t.Serie)
                .IsRequired();

        builder.Property(t => t.QuantidadeQuestoes)
                .IsRequired();

        builder.Property(t => t.TipoTeste)
                .IsRequired();

        builder.HasMany(t => t.Questoes)
                .WithMany(q => q.Testes);
    }
}
