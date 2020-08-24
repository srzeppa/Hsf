using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hsf.ApplicatonProcess.August2020.Domain.DatabaseContext;
using Hsf.ApplicatonProcess.August2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hsf.ApplicatonProcess.August2020.Domain.Services
{
    public interface IApplicantService
    {
        Task Save(Applicant applicant);
        List<Applicant> Get();
        Task Delete(int id);
        Applicant GetById(int id);
        Task Edit(Applicant applicant);
    }

    public class ApplicantService : IApplicantService
    {
        private readonly Context _database;

        public ApplicantService(Context database)
        {
            _database = database;
        }

        public List<Applicant> Get()
        {
            return _database.Applicants.ToList();
        }

        public async Task Save(Applicant applicant)
        {
            await _database.AddAsync<Applicant>(applicant);
            await _database.SaveChangesAsync();
        }

        public async Task Update(Applicant applicant)
        {
            _database.Update<Applicant>(applicant);
            await _database.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var applicant = new Applicant
            {
                Id = id
            };
            _database.Applicants.Remove(applicant);
            await _database.SaveChangesAsync();
        }

        public Applicant GetById(int id)
        {
            return _database.Applicants.First(x => x.Id == id);
        }

        public async Task Edit(Applicant applicant)
        {
            _database.Entry(applicant).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }
    }
}
