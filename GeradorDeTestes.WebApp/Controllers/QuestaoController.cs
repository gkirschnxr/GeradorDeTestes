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
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<QuestaoController> logger;

        public QuestaoController(
        IRepositorioQuestao repositorioQuestao,
        IRepositorioMateria repositorioMateria,
        IUnitOfWork unitOfWork,
        ILogger<QuestaoController> logger
    )
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioMateria = repositorioMateria;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
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
            var materias = repositorioMateria.SelecionarRegistros();

            var cadastrarVM = new CadastrarQuestaoViewModel(materias);

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarQuestaoViewModel cadastrarVM) 
        {
            var registros = repositorioQuestao.SelecionarRegistros();

            var materias = repositorioMateria.SelecionarRegistros();

            
            if (registros.Any(i => i.Enunciado.Equals(cadastrarVM.Enunciado)))
            {
                ModelState.AddModelError(
                    "CadastroUnico",
                    "Já existe uma questão registrada com este enunciado."
                );

                cadastrarVM.MateriasDisponiveis = materias
                    .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
                    .ToList();

                return View(cadastrarVM);
            }

            try
            {
                var entidade = CadastrarQuestaoViewModel.ParaEntidade(cadastrarVM, materias);

                repositorioQuestao.CadastrarRegistro(entidade);

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();

                logger.LogError(
                    ex,
                    "Ocorreu um erro durante o registro de {@ViewModel}.",
                    cadastrarVM
                );
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("cadastrar/adicionar-alternativa")]
        public IActionResult AdicionarAlternativa(
            CadastrarQuestaoViewModel cadastrarVM, 
            AdicionarAlternativaQuestaoViewModel alternativaVM)
        {
            cadastrarVM.MateriasDisponiveis = repositorioMateria
                .SelecionarRegistros()
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
            var registro = repositorioQuestao.SelecionarRegistroPorId(id);

            if (registro is null)
                return RedirectToAction(nameof(Index));

            var materias = repositorioMateria.SelecionarRegistros();

            var editarVm = new EditarQuestaoViewModel(
                registro.Id,
                registro.Enunciado,
                registro.Materias.Id,
                registro.Alternativas,
                materias
            );

            return View(editarVm);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarQuestaoViewModel editarVm)
        {
            var registros = repositorioQuestao.SelecionarRegistros();

            var materias = repositorioMateria.SelecionarRegistros();

            if (registros.Any(i => !i.Id.Equals(id) && i.Enunciado.Equals(editarVm.Enunciado)))
            {
                ModelState.AddModelError(
                    "CadastroUnico",
                    "Já existe uma questão registrada com este enunciado."
                );

                editarVm.MateriasDisponiveis = materias
                    .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
                    .ToList();

                return View(editarVm);
            }

            try
            {
                var entidadeEditada = EditarQuestaoViewModel.ParaEntidade(editarVm, materias);

                repositorioQuestao.EditarRegistro(id, entidadeEditada);

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();

                logger.LogError(
                    ex,
                    "Ocorreu um erro durante o registro de {@ViewModel}.",
                    editarVm
                );
            }

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
