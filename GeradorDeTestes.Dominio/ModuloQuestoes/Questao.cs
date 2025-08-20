﻿using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;

namespace GeradorDeTestes.Dominio.ModuloQuestoes
{
    public class Questao : EntidadeBase<Questao>
    {
        public string Enunciado { get; set; } = string.Empty;
        public List<Alternativa>? Alternativas { get; set; }
        public Materia Materias { get; set; } 
        public bool UtilizadaEmTeste { get; set; }
        public List<Teste> Testes { get; set; }
        public Alternativa? AlternativaCorreta => Alternativas?.Find(a => a.Correta);

        public Questao()
        {
            Alternativas = new List<Alternativa>();
            Testes = new List<Teste>();
        }

        public Questao(string enunciado, Materia materia) : this()
        {
            Id  = Guid.NewGuid();
            Enunciado = enunciado;
            Materias = materia;
            UtilizadaEmTeste = false ;
        }

        public Alternativa AdicionarAlternativa(string resposta, bool correta)
        {
            int qtdAlternativas = Alternativas.Count;

            char letra = (char)('a' + qtdAlternativas);

            var alternativa = new Alternativa(letra, resposta, correta, this);

            Alternativas.Add(alternativa);

            return alternativa;
        }

        public void RemoverAlternativa(char letra)
        {
            if (!Alternativas.Any(a => a.Letra.Equals(letra)))
                return;

            var alternativa = Alternativas.Find(a => a.Letra.Equals(letra));

            if (alternativa is null)
                return;

            Alternativas.Remove(alternativa);

            ReatribuirLetras();
        }

        private void ReatribuirLetras()
        {
            for (int i = 0; i < Alternativas.Count; i++)
            {
                Alternativas[i].Letra = (char)('a' + i);
            }
        }

        public override void AtualizarRegistro(Questao registroEditado)
        {
            Enunciado = registroEditado.Enunciado;
            UtilizadaEmTeste = registroEditado.UtilizadaEmTeste;
        }


    }
}
