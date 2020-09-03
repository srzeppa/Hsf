using System.Collections.Generic;
using System.Linq;
using Hsf.ApplicatonProcess.August2020.Domain.Models;

namespace Hsf.ApplicatonProcess.August2020.Domain.Storage
{
    public static class ApplicantStorage
    {
        public static List<Applicant> Applicants = new List<Applicant>();

        public static void Add(Applicant applicant)
        {
            Applicants.Add(applicant);
        }

        public static void Remove(Applicant applicant)
        {
            Applicants.RemoveAll(x => x.Id == applicant.Id);
        }

        public static Applicant Get(int id)
        {
            return Applicants.First(x => x.Id == id);
        }
    }
}
