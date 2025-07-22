using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class TesteExtensions
{
    public static Teste ParaEntidade(this FormularioTesteViewModel formularioVM, List<Dominio.ModuloDisciplina.Disciplina> disciplinas, List<Materia> materias) {

        Dominio.ModuloDisciplina.Disciplina? disciplinaSelecionada = null;

        foreach (var d in disciplinas) {
            if (d.Id.Equals(formularioVM.DisciplinaId)) {
                disciplinaSelecionada = d;
                break;
            }
        }

        //testar
        var materiaSelecionada = materias.FirstOrDefault(m => m.Id == formularioVM.MateriaId);

        return new Teste(
            formularioVM.Titulo,
            disciplinaSelecionada!,
            materiaSelecionada,
            formularioVM.Serie,
            formularioVM.QuantidadeQuestoes,
            formularioVM.TipoTeste
        );
    }

    public static DetalhesTesteViewModel ParaDetalhesVM(this Teste teste) {
        return new DetalhesTesteViewModel(
                teste.Id,
                teste.Titulo,
                teste.Disciplina?.Nome!,
                teste.Materia?.Nome!,
                teste.Serie,
                teste.TipoTeste,
                teste.QuantidadeQuestoes
        );
    }
}
