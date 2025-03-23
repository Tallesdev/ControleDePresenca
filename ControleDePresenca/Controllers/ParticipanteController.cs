using ControleDePresenca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDePresenca.Controllers
{
    public class ParticipanteController : Controller
    {

        public Context context; //atributo da classe objeto da classe contexto

        public ParticipanteController(Context ctx)//metodo construtor
                                             //manipula o banco de dados
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Participantes.Include(e => e.Eventos));
        }
    }
}
