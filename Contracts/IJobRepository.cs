using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetJobs(bool trackChanges);
        Task<Job> GetJob(int id, bool trackChanges);
        void CreateJob(Job job);
        void DeleteJob(Job job);
    }
}
