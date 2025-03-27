using ControleDePresenca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ControleDePresenca.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly Context context;

        public ParticipanteController(Context ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var participantes = context.Participantes.Include(p => p.Eventos);
            return View(participantes);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            // Exibir os eventos disponíveis para o participante escolher ao se cadastrar
            ViewBag.EventosID = new SelectList(context.Eventos.OrderBy(e => e.EventoNome), "EventoId", "EventoNome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Participante participante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Participantes.Add(participante);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao salvar o participante: " + ex.ToString());
                }
            }
            return View(participante);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var participante = context.Participantes
                .Include(p => p.Eventos)
                .FirstOrDefault(p => p.ParticipanteID == id);

            if (participante == null) return NotFound();

            return View(participante);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            var participante = context.Participantes.Find(id);
            if (participante == null) return NotFound();

            ViewBag.EventosID = new SelectList(context.Eventos.OrderBy(e => e.EventoNome), "EventoId", "EventoNome", participante.EventosID);
            return View(participante);
        }

        [HttpPost]
        public IActionResult Edit(Participante participante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Entry(participante).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao editar o participante: " + ex.ToString());
                }
            }
            return View(participante);
        }


        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            var participante = context.Participantes
                .Include(p => p.Eventos)
                .FirstOrDefault(p => p.ParticipanteID == id);

            if (participante == null) return NotFound();

            return View(participante);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var participante = context.Participantes.Find(id);
            if (participante == null) return NotFound();

            context.Participantes.Remove(participante);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
