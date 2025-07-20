using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloTarefa;
public class Tarefa : EntidadeBase<Tarefa>
{
    public string Nome { get; set; } = string.Empty;
    public Disciplina Disciplina { get; set; }
    public Serie Serie { get; set; }

    public Tarefa() { }

    public Tarefa(string nome, Disciplina disciplina, Serie serie) {
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }

    public override void AtualizarRegistro(Tarefa registroEditado) {
        Nome = registroEditado.Nome;
        Disciplina = registroEditado.Disciplina;
        Serie = registroEditado.Serie;
    }
}
