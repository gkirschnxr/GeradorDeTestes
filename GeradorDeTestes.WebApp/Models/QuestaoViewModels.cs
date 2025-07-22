using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models
{
    public class FormularioQuestaoViewModels
    {
        [Required(ErrorMessage = "O campo 'Enunciado' é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo 'Enunciado' precisa conter ao menos 2 caracteres.")]
        public string Enunciado { get; set; }
        public bool Correta { get; set; }
        public Guid MateriaId{ get; set; }
        public List<SelectListItem> MateriasDisponiveis { get; set; } = new List<SelectListItem>();
    }

    public class CadastrarQuestaoViewModel : FormularioQuestaoViewModels
    {
        public CadastrarQuestaoViewModel(){}

        public CadastrarQuestaoViewModel(string enunciado, bool correta)
        {
            Enunciado = enunciado;
            Correta = correta;
        }
    }

    public class EditarQuestaoViewModel : FormularioQuestaoViewModels
    {
        public Guid Id { get; set; }
        public EditarQuestaoViewModel(){}
        public EditarQuestaoViewModel(
            Guid id, 
            string enunciado, 
            bool correta,
            List<AlternativaQuestao> alternativas,
            Materia materias
            )
        {
            Id = id;
            Enunciado = enunciado;
            Correta = correta;
            
        }
    }

    public class ExcluirQuestaoViewModel 
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; }

        public ExcluirQuestaoViewModel(Guid id, string enunciado)
        {
            Id = id;
            Enunciado = enunciado;
        }
    }

    public class VisualizarQuestaoViewModel
    {
        public List<DetalhesQuestaoViewModel> Registros { get; set; }

        public VisualizarQuestaoViewModel(List<Questao> questoes)
        {
            Registros = new List<DetalhesQuestaoViewModel>();

            foreach (var q in questoes)
                Registros.Add(q.ParaDetalhesVm());
        }
    }

    public class DetalhesQuestaoViewModel
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; }
        public bool Correta { get; set; }

        public DetalhesQuestaoViewModel(Guid id, string enunciado, bool correta)
        {
            Id = id;
            Enunciado = enunciado;
            Correta = correta;
        }
    }

    public class AlternativaQuestaoViewModel 
    {
        public char Letra { get; set; }
        public string Resposta { get; set; }
        public bool Correta { get; set; }
        
        public AlternativaQuestaoViewModel(){}

        public AlternativaQuestaoViewModel(char letra, string resposta, bool correta)
        {
            Letra = letra;
            Resposta = resposta;
            Correta = correta;
        }
    }

    public class AdicionarAlternativaQuestaoViewModel 
    {
        public string Resposta { get; set; }
        public bool Correta { get; set; }

        public AdicionarAlternativaQuestaoViewModel(){}

        public AdicionarAlternativaQuestaoViewModel(string resposta, bool correta)
        {
            Resposta= resposta;
            Correta = correta;
        }
    }
}
