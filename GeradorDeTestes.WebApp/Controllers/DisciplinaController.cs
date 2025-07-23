using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers
{
    [Route("disciplinas")]
    public class DisciplinaController : Controller
    {
        private readonly GeradorDeTestesDbContext contexto;
        private readonly IRepositorioDisciplina repositorioDisciplina;

        public DisciplinaController(IRepositorioDisciplina repositorioDisciplina, GeradorDeTestesDbContext contexto) {
            this.repositorioDisciplina = repositorioDisciplina;
            this.contexto = contexto;
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

            var transacao = contexto.Database.BeginTransaction();

            try {
                repositorioDisciplina.CadastrarRegistro(entidade);

                contexto.SaveChanges();

                transacao.Commit();

            } catch (Exception) {
                transacao.Rollback();

                throw;
            }

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

            var transacao = contexto.Database.BeginTransaction();

            try {
                repositorioDisciplina.EditarRegistro(id, entidadeEditada);

                contexto.SaveChanges();

                transacao.Commit();

            } catch (Exception) {
                transacao.Rollback();

                throw;
            }

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
            var transacao = contexto.Database.BeginTransaction();

            try {
                repositorioDisciplina.ExcluirRegistro(id);

                contexto.SaveChanges();

                transacao.Commit();

            } catch (Exception) {
                transacao.Rollback();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("detalhes/{id:guid}")]
        public IActionResult Detalhes(Guid id)
        {
            var registroSelecionado = repositorioDisciplina.SelecionarRegistroPorId(id);

            if (registroSelecionado is null)
                return RedirectToAction(nameof(Index));

            var detalhesVM = new DetalhesDisciplinaViewModel(
                id,
                registroSelecionado!.Nome,
                registroSelecionado.Materias
            );

            return View(detalhesVM);
        }
    }
}
