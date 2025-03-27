using ControleDePresenca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Create()
        {
            ViewBag.ParticipanteId = new SelectList(context.Participantes.OrderBy(p => p.ParticipanteNome), "ParticipanteID", "Nome");// viewbag pra gerar lista de participantes
            return View();
        }
        [HttpPost]
        public IActionResult Create(Evento evento)
        {
            context.Add(evento);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
