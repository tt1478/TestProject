using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JobRepository:RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(RepositoryContext repository):base(repository)
        {

        }

        public void CreateJob(Job job)
        {
            Create(job);
        }

        public void DeleteJob(Job job)
        {
            Delete(job);
        }

        public async Task<Job> GetJob(int id, bool trackChanges)
        {
            return await FindByCondition(j => j.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Job>> GetJobs(bool trackChanges)
        {
            return await FindAll(trackChanges)
                    .OrderBy(j => j.Description)
                    .ToListAsync();
        }
    }
}
