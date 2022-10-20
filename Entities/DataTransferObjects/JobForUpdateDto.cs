using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class JobForUpdateDto
    {
        [Required]
        [MinLength(5)]
        public string Description { get; set; }
        public IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }
}
