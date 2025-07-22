using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Models;
using System.Runtime.CompilerServices;

namespace GeradorDeTestes.WebApp.Extensions;

public static class MateriaExtensions
{
    public static Materia ParaEntidade(this FormularioMateriaViewModel formularioVM) {
        return new Materia(formularioVM.Nome, formularioVM.Disciplina!, (Serie)formularioVM.Serie);
    }

    public static DetalhesMateriasViewModel ParaDetalhesVM(this Materia Materia) {
        return new DetalhesMateriasViewModel(Materia.Id, Materia.Nome, Materia.Disciplina!, Materia.Serie);
    }
}
