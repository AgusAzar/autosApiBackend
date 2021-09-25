using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutosApi.Models
{
    public class Marca{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarcaId { get; set; }
        public string Name { get; set; }

        public List<Auto> Autos { get; } = new List<Auto>();
    }
}
