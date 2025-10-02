using System.ComponentModel.DataAnnotations;

namespace Sprint_4.Models
{
    public class Oficina
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Endereco { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        public List<string> Especialidades { get; set; } = new();
    }
}