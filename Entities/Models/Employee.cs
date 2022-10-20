using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int? JobId { get; set; }
        public Job Job { get; set; }
    }
}
