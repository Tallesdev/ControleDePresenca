using ControleDePresenca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ControleDePresenca.Controllers
{
    public class EventoController : Controller
    {
        public Context context; 

        public EventoController(Context ctx)
                                                
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Eventos.Include(p => p.Participantes));
        }
    }
}
