﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs.Entities
{
    public class Borrow
    {
        public int Id { get; set; }
        public Student students { get; set; }
        public Book Books { get; set; }
    }
}
