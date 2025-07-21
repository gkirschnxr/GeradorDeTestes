using GeradorDeTestes.Dominio.Compartilhado;
namespace GeradorDeTestes.Dominio.Disciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public string Nome { get; set; }
        // public List<Materia> Materia { get; set; }

        public Disciplina()
        {
        //    Materia = new List<Materia>();
        }

        public Disciplina(string nome) : this()
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public override void AtualizarRegistro(Disciplina registroEditado)
        {
            Nome = registroEditado.Nome;
        }
    }
}
