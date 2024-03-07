using EscuelaNegociosJames.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EscuelaNegociosJames.DbContext
{
    public class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public Context(DbContextOptions<Context> options) :
            base(options)
        {

        }
    }
}
