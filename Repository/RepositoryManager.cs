using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repository;
        private IJobRepository _jobRepository;
        private IEmployeeRepository _employeeRepository;
        public RepositoryManager(RepositoryContext repository)
        {
            _repository = repository;
        }
        public IJobRepository Job 
        {
            get
            {
                if(_jobRepository == null)
                {
                    _jobRepository = new JobRepository(_repository);
                }
                return _jobRepository;
            }
        }

        public IEmployeeRepository Employee 
        {
            get 
            {
                if(_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_repository);
                }
                return _employeeRepository;
            }
        }

        public Task SaveAsync()
        {
            return _repository.SaveChangesAsync();
        }
    }
}
