using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("testes")]
public class TesteController : Controller {
    private readonly IRepositorioTeste _repositorioTeste;
    private readonly IRepositorioDisciplina _repositorioDisciplina;
    private readonly IRepositorioMateria _repositorioMateria;
    private readonly IRepositorioQuestao _repositorioQuestao;

    public TesteController(IRepositorioTeste repositorioTeste, IRepositorioDisciplina repositorioDisciplina,
                          IRepositorioMateria repositorioMateria, IRepositorioQuestao repositorioQuestao) {
        _repositorioTeste = repositorioTeste;
        _repositorioDisciplina = repositorioDisciplina;
        _repositorioMateria = repositorioMateria;
        _repositorioQuestao = repositorioQuestao;
    }

    [HttpGet]
    public IActionResult Index() {
        var registros = _repositorioTeste.SelecionarRegistros();

        var visualizarVM = new VisualizarTestesViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("gerar")]
    public IActionResult Gerar() {
        var gerarVM = new GerarTesteViewModel {
            Disciplinas = _repositorioDisciplina.SelecionarRegistros(),
            Materias = _repositorioMateria.SelecionarRegistros()
        };
        
        return View(gerarVM);
    }

    [HttpPost("gerar")]
    [ValidateAntiForgeryToken]
    public IActionResult Gerar(GerarTesteViewModel gerarVM) {
        var registros = _repositorioTeste.SelecionarRegistros();

        if (registros.Any(x => x.Titulo.Equals(gerarVM.Titulo)))
            ModelState.AddModelError("CadastroUnico", "Já existe um teste com este título.");

        var disciplinas = _repositorioDisciplina.SelecionarRegistros();
        var materias = _repositorioMateria.SelecionarRegistros();

        if (!ModelState.IsValid) {
            gerarVM.Disciplinas = disciplinas;
            gerarVM.Materias = materias;
            return View(gerarVM);
        }

        var entidade = gerarVM.ParaEntidade(disciplinas, materias);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("preview")]
    [ValidateAntiForgeryToken]
    public IActionResult Preview(GerarTesteViewModel gerarVM) {
        var questoes = _repositorioQuestao.SelecionarRegistros()
            .Where(q => q.Materia.Any(m => m.Id == gerarVM.MateriaId && m.Serie == gerarVM.Serie))
            .ToList();

        if (gerarVM.QuantidadeQuestoes > questoes.Count) {
            ModelState.AddModelError(nameof(gerarVM.QuantidadeQuestoes), "Quantidade maior do que o número de questões disponíveis.");
        }

        gerarVM.Disciplinas = _repositorioDisciplina.SelecionarRegistros();
        gerarVM.Materias = _repositorioMateria.SelecionarRegistros();

        gerarVM.QuestoesSorteadas = questoes.OrderBy(_ => Guid.NewGuid())
                                            .Take(gerarVM.QuantidadeQuestoes)
                                            .ToList();

        return View("Gerar", gerarVM);
    }
}
