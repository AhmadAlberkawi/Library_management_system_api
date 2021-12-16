using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.DTOs
{
    public class BookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public string Verlag { get; set; }
        [Required]
        public int Verfuegbar { get; set; }
        public int Anzahl { get; set; }
        //  public string B_foto { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Kategorie { get; set; }
    }
}
