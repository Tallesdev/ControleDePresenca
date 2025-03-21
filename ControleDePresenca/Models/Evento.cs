﻿namespace ControleDePresenca.Models
{
    public class Evento
    {
        public int EventoId { get; set; }
        public int EventoNome { get; set; }

        public int Duracao { get; set; }

        public ICollection <Participante> Participantes { get; set; }
    }
}
