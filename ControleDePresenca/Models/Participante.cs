using System.ComponentModel.DataAnnotations;

namespace ControleDePresenca.Models
{
    public class Participante
    {
        public int ParticipanteID { get; set; }

        [Required(ErrorMessage = "O nome do participante é obrigatório.")]
        public string ?ParticipanteNome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        public double Matriculas { get; set; }

        [Required(ErrorMessage = "O evento é obrigatório.")]
        public int EventosID { get; set; }

        public Evento ?Eventos { get; set; }
    }
}