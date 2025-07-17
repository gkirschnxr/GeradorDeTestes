namespace GeradorDeTestes.Dominio.ModuloQuestoes
{
    public class AlternativaQuestao
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public bool Correta { get; set; }
        public Questao Questao { get; set; }

        public AlternativaQuestao(){}

        public AlternativaQuestao(string titulo, Questao questao) : this()
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Correta = false;
            Questao = questao;
        }

        public void EstaCorreta()
        {
            Correta = true;
        }

        public void EstaIncorreta()
        {
            Correta = false;
        }


    }
}
