using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public string Rolle { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
