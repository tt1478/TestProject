using Contracts;
using LoggingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.ActionFilters
{
    public class ValidateJobExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateJobExistsAttribute(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (int)context.ActionArguments["id"];
            var job = await _repository.Job.GetJob(id, trackChanges);
            if (job == null) 
            { 
                _logger.LogInfo($"Job with id: {id} doesn't exist in the database."); 
                context.Result = new NotFoundResult(); 
            } 
            else 
            { 
                context.HttpContext.Items.Add("job", job); 
                await next(); 
            }
        }
    }
}
