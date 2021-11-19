using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs.Entities
{
    [Keyless]
    public class Borrow
    {
        public Student students { get; set; }
        public Book Books { get; set; }
    }
}
