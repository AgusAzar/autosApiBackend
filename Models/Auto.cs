using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutosApi.Models
{
    public class Auto{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }
        [Required]
        public string Modelo { get; set; }
        public string FotoUrl { get; set; }

        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca MarcaActual { get; set; }
    }
}
