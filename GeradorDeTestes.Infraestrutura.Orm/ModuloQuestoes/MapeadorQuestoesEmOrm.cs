using GeradorDeTestes.Dominio.ModuloQuestoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    public class MapeadorQuestoesEmOrm : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            
        }
    }
}
