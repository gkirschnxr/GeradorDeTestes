using GeradorDeTestes.Dominio.Disciplina;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
public class RepositorioDisciplina : RepositorioBase<Disciplina>, IRepositorioDisciplina
{
    public RepositorioDisciplina(ContextoDados contexto) : base(contexto) { }

    protected override List<Disciplina> ObterRegistros()
    {
        return contexto.Disciplinas;
    }
}
