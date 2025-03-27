using ControleDePresenca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDePresenca.Controllers
{
    public class EventoController : Controller
    {
        private readonly Context context;

        public EventoController(Context ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var eventos = context.Eventos.Include(e => e.Participantes);
            return View(eventos);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                context.Eventos.Add(evento);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var evento = context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefault(e => e.EventoId == id);

            if (evento == null) return NotFound();

            return View(evento);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            var evento = context.Eventos.Find(id);
            if (evento == null) return NotFound();

            return View(evento);
        }

        [HttpPost]
        public IActionResult Edit(Evento evento)
        {
            if (ModelState.IsValid)
            {
                context.Entry(evento).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            var evento = context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefault(e => e.EventoId == id);

            if (evento == null) return NotFound();

            return View(evento);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var evento = context.Eventos.Find(id);
            if (evento == null) return NotFound();

            context.Eventos.Remove(evento);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
