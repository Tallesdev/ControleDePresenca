using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
namespace ControleDePresenca.Models
{
    public class Evento
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "O nome do evento é obrigatório.")]
        public string ?EventoNome { get; set; }

        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A duração deve ser maior que 0.")]
        public int Duracao { get; set; } // Ou double

        public ICollection<Participante> ?Participantes { get; set; }
    }
}
