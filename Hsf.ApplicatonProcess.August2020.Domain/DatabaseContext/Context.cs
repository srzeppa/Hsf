using Hsf.ApplicatonProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hsf.ApplicatonProcess.August2020.Domain.DatabaseContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
