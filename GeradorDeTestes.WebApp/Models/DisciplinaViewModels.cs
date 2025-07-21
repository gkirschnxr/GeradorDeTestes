using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models
{
    public class FormularioDisciplinaViewModel
    {
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.")]
        public string? Nome { get; set; }
    }

    public class CadastrarDisciplinaViewModel : FormularioDisciplinaViewModel
    {
        public CadastrarDisciplinaViewModel() { }

        public CadastrarDisciplinaViewModel(string nome) : this()
        {
            Nome = nome;
        }
    }

    public class EditarDisciplinaViewModel : FormularioDisciplinaViewModel
    {
        public Guid Id { get; set; }

        public EditarDisciplinaViewModel() { }

        public EditarDisciplinaViewModel(Guid id, string nome) : this()
        {
            Id = id;
            Nome = nome;
        }
    }

    public class ExcluirDisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ExcluirDisciplinaViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class VisualizarDisciplinaViewModel
    {
        public List<DetalhesDisciplinaViewModel> Registros { get; set; }

        public VisualizarDisciplinaViewModel(List<Disciplina> disciplinas)
        {
            Registros = new List<DetalhesDisciplinaViewModel>();

            foreach (var g in disciplinas)
                Registros.Add(g.ParaDetalhesVM());
        }
    }

    public class DetalhesDisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        //public List<DetalhesMateriasViewModel> Materia { get; set; }
        public decimal TotalDespesas { get; set; }

        public DetalhesDisciplinaViewModel(Guid id, string nome)//, List<Materia> materias)
        {
            Id = id;
            Nome = nome;

          //  Materias = new List<DetalhesMateriasViewModel>();

          //  foreach (var d in materias)
          //  {
          //      TotalDespesas += d.materias;

        //        var detalhesDespesaVM = new DetalhesMateriasViewModel(
        //            d.Id,
        //            d.Materias
       //         );

      //          Materias.Add(detalhesDespesaVM);
       //     }
        }
    }
}
