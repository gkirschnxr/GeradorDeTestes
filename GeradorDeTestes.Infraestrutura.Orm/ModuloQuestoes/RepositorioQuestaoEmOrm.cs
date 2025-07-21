using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    public class RepositorioQuestaoEmOrm : RepositorioBaseEmOrm<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
        {
        }
    }
}
