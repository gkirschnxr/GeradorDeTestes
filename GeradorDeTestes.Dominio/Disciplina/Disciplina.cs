using GeradorDeTestes.Dominio.Compartilhado;
namespace GeradorDeTestes.Dominio.Disciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public string Nome { get; set; }
        public List<Despesa> Despesas { get; set; }

        public Disciplina()
        {
            Despesas = new List<Despesa>();
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
