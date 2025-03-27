using ControleDePresenca.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDePresenca.Models
{
    public class SeedData
    {
       
           public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<Context>();

            context.Database.EnsureCreated();  // Aplica migrações sem recriar tabelas já existentes

            if (!context.Eventos.Any()) // Evita inserção duplicada
            {
                context.Eventos.AddRange(
                    new Evento { EventoNome = "Hackaton", Duracao = 3 },
                    new Evento { EventoNome = "Palestra IA", Duracao = 4 }
                );
            }

            if (!context.Participantes.Any()) // Evita inserção duplicada
            {
                context.Participantes.AddRange(
                    new Participante { ParticipanteNome = "Maria Eduarda", Matriculas = 11, EventosID = 1 },
                    new Participante { ParticipanteNome = "Short", Matriculas = 120, EventosID = 1 },
                    new Participante { ParticipanteNome = "Tênis", Matriculas = 540, EventosID = 2 }
                );
            }

            context.SaveChanges();
        }

    }
}

