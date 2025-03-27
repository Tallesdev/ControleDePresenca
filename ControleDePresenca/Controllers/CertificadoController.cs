using ControleDePresenca.Areas.Identity.Data;
using ControleDePresenca.Data;
using ControleDePresenca.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.IO;
using System.Threading.Tasks;

public class CertificadoController : Controller
{
    private readonly Context _context;
    private readonly AuthDbContext _authContext;
    private readonly UserManager<ControleDePresencaUser> _userManager;

    public CertificadoController(Context context, AuthDbContext authContext, UserManager<ControleDePresencaUser> userManager)
    {
        _context = context;
        _authContext = authContext;
        _userManager = userManager;
    }

    public async Task<IActionResult> GerarCertificado(int participanteId, int eventoId)
    {
        {

            if (participanteId == 0 || eventoId == 0)
            {
                return BadRequest("Participante ou Evento não foram fornecidos.");
            }
            else { 


            // Buscar participante e evento usando o DbContext do BancoPresenca
            var participante = await _context.Participantes
                .FirstOrDefaultAsync(p => p.ParticipanteID == participanteId);

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(e => e.EventoId == eventoId);

            if (participante == null || evento == null)
            {
                return NotFound("Participante ou Evento não encontrados.");
            }

            // Gerar certificado com os dados do banco
            string certificadoId = Guid.NewGuid().ToString();

            // Caminho para salvar o arquivo no servidor
            string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "certificados", certificadoId + ".pdf");

            // Gerar o PDF
            byte[] pdfBytes = GerarPDF(participanteId, eventoId);

            // Salvar o PDF no caminho especificado
            System.IO.File.WriteAllBytes(caminhoArquivo, pdfBytes);

            // Retorna o caminhoo para a URL
            string urlCertificado = "/certificados/" + certificadoId + ".pdf";

            // Criarr o modelo do certificado
            var modeloCertificado = new Certificado
            {
                NomeParticipante = participante.ParticipanteNome,
                NomeEvento = evento.EventoNome,
                Duracao = evento.Duracao,
                URLCertificado = urlCertificado,                
            };
            Console.WriteLine($"Participante: {participante?.ParticipanteNome}");
            Console.WriteLine($"Evento: {evento?.EventoNome}");

                return View(modeloCertificado);
            }
        }
    }

    public byte[] GerarPDF(int participanteId, int eventoId)
    {
        // Buscar o participante e evento a partir dos IDs fornecidos
        var participante = _context.Participantes
            .FirstOrDefault(p => p.ParticipanteID == participanteId);

        var evento = _context.Eventos
            .FirstOrDefault(e => e.EventoId == eventoId);

        if (participante == null || evento == null)
        {
            throw new Exception("Participante ou Evento não encontrados.");
        }

        using (var memoryStream = new MemoryStream())
        {
            // Criar o escritor do PDF
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            // Adicionar conteúdo no documento
            document.Add(new Paragraph($"Certificado de Participação - {evento.EventoNome}"));
            document.Add(new Paragraph($"Participante: {participante.ParticipanteNome}"));
            document.Add(new Paragraph($"Evento: {evento.EventoNome}"));
            document.Add(new Paragraph($"Duração: {evento.Duracao}"));

            // Fechar o documento
            document.Close();

            // Retorna o conteúdo do PDF como um array de bytes
            return memoryStream.ToArray();
        }
    }
}
