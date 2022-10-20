using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EmployeeForUpdateDto
    {
        [Required]
        [MinLength(5)]
        public string FullName { get; set; }
        [Required]
        [MinLength(11), MaxLength(11)]
        public string PhoneNumber { get; set; }
        public int? JobId { get; set; }
    }
}
