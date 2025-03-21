using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using ControleDePresenca.Models;


namespace ControleDePresenca.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
              : base(options)
        {

        }
        public DbSet<Evento> Eventos { get; set; }//dbset é tabela
        public DbSet<Participante> Participantes { get; set; }//tabela
    }
}
