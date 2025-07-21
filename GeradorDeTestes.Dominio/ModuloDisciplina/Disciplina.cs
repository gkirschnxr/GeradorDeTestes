using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;

namespace GeradorDeTestes.Dominio.ModuloDisciplina;
public class Disciplina : EntidadeBase<Disciplina>
{
    public string Nome { get; set; } = string.Empty;
    public List<Materia> Materia { get; set; }
    public List<Teste> Teste { get; set; }

    public Disciplina() {
        Materia = new List<Materia>();
        Teste = new List<Teste>();
    }

    public Disciplina(string nome) : this() {
        Id = Guid.NewGuid();
        Nome = nome;
    }

    public override void AtualizarRegistro(Disciplina registroEditado) {
        Nome = registroEditado.Nome;
    }
}