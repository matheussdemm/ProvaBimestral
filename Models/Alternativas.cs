namespace ProvaBimestral.Models
{
    public class Alternativas
    {
        public int Id { get; set; }
        public int Id_Pergunta { get; set; }

        public bool Correta { get; set; } 
        public string AlternativaTexto { get; set; } 

        public virtual Perguntas Pergunta { get; set; }
    }
}
