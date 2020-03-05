using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Database
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {

        }
        public DbSet<StudentManagement.Models.Payment> Payment { get; set; }
        public DbSet<StudentManagement.Models.Student> Student { get; set; }
        public DbSet<StudentManagement.Models.Registration> Registration { get; set; }
        public DbSet<StudentManagement.Models.Faculty> Faculty { get; set; }
    }
}
