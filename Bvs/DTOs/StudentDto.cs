using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bvs_API.DTOs
{
    public class StudentDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Vorname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int MatrikelNum { get; set; }
        [Required]
        public int BibNum { get; set; }
        [Required]
        public string Foto { get; set; }
    }
}
