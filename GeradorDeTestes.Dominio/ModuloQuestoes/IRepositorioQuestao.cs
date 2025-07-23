using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;

namespace GeradorDeTestes.Dominio.ModuloQuestoes
{
    public interface IRepositorioQuestao : IRepositorioBase<Questao>
    {
        List<Questao> SelecionarQuestoesPorDisciplinaESerie(Guid disciplinaId, Serie serie, int quantidadeQuestoes);
        List<Questao> SelecionarQuestoesPorMateria(Guid materiaId, int quantidadeQuestoes);
    }
}
