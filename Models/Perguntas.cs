namespace ProvaBimestral.Models
{
    public class Perguntas
    {
        public int Id { get; set; } 
        public string Pergunta { get; set; }

    
        public virtual ICollection<Alternativas> Alternativas { get; set; }
    }
}
