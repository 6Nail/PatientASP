using Microsoft.EntityFrameworkCore;
using PatientAndDoctors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientAndDoctors.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<VisitHistory> VisitHistories { get; set; }
    }
}
