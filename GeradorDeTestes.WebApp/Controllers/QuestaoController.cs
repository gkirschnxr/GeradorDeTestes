using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers
{
    [Route("questoes")]
    public class QuestaoController : Controller
    {
        private readonly GeradorDeTestesDbContext contexto;
        private readonly IRepositorioQuestao repositorioQuestao;

        public QuestaoController(GeradorDeTestesDbContext contexto, IRepositorioQuestao repositorioQuestao)
        {
            this.contexto = contexto;
            this.repositorioQuestao = repositorioQuestao;
        }

        public IActionResult Index()
        {
            var registros = repositorioQuestao.SelecionarRegistros();

            var visualizarVM = new VisualizarQuestaoViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            return View(new CadastrarQuestaoViewModel());
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastrarQuestaoViewModel cadastrarVM) 
        {
            var registros = repositorioQuestao.SelecionarRegistros();
            
            var entidade = cadastrarVM.ParaEntidade();

            repositorioQuestao.CadastrarRegistro(entidade);

            contexto.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioQuestao.SelecionarRegistroPorId(id);

            var editarVM = new EditarQuestaoViewModel(
                id,
                registroSelecionado!.Enunciado,
                registroSelecionado.FoiAcertada
            );

            return View(editarVM);
        }


    }
}
