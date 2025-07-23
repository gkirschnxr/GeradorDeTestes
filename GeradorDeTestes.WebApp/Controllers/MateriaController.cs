using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("materias")]
public class MateriaController : Controller
{
    private readonly GeradorDeTestesDbContext _contexto;
    private readonly IRepositorioMateria _repositorioMateria;
    private readonly IRepositorioDisciplina _repositorioDisciplina;

    public MateriaController(GeradorDeTestesDbContext contexto, IRepositorioMateria repositorioMateria,
                            IRepositorioDisciplina repositorioDisciplina) {
        _contexto = contexto;
        _repositorioMateria = repositorioMateria;
        _repositorioDisciplina = repositorioDisciplina;
    }


    [HttpGet]
    public IActionResult Index() {
        var registros = _repositorioMateria.SelecionarRegistros();

        var visualizarVM = new VisualizarMateriasViewModel(registros);

        return View(visualizarVM);
    }


    [HttpGet("cadastrar")]
    public IActionResult Cadastrar() {
        var disciplinasDisponiveis = _repositorioDisciplina.SelecionarRegistros();        

        var cadastrarVM = new CadastrarMateriaViewModel(disciplinasDisponiveis);

        return View(cadastrarVM);
    }


    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarMateriaViewModel cadastrarVM) {
        var disciplinasDisponiveis = _repositorioDisciplina.SelecionarRegistros();

        if (!ModelState.IsValid) {

            foreach (var dD in disciplinasDisponiveis) {
                var selecionarVM = new SelectListItem(dD.Nome, dD.Id.ToString());

                cadastrarVM.DisciplinasDisponiveis!.Add(selecionarVM);
            }

            return View(cadastrarVM);
        }

        var registro = cadastrarVM.ParaEntidade(disciplinasDisponiveis);

        var transacao = _contexto.Database.BeginTransaction();

        try {
            _repositorioMateria.CadastrarRegistro(registro);

            _contexto.SaveChanges();

            transacao.Commit();
        }
        catch (Exception) {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id) {
        var disciplinasDisponiveis = _repositorioDisciplina.SelecionarRegistros();

        var registroSelecionado = _repositorioMateria.SelecionarRegistroPorId(id);

        if (registroSelecionado != null) return RedirectToAction(nameof(Index));

        var editarVM = new EditarMateriaViewModel(id, registroSelecionado!.Nome, registroSelecionado.Disciplina!.Id, disciplinasDisponiveis, registroSelecionado.Serie);

        return View(editarVM);
    }


    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarMateriaViewModel editarVM) {
        var disciplinasDisponiveis = _repositorioDisciplina.SelecionarRegistros();

        if (!ModelState.IsValid) {

            foreach (var dD in disciplinasDisponiveis) {
                var selecionarVM = new SelectListItem(dD.Nome, dD.Id.ToString());

                editarVM.DisciplinasDisponiveis!.Add(selecionarVM);
            }

            return View(editarVM);
        }

        var registroEditado = editarVM.ParaEntidade(disciplinasDisponiveis);

        var transacao = _contexto.Database.BeginTransaction();

        try {
            _repositorioMateria.EditarRegistro(id, registroEditado);

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
        var registro = _repositorioMateria.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirMateriaViewModel(registro!.Id, registro.Nome);

        return View(excluirVM);
    }


    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirRegistro(Guid id) {
        var transacao = _contexto.Database.BeginTransaction();

        try {
            _repositorioMateria.ExcluirRegistro(id);

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
        var registro = _repositorioMateria.SelecionarRegistroPorId(id);

        var detalhesVM = new DetalhesMateriasViewModel(id, registro!.Nome, registro.Disciplina.Nome, registro.Serie);

        return View(detalhesVM);
    }
}
