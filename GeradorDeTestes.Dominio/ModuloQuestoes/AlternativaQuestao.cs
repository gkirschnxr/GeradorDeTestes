namespace GeradorDeTestes.Dominio.ModuloQuestoes
{
    public class AlternativaQuestao
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public bool Correta { get; set; }
        public Guid QuestaoId { get; set; }
        public Questao Questao { get; set; }

        public AlternativaQuestao(){}

        public AlternativaQuestao(string texto, Questao questao) : this()
        {
            Id = Guid.NewGuid();
            Texto = texto;
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
