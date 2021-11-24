using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.DTOs
{
    public class AdminTokenDto
    {
        public string Username { get; set; }
    
        public string Name { get; set; }
 
        public string Vorname { get; set; }

        public string Email { get; set; }

        public string Foto { get; set; }

        public string Rolle { get; set; }

        public string Token { get; set; }
    }
}
