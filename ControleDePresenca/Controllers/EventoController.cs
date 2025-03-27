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
            ViewBag.ParticipanteId = new SelectList(context.Participantes.OrderBy(p => p.ParticipanteNome), "ParticipanteID", "ParticipanteNome");// viewbag pra gerar lista de participantes
            return View();
        }
        [HttpPost]
        public IActionResult Create(Evento evento)
        {
            context.Add(evento);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var Evento = context.Eventos    
                .Include(p => p.Participantes)
                .FirstOrDefault(e => e.EventoId == id);
            return View(Evento);
        }
        public IActionResult Edit(int id)
        {
            var Evento = context.Eventos.Find(id);
            ViewBag.ParticipanteID = new SelectList(context.Participantes.OrderBy(p => p.ParticipanteNome), "ParticipanteID", "ParticipanteNome");
            return View(Evento);
        }

        [HttpPost]
        public IActionResult Edit(Evento evento)
        {
            context.Entry(evento).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var evento = context.Eventos
                .Include(p => p.Participantes)
                .FirstOrDefault(e => e.EventoId == id);
            return View(evento);
        }

        [HttpPost]
        public IActionResult Delete(Evento evento)
        {
            context.Eventos.Remove(evento);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
