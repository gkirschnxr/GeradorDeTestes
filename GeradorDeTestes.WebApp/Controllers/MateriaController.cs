using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("materias")]
public class MateriaController : Controller
{
    private readonly IRepositorioMateria _repositorioMateria;

    public MateriaController(IRepositorioMateria repositorioMateria) {
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
        _repositorioMateria.ExcluirRegistro(id);     

        return RedirectToAction(nameof(Index));
    }
}
