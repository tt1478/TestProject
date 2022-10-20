using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Job
            CreateMap<Job, JobDto>();
            CreateMap<JobForCreationDto, Job>();
            CreateMap<JobForUpdateDto, Job>();
            #endregion
            #region Employee
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            #endregion
        }

    }
}
