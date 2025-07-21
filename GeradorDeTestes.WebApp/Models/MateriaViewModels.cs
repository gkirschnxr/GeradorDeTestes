using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;

public class FormularioMateriaViewModel 
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo \"Disciplina\" é obrigatório.")]
    public TipoDisciplina Disciplina { get; set; }

    [Required(ErrorMessage = "O campo \"Série\" é obrigatório.")]
    public TipoSerie Serie { get; set; }
}

public class CadastrarMateriaViewModel : FormularioMateriaViewModel
{
    public CadastrarMateriaViewModel() { }
    public CadastrarMateriaViewModel(string nome, TipoDisciplina disciplina, TipoSerie serie) : this() {
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }
}

public class EditarMateriaViewModel : FormularioMateriaViewModel 
{
    public Guid Id { get; set; }

    public EditarMateriaViewModel() { }
    public EditarMateriaViewModel(Guid id, string nome, TipoDisciplina disciplina, TipoSerie serie) : this() {
        Id = id;
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }
}

public class ExcluirMateriaViewModel 
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ExcluirMateriaViewModel() { }
    public ExcluirMateriaViewModel(Guid id, string nome) : this() {
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
    public TipoDisciplina Disciplina { get; set; }
    public TipoSerie Serie { get; set; }

    public DetalhesMateriasViewModel(Guid id, string nome, TipoDisciplina disciplina, TipoSerie serie) {
        Id = id;
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }
}