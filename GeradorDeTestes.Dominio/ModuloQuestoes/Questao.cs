using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloQuestoes
{
    public class Questao : EntidadeBase<Questao>
    {
        public string Enunciado { get; set; }
        public List<AlternativaQuestao> Alternativas { get; set; }
        public bool Correta { get; set; }

        public Questao()
        {
            Alternativas = new List<AlternativaQuestao>();
        }

        public Questao(string enunciado) : this()
        {
            Id  = Guid.NewGuid();
            Enunciado = enunciado;
            Correta = false;
        }

        public AlternativaQuestao AdicionarAlternativa(string titulo)
        {
            var alternativa = new AlternativaQuestao(titulo, this);

            Alternativas.Add(alternativa);

            EstaIncorreta();

            return alternativa;
        }

        public AlternativaQuestao AdicionarAlternativa(AlternativaQuestao alternativa)
        {
            Alternativas.Add(alternativa);

            return alternativa;
        }

        public bool RemoverAlternativa(AlternativaQuestao alternativa)
        {
            Alternativas.Remove(alternativa);

            return true;
        }

        public void AlternativaCorreta(AlternativaQuestao alternativa)
        {
            if (Alternativas.Contains(alternativa))
            {
                EstaCorreta();
                alternativa.EstaCorreta();
            }
        }

        public void EstaCorreta()
        {
            Correta = true;
        }

        public void EstaIncorreta()
        {
            Correta = false;
        }

        public override void AtualizarRegistro(Questao registroEditado)
        {
            Enunciado = registroEditado.Enunciado;
            Alternativas = registroEditado.Alternativas;
        }


    }
}
