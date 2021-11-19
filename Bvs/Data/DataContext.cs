using Bvs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Borrow> Borrow { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<NumberOverview> NumberOverview { get; set; }

    }
}
