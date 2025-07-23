using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GeradorDeTestes.WebApp.Controllers
{
    [Route("questoes")]
    public class QuestaoController : Controller
    {
        private readonly GeradorDeTestesDbContext contexto;
        private readonly IRepositorioQuestao repositorioQuestao;
        private readonly IRepositorioMateria repositorioMateria;

        public QuestaoController(
            GeradorDeTestesDbContext contexto, 
            IRepositorioQuestao repositorioQuestao, 
            IRepositorioMateria repositorioMateria)
        {
            this.contexto = contexto;
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioMateria = repositorioMateria;
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
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarQuestaoViewModel cadastrarVM) 
        {
            var registros = repositorioQuestao.SelecionarRegistros();
            var materias = repositorioMateria.SelecionarRegistros();

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                var entidade = cadastrarVM.ParaEntidade();

                repositorioQuestao.CadastrarRegistro(entidade);

                transacao.Commit();
            }
            catch (Exception) 
            {
                    transacao.Rollback();

                throw;
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("cadastrar/adicionar-alternativa")]
        public IActionResult AdicionarAlternativa(CadastrarQuestaoViewModel cadastrarVM, AdicionarAlternativaQuestaoViewModel alternativaVM)
        {
            cadastrarVM.MateriasDisponiveis = repositorioMateria.SelecionarRegistros()
                .Select(m => new SelectListItem(m.Nome, m.Id.ToString()))
                .ToList();

            cadastrarVM.AdicionarAlternativa(alternativaVM);

            return View(nameof(Cadastrar), cadastrarVM);
        }

        [HttpPost("cadastrar/remover-alternativa/{letra:alpha}")]
        public IActionResult RemoverAlternativa(char letra, CadastrarQuestaoViewModel cadastrarVM)
        {
            var alternativa = cadastrarVM.AlternativasSelecionadas
                .Find(a => a.Letra.Equals(letra));

            if(alternativa is not null)
                cadastrarVM.RemoverAlternativa(alternativa);

            cadastrarVM.MateriasDisponiveis = repositorioMateria
                .SelecionarRegistros()
                .Select(m => new SelectListItem(m.Nome, m.Id.ToString()))
                .ToList();

            return View(nameof(Cadastrar), cadastrarVM);
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioQuestao.SelecionarRegistroPorId(id);

            var editarVM = new EditarQuestaoViewModel(
                id,
                registroSelecionado!.Enunciado,
                registroSelecionado.FoiAcertada,
                registroSelecionado.Alternativas!,
                registroSelecionado.Materias!
            );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarQuestaoViewModel editarVM)
        {
            var registros = repositorioQuestao.SelecionarRegistros();

            var entidadeEditada = editarVM.ParaEntidade();

            repositorioQuestao.EditarRegistro(id, entidadeEditada);

            contexto.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioQuestao.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirQuestaoViewModel(registroSelecionado!.Id, registroSelecionado.Enunciado);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioQuestao.ExcluirRegistro(id);

            contexto.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




    }
}
