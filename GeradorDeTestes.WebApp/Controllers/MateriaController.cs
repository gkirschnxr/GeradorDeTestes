using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("materias")]
public class MateriaController : Controller
{
    private readonly GeradorDeTestesDbContext _contexto;
    private readonly IRepositorioMateria _repositorioMateria;

    public MateriaController(GeradorDeTestesDbContext contexto, IRepositorioMateria repositorioMateria) {
        _contexto = contexto;
        _repositorioMateria = repositorioMateria;
    }


    [HttpGet]
    public IActionResult Index() {
        var registros = _repositorioMateria.SelecionarRegistros();

        var visualizarVM = new VisualizarMateriasViewModel(registros);

        return View(visualizarVM);
    }


    [HttpGet("cadastrar")]
    public IActionResult Cadastrar() {
        var cadastrarVM = new CadastrarMateriaViewModel();

        return View(cadastrarVM);
    }


    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarMateriaViewModel cadastrarVM) {
        var registro = cadastrarVM.ParaEntidade();

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
        var registro = _repositorioMateria.SelecionarRegistroPorId(id);

        var editarVM = new EditarMateriaViewModel(id, registro!.Nome, registro.Disciplina, registro.Serie);

        return View(editarVM);
    }


    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarMateriaViewModel editarVM) {
        var registros = _repositorioMateria.SelecionarRegistros();

        var registroEditado = editarVM.ParaEntidade();

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

        var detalhesVM = new DetalhesMateriasViewModel(id, registro!.Nome, registro.Disciplina, registro.Serie);

        return View(detalhesVM);
    }
}
