using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.DTOs
{
    public class AdminDto
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public string Rolle { get; set; }
        public string Password { get; set; }
    }
}
