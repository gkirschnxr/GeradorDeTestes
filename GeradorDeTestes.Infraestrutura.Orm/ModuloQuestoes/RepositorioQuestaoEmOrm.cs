using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;


namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestoes
{
    public class RepositorioQuestaoEmOrm : RepositorioBaseEmOrm<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
        {
        }

        public List<Questao> SelecionarQuestoesPorDisciplinaESerie(Guid disciplinaId, Serie serie, int quantidadeQuestoes)
        {
            return registros
            .Include(q => q.Alternativas)
            .Include(q => q.Materias)
            .ThenInclude(m => m.Disciplina)
            .Where(x => x.Materias.Disciplina.Id.Equals(disciplinaId))
            .Where(x => x.Materias.Serie.Equals(serie))
            .Take(quantidadeQuestoes)
            .ToList();
        }

        public List<Questao> SelecionarQuestoesPorMateria(Guid materiaId, int quantidadeQuestoes)
        {
            return registros 
            .Include(q => q.Alternativas)
            .Include(q => q.Materias)
            .Where(x => x.Materias.Id.Equals(materiaId))
            .Take(quantidadeQuestoes)
            .ToList();
        }
    }
}
