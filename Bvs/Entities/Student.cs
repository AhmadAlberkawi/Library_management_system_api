using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Email { get; set; }
        public int MatrikelNum { get; set; }
        public int BibNum { get; set; }
        public string Foto { get; set; }
    }
}
