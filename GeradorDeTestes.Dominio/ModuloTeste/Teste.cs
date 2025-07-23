using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestoes;

namespace GeradorDeTestes.Dominio.ModuloTeste;
public class Teste : EntidadeBase<Teste>
{
    public string Titulo { get; set; } = string.Empty;
    public Disciplina? Disciplina { get; set; }
    public Materia? Materia { get; set; }
    public Serie Serie { get; set; }
    public int QuantidadeQuestoes { get; set; }
    public bool TipoTeste { get; set; }
    public List<Questao> Questoes { get; set; } = new List<Questao>();

    public Teste() { }

    public Teste(string titulo, Disciplina disciplina, Materia? materia, Serie serie, int quantidadeQuestoes, bool TipoTeste) {
        Titulo = titulo;
        Disciplina = disciplina;
        Materia = materia;
        Serie = serie;
        QuantidadeQuestoes = quantidadeQuestoes;
        TipoTeste = TipoTeste;
    }

    public override void AtualizarRegistro(Teste registroEditado) {
        Titulo = registroEditado.Titulo;
        Disciplina = registroEditado.Disciplina;
        Materia = registroEditado.Materia;
        Serie = registroEditado.Serie;
        QuantidadeQuestoes = registroEditado.QuantidadeQuestoes;
        TipoTeste = registroEditado.TipoTeste;
    }
}
