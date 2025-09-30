using System.ComponentModel.DataAnnotations;

namespace Sprint_4.Models
{
    public class Moto
    {
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; } = string.Empty;
        [Required]
        public string Modelo { get; set; } = string.Empty;
        [Range(1900, 2100)]
        public int Ano { get; set; }
    }
}