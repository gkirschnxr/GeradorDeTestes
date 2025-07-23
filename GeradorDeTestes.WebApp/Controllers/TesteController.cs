using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("testes")]
public class TesteController : Controller 
{
    public GeradorDeTestesDbContext _contexto;
    private readonly IRepositorioTeste _repositorioTeste;
    private readonly IRepositorioDisciplina _repositorioDisciplina;
    private readonly IRepositorioMateria _repositorioMateria;
    private readonly IRepositorioQuestao _repositorioQuestao;

    public TesteController(IRepositorioTeste repositorioTeste, IRepositorioDisciplina repositorioDisciplina,
                          IRepositorioMateria repositorioMateria, IRepositorioQuestao repositorioQuestao, GeradorDeTestesDbContext contexto) {
        _repositorioTeste = repositorioTeste;
        _repositorioDisciplina = repositorioDisciplina;
        _repositorioMateria = repositorioMateria;
        _repositorioQuestao = repositorioQuestao;
        _contexto = contexto;
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

        var transacao = _contexto.Database.BeginTransaction();

        try {
            _repositorioTeste.CadastrarRegistro(entidade);

            _contexto.SaveChanges();

            transacao.Commit();

        }
        catch (Exception) {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("preview")]
    [ValidateAntiForgeryToken]
    public IActionResult Preview(GerarTesteViewModel gerarVM) {
        var questoes = _repositorioQuestao.SelecionarRegistros()
            .Where(q => q.Materias is not null &&
                        q.Materias.Id == gerarVM.MateriaId &&
                        q.Materias.Serie == gerarVM.Serie)
            .ToList();

        if (gerarVM.QuantidadeQuestoes > questoes.Count) {
            ModelState.AddModelError(nameof(gerarVM.QuantidadeQuestoes), "Quantidade maior que o número de questões disponíveis!");
        }

        gerarVM.Disciplinas = _repositorioDisciplina.SelecionarRegistros();
        gerarVM.Materias = _repositorioMateria.SelecionarRegistros();

        gerarVM.QuestoesSorteadas = questoes.OrderBy(_ => Guid.NewGuid()).Take(gerarVM.QuantidadeQuestoes).ToList();

        return View("Gerar", gerarVM);
    }

    [HttpGet("duplicar/{id:guid}")]
    public IActionResult Duplicar(Guid id) {
        Teste? testeDuplicado = _repositorioTeste.SelecionarRegistroPorId(id);

        var disciplinas = _repositorioDisciplina.SelecionarRegistros();
        var materias = _repositorioMateria.SelecionarRegistros();

        var duplicarVM = new DuplicarTesteViewModel(testeDuplicado!);

        duplicarVM.Disciplinas = disciplinas;
        duplicarVM.Materias = materias;

        return View(duplicarVM);
    }

    [HttpPost("duplicar/preview")]
    [ValidateAntiForgeryToken]
    public IActionResult Preview(DuplicarTesteViewModel duplicarVM) {
        var questoes = _repositorioQuestao.SelecionarRegistros()
            .Where(q => q.Materias is not null &&
                        q.Materias.Id == duplicarVM.MateriaId &&
                        q.Materias.Serie == duplicarVM.Serie)
            .ToList();

        if (duplicarVM.QuantidadeQuestoes > questoes.Count) {
            ModelState.AddModelError(nameof(duplicarVM.QuantidadeQuestoes), "Quantidade maior que o número de questões disponíveis!");
        }

        duplicarVM.Disciplinas = _repositorioDisciplina.SelecionarRegistros();
        duplicarVM.Materias = _repositorioMateria.SelecionarRegistros();

        duplicarVM.QuestoesSorteadas = questoes.OrderBy(_ => Guid.NewGuid()).Take(duplicarVM.QuantidadeQuestoes).ToList();

        return View("Duplicar", duplicarVM);
    }

    [HttpPost("duplicar")]
    [ValidateAntiForgeryToken]
    public IActionResult Duplicar(DuplicarTesteViewModel duplicarVM) {
        var registros = _repositorioTeste.SelecionarRegistros();

        if (registros.Any(x => x.Titulo.Equals(duplicarVM.Titulo)))
            ModelState.AddModelError("CadastroUnico", "Já existe um teste com este título.");

        var disciplinas = _repositorioDisciplina.SelecionarRegistros();
        var materias = _repositorioMateria.SelecionarRegistros();

        if (!ModelState.IsValid) {
            duplicarVM.Disciplinas = disciplinas;
            duplicarVM.Materias = materias;
            return View(duplicarVM);
        }

        var entidade = duplicarVM.ParaEntidade(disciplinas, materias);

        var transacao = _contexto.Database.BeginTransaction();

        try {
            _repositorioTeste.CadastrarRegistro(entidade);

            _contexto.SaveChanges();

            transacao.Commit();
        } 
        catch (Exception) {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id) {
        var registro = _repositorioTeste.SelecionarRegistroPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var excluirVM = new ExcluirTesteViewModel(registro.Id, registro.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(Guid id, ExcluirTesteViewModel excluirVM) {
        var transacao = _contexto.Database.BeginTransaction();

        try {
            _repositorioTeste.ExcluirRegistro(id);

            _contexto.SaveChanges();

            transacao.Commit();
        }
        catch (Exception) {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id) {
        var registro = _repositorioTeste.SelecionarRegistroPorId(id);

        var questoes = _repositorioQuestao.SelecionarRegistros()
            .Where(q => q.Materias is not null &&
                        q.Materias.Id == registro!.Materia!.Id &&
                        q.Materias.Serie == registro.Materia.Serie)
            .ToList();

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var detalhesVM = new DetalhesTesteViewModel(
            registro.Id,
            registro.Titulo,
            registro.Disciplina!.Nome,
            registro.Materia!.Nome,
            registro.Serie,
            registro.TipoTeste,
            registro.QuantidadeQuestoes
        );

        detalhesVM.QuestoesSorteadas = questoes;

        return View(detalhesVM);
    }
}
