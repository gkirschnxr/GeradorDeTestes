using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloQuestoes
{
    public class Questao : EntidadeBase<Questao>
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; }
        public List<AlternativaQuestao> Alternativas { get; set; }
        public bool FoiAcertada { get; set; }

        public Questao()
        {
            Alternativas = new List<AlternativaQuestao>();
        }

        public Questao(string enunciado, bool correta) : this()
        {
            Id  = Guid.NewGuid();
            Enunciado = enunciado;
            FoiAcertada = correta;
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
            FoiAcertada = true;
        }

        public void EstaIncorreta()
        {
            FoiAcertada = false;
        }

        public override void AtualizarRegistro(Questao registroEditado)
        {
            Enunciado = registroEditado.Enunciado;
            Alternativas = registroEditado.Alternativas;
        }


    }
}
