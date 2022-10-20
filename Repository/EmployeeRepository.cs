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
    public class EmployeeRepository:RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repository):base(repository)
        {

        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public async Task<Employee> GetEmployee(int id, bool trackChanges)
        {
            return await FindByCondition(e => e.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees(bool trackChanges, string searchTerm)
        {
            return await FindByCondition(e => e.FullName.ToLower().Contains(searchTerm.Trim().ToLower()), trackChanges)
                    .OrderBy(e => e.FullName)
                    .ToListAsync();
        }
    }
}
