using GeradorDeTestes.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    public class MapeadorAlternativasEmOrm : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(a => a.Letra)
           .IsRequired();
            builder.Property(a => a.Resposta)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(a => a.Correta)
                .IsRequired();
            builder.HasOne(a => a.Questao)
                .WithMany(q => q.Alternativas)
                .IsRequired();



        }
    }
}
