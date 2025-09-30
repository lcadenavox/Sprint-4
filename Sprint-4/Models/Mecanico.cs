using System.ComponentModel.DataAnnotations;

namespace Sprint_4.Models
{
    public class Mecanico
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Especialidade { get; set; } = string.Empty;
    }
}