using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IJobRepository Job { get; }
        IEmployeeRepository Employee { get; }

        Task SaveAsync();
    }
}
