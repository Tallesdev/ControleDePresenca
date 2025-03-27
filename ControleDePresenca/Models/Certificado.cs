namespace ControleDePresenca.Models
{
    public class Certificado
    {
            public int Id { get; set; }
            public string NomeParticipante { get; set; }
            public string NomeEvento { get; set; }
            public DateTime DataEvento { get; set; }
            public string URLCertificado { get; set; } // Caminho do certificado gerado
        }
}

