using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloQuestoes;
using GeradorDeTestes.Dominio.ModuloTeste;

namespace GeradorDeTestes.Dominio.ModuloMateria;
public class Materia : EntidadeBase<Materia>
{
    public string Nome { get; set; } = string.Empty;
    public Disciplina? Disciplina { get; set; }
    public Serie Serie { get; set; }
    public List<Questao>? Questoes { get; set; }
    public List<Teste>? Testes { get; set; }

    public Materia() { }

    public Materia(string nome, Disciplina disciplina, Serie serie) {
        Id = Guid.NewGuid();
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }

    public override void AtualizarRegistro(Materia registroEditado) {
        Nome = registroEditado.Nome;
        Disciplina = registroEditado.Disciplina;
        Serie = registroEditado.Serie;
    }
}