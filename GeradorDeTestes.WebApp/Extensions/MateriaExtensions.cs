using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Models;
using System.Runtime.CompilerServices;

namespace GeradorDeTestes.WebApp.Extensions;

public static class MateriaExtensions
{
    public static Materia ParaEntidade(this FormularioMateriaViewModel formularioVM, List<Disciplina> disciplinas) {
        
        Disciplina disciplinaSelecionada = null!;

        foreach (var d in disciplinas) {
            disciplinaSelecionada = disciplinas.FirstOrDefault(d => d.Id == formularioVM.DisciplinaId)!;
        }

        return new Materia(formularioVM.Nome, disciplinaSelecionada!, formularioVM.Serie);
    }

    public static DetalhesMateriasViewModel ParaDetalhesVM(this Materia materia) {
        return new DetalhesMateriasViewModel(materia.Id, materia.Nome, materia.Disciplina.Nome, materia.Serie);
    }
}
