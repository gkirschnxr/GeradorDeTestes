using GeradorDeTestes.Dominio.Disciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Dominio.ModuloTeste;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("testes")]
public class TesteController : Controller
{
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
        return View();
    }
}
