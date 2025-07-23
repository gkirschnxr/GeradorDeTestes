using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions
{
    public static class QuestãoExtensions
    {
        public static Questao ParaEntidade(CadastrarQuestaoViewModel viewModel, List<Materia> materias)
        {
            Materia? materia = materias.Find(i => i.Id.Equals(viewModel.MateriaId));

            if (materia is null)
                throw new InvalidOperationException("A matéria requisitada selecionada não foi encontrada.");

            var questao = new Questao(viewModel.Enunciado ?? string.Empty, materia);

            if (viewModel.AlternativasSelecionadas is not null)
            {
                foreach (var a in viewModel.AlternativasSelecionadas)
                    questao.AdicionarAlternativa(a.Resposta, a.Correta);
            }

            return questao;
        }

        public static Questao ParaEntidade(EditarQuestaoViewModel viewModel, List<Materia> materias)
        {
            Materia? materia = materias.Find(i => i.Id.Equals(viewModel.MateriaId));

            if (materia is null)
                throw new InvalidOperationException("A matéria requisitada selecionada não foi encontrada.");

            var questao = new Questao(viewModel.Enunciado ?? string.Empty, materia);

            return questao;
        }
    }
}
