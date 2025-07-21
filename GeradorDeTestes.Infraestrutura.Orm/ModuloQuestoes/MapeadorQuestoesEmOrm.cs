using GeradorDeTestes.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    public class MapeadorQuestoesEmOrm : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(x => x.Enunciado)
                .IsRequired();
            builder.Property(x => x.FoiAcertada)
                .IsRequired();
            builder.HasMany(q => q.Alternativas)
                .WithOne(a => a.Questao)
                .HasForeignKey(a => a.QuestaoId)
                .HasConstraintName("FK_TBAlternativa_TBQuestao");
        }
    }
}
