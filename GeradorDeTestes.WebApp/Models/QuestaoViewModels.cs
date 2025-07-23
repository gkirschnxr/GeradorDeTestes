using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;

    public class FormularioQuestaoViewModels
    {
        [Required(ErrorMessage = "O campo 'Enunciado' é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo 'Enunciado' precisa conter ao menos 2 caracteres.")]
        public string? Enunciado { get; set; }
        public Guid MateriaId { get; set; }
        public List<SelectListItem> MateriasDisponiveis { get; set; } = new List<SelectListItem>();
        public List<AlternativaQuestaoViewModel> AlternativasSelecionadas { get; set; } = new List<AlternativaQuestaoViewModel>();

        public void AdicionarAlternativa(AdicionarAlternativaQuestaoViewModel alternativaVM)
        {
            var letra = (char)('A' + AlternativasSelecionadas.Count);

            var novaAlternativa = new AlternativaQuestaoViewModel(
                letra,
                alternativaVM.Resposta,
                alternativaVM.Correta);

            AlternativasSelecionadas.Add(novaAlternativa);
        }

        public void RemoverAlternativa(AlternativaQuestaoViewModel alternativaVm)
        {
            if (!AlternativasSelecionadas.Contains(alternativaVm))
                return;

            AlternativasSelecionadas?.Remove(alternativaVm);
            ReatribuirLetras();
        }

        private void ReatribuirLetras()
        {
            if (AlternativasSelecionadas == null)
                return;

            for (int i = 0; i < AlternativasSelecionadas.Count; i++)
                AlternativasSelecionadas[i].Letra = (char)('A' + i);
        }
    }

    public class CadastrarQuestaoViewModel : FormularioQuestaoViewModels
    {
        public CadastrarQuestaoViewModel() { }

        public CadastrarQuestaoViewModel(List<Materia> materias)
        {
            MateriasDisponiveis = materias
                .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
                .ToList();
        }

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
}

    public class EditarQuestaoViewModel : FormularioQuestaoViewModels
    {
        public Guid Id { get; set; }
        public EditarQuestaoViewModel() { }
        public EditarQuestaoViewModel(
            Guid id,
            string enunciado,
            Guid materiaId,
            List<Alternativa> alternativas,
            List<Materia> materias
            ) : this()
        {
            Id = id;
            Enunciado = enunciado;
            MateriaId = materiaId;

            AlternativasSelecionadas = alternativas
             .Select(a => new AlternativaQuestaoViewModel(a.Letra, a.Resposta, a.Correta))
             .ToList();

            MateriasDisponiveis = materias
                .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
                .ToList();

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
            {
                var detalhesVm = DetalhesQuestaoViewModel.ParaDetalhesVm();
                Registros.Add(detalhesVm);
            }
        }
    }

public class DetalhesQuestaoViewModel
{
    public Guid Id { get; set; }
    public string Enunciado { get; set; }
    public string Materia { get; set; }
    public string UtilizadaEmTeste { get; set; }
    public string RespostaCorreta { get; set; }
    public List<AlternativaQuestaoViewModel> Alternativas { get; set; }

    public DetalhesQuestaoViewModel(
        Guid id,
        string enunciado,
        string materia,
        string utilizadaEmTeste,
        string respostaCorreta,
        List<Alternativa> alternativas
    )
    {
        Id = id;
        Enunciado = enunciado;
        Materia = materia;
        UtilizadaEmTeste = utilizadaEmTeste;
        RespostaCorreta = respostaCorreta;

        Alternativas = alternativas
            .Select(a => new AlternativaQuestaoViewModel(a.Letra, a.Resposta, a.Correta))
            .ToList();
    }

    public static DetalhesQuestaoViewModel ParaDetalhesVm(Questao questao)
    {
        return new DetalhesQuestaoViewModel(
            questao.Id,
            questao.Enunciado,
            questao.Materias.Nome,
            questao.UtilizadaEmTeste ? "Sim" : "Não",
            questao.AlternativaCorreta?.Resposta ?? string.Empty,
            questao.Alternativas
        );
    }
}


    public class AlternativaQuestaoViewModel
    {
        public char Letra { get; set; }
        public string Resposta { get; set; } = string.Empty;
        public bool Correta { get; set; }

        public AlternativaQuestaoViewModel() { }

        public AlternativaQuestaoViewModel(char letra, string resposta, bool correta)
        {
            Letra = letra;
            Resposta = resposta;
            Correta = correta;
        }
    }

    public class AdicionarAlternativaQuestaoViewModel
    {
        public string Resposta { get; set; } = string.Empty;
        public bool Correta { get; set; }

        public AdicionarAlternativaQuestaoViewModel() { }

        public AdicionarAlternativaQuestaoViewModel(string resposta, bool correta) : this()
        {
            Resposta = resposta;
            Correta = correta;
        }
    }


