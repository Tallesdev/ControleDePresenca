namespace ControleDePresenca.Models
{
    public class Participante
    {
        public int ParticipanteID { get; set; }
        public string ParticipanteNome { get; set; }
        public float Matriculas { get; set; }
        public int EventosID { get; set; }
        public Evento Eventos { get; set; }
    }
}
