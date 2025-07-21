using GeradorDeTestes.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.HasOne(x => x.Questao)
                .WithMany(x => x.Alternativas)
                .IsRequired(); 
                
        }
    }
}
