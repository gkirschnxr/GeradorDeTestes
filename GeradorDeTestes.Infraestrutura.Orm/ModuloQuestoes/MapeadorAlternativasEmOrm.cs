using GeradorDeTestes.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    internal class MapeadorAlternativasEmOrm : IEntityTypeConfiguration<AlternativaQuestao>
    {
        public void Configure(EntityTypeBuilder<AlternativaQuestao> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(x => x.Texto)
                .IsRequired();
            builder.Property(x => x.Correta)
                .IsRequired();
            builder.Property(x => x.QuestaoId)
                .IsRequired();
            builder.HasOne(a => a.Questao)
                .WithMany(x => x.Alternativas)
                .IsRequired();


        }
    }
}
