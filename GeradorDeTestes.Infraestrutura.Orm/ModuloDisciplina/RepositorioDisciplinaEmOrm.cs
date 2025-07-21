using GeradorDeTestes.Dominio.Disciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
public class RepositorioDisciplinaEmOrm : RepositorioBaseEmOrm<Disciplina>, IRepositorioDisciplina
{
    public RepositorioDisciplinaEmOrm(GeradorDeTestesDbContext contexto) : base(contexto) { }
}
