using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.DTOs
{
    public class BorrowDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
