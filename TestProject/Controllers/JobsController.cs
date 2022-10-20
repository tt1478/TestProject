using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.ActionFilters;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public JobsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {

            var jobs = await _repository.Job.GetJobs(false);
            var jobsDto = _mapper.Map<IEnumerable<JobDto>>(jobs);
            return Ok(jobsDto);
        }
        [HttpGet("{id}", Name = "JobById")]
        public async Task<IActionResult> GetJob(int id)
        {

            var job = await _repository.Job.GetJob(id, false);
            if (job == null)
            {
                _logger.LogInfo($"Job with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var jobDto = _mapper.Map<JobDto>(job);
                return Ok(jobDto);
            }
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateJob([FromBody] JobForCreationDto job)
        {
            var jobEntity = _mapper.Map<Job>(job);
            _repository.Job.CreateJob(jobEntity);
            await _repository.SaveAsync();
            var jobToReturn = _mapper.Map<JobDto>(jobEntity);
            return CreatedAtRoute("JobById", new { id = jobToReturn.Id }, jobToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateJobExistsAttribute))]
        public async Task<IActionResult> UpdateJob(int id, [FromBody]JobForUpdateDto job)
        {
            var jobEntity = HttpContext.Items["job"] as Job;
            _mapper.Map(job, jobEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = HttpContext.Items["job"] as Job;
            _repository.Job.DeleteJob(job);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
