using Microsoft.EntityFrameworkCore;

namespace ControleDePresenca.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            //associa os dados ao contexto
            Context context = app.ApplicationServices.GetRequiredService<Context>();
            //inserir os dados nas entidades do contexto
            context.Database.Migrate();
            //se o contexto estiver vazio
            if (!context.Participantes.Any())
            //inserir os produtos iniciais
            {
                context.Participantes.AddRange(
                    new Participante { ParticipanteNome = "Camiseta Oficial", Matriculas = 11, EventosID = 1 },
                    new Participante { ParticipanteNome = "Short", Matriculas = 120, EventosID = 1 },
                    new Participante { ParticipanteNome = "Tênis", Matriculas = 540, EventosID = 2 });

                context.Eventos.AddRange(
                    new Evento { EventoNome = "Hackaton", Duracao = 3 },
                    new Evento { EventoNome = "Palestra IA", Duracao = 4});

                context.SaveChanges();
            }
        }
    }
}
