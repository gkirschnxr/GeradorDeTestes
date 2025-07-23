using GeradorDeTestes.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    public class MapeadorQuestoesEmOrm : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.Property(q => q.Id)
            .ValueGeneratedNever()
            .IsRequired();

            builder.Property(q => q.Enunciado)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(q => q.UtilizadaEmTeste)
                .IsRequired();

            builder.HasOne(q => q.Materias)
                .WithMany(m => m.Questoes)
                .IsRequired();

            builder.HasMany(q => q.Alternativas)
                .WithOne(a => a.Questao);
        }
    }
}
