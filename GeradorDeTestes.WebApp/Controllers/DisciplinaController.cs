using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers
{
    [Route("disciplinas")]
    public class DisciplinaController : Controller
    {
        
        private readonly IRepositorioDisciplina repositorioDisciplina;

        public DisciplinaController(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioDisciplina.SelecionarRegistros();

            var visualizarVM = new VisualizarDisciplinaViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarDisciplinaViewModel();

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarDisciplinaViewModel cadastrarVM)
        {
            var registros = repositorioDisciplina.SelecionarRegistros();

            foreach (var item in registros)
            {
                if (item.Nome.Equals(cadastrarVM.Nome))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma Disciplina registrada com este título.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();

            repositorioDisciplina.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public ActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioDisciplina.SelecionarRegistroPorId(id);

            var editarVM = new EditarDisciplinaViewModel(
                id,
                registroSelecionado!.Nome
            );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Guid id, EditarDisciplinaViewModel editarVM)
        {
            var registros = repositorioDisciplina.SelecionarRegistros();

            foreach (var item in registros)
            {
                if (!item.Id.Equals(id) && item.Nome.Equals(editarVM.Nome))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma Disciplina registrada com este título.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(editarVM);

            var entidadeEditada = editarVM.ParaEntidade();

            repositorioDisciplina.EditarRegistro(id, entidadeEditada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioDisciplina.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirDisciplinaViewModel(registroSelecionado!.Id, registroSelecionado.Nome);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioDisciplina.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("detalhes/{id:guid}")]
        public IActionResult Detalhes(Guid id)
        {
            var registroSelecionado = repositorioDisciplina.SelecionarRegistroPorId(id);

            var detalhesVM = new DetalhesDisciplinaViewModel(
                id,
                registroSelecionado!.Nome
                //registroSelecionado.Materia
            );

            return View(detalhesVM);
        }
    }
}
