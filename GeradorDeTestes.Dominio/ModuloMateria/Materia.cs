using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloMateria;
public class Materia : EntidadeBase<Materia>
{
    public string Nome { get; set; } = string.Empty;
    public TipoDisciplina Disciplina { get; set; }
    public TipoSerie Serie { get; set; }

    public Materia() { }

    public Materia(string nome, TipoDisciplina disciplina, TipoSerie serie) {
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