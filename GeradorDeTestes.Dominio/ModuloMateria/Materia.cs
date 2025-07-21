using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloMateria;
public class Materia : EntidadeBase<Materia>
{
    public string Nome { get; set; } = string.Empty;
    public TipoDisciplina Disciplina { get; set; }
    public Serie Serie { get; set; }

    public Materia() { }

    public Materia(string nome, TipoDisciplina disciplina, Serie serie) {
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