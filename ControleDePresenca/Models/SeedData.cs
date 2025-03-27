using ControleDePresenca.Areas.Identity.Data;
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

        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ControleDePresencaUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Criando roles (funções) sem a role "Organizador"
            string[] roleNames = { "Administrador", "Participante" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Agora podemos adicionar um usuário padrão com a role 'Administrador'
            var defaultUser = await userManager.FindByEmailAsync("admin@admin.com");

            if (defaultUser == null)
            {
                var user = new ControleDePresencaUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Admin",  // Preenchendo FirstName
                    LastName = "User"     // Preenchendo LastName
                };

                var result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrador");
                }
            }
        }
    }
}
