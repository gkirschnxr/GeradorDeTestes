using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;

public class FormularioMateriaViewModel 
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Nome\" não pode conter menos que dois caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Nome\" não pode conter mais que dois caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo \"Disciplina\" é obrigatório.")]
    public Guid? DisciplinaId { get; set; }
    public List<SelectListItem>? DisciplinasDisponiveis { get; set; }

    [Required(ErrorMessage = "O campo \"Série\" é obrigatório.")]
    public Serie Serie { get; set; }
}

public class CadastrarMateriaViewModel : FormularioMateriaViewModel
{
    public CadastrarMateriaViewModel() { 
        DisciplinasDisponiveis = new List<SelectListItem>();
    }
    public CadastrarMateriaViewModel(List<Disciplina> disciplinas) : this() {
        foreach (var d in disciplinas) {
            var selecionarVM = new SelectListItem(d.Nome, d.Id.ToString());

            DisciplinasDisponiveis?.Add(selecionarVM);
        }
    }
}

public class EditarMateriaViewModel : FormularioMateriaViewModel 
{
    public Guid Id { get; set; }

    public EditarMateriaViewModel() {
        DisciplinasDisponiveis = new List<SelectListItem>();
    }
    public EditarMateriaViewModel(Guid id, string nome, Guid? disciplinaId, List<Disciplina> disciplinas, Serie serie) : this() {
        Id = id;
        Nome = nome;
        DisciplinaId = disciplinaId;

        foreach (var d in disciplinas) {
            var selecionarVM = new SelectListItem(d.Nome, d.Id.ToString());

            DisciplinasDisponiveis?.Add(selecionarVM);
        }

        Serie = serie;
    }
}

public class ExcluirMateriaViewModel 
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ExcluirMateriaViewModel(Guid id, string nome) {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarMateriasViewModel
{
    public List<DetalhesMateriasViewModel> Registros { get; set; }

    public VisualizarMateriasViewModel(List<Materia> Materias) {
        Registros = new List<DetalhesMateriasViewModel>();

        foreach (var t in Materias) {
            Registros.Add(t.ParaDetalhesVM());
        }
    }
}

public class DetalhesMateriasViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Disciplina { get; set; }
    public Serie Serie { get; set; }

    public DetalhesMateriasViewModel(Guid id, string nome, string disciplina, Serie serie) {
        Id = id;
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }
}