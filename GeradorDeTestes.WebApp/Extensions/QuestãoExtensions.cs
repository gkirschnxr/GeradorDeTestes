using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions
{
    public static class QuestãoExtensions
    {
        public static Questao ParaEntidade(this FormularioQuestaoViewModels formularioVM)
        {
            return new Questao(formularioVM.Enunciado, formularioVM.Correta);
        }

        public static DetalhesQuestaoViewModel ParaDetalhesVm(this Questao questao)
        {
            return new DetalhesQuestaoViewModel(
                questao.Id,
                questao.Enunciado,
                questao.FoiAcertada
                );
        }
    }
}
