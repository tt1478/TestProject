using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Validation//
            builder.HasOne(x => x.Job)
                    .WithMany(x => x.Employees)
                    .HasForeignKey(x => x.JobId);
            builder.Property(x => x.FullName).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            //Seed-Data//
            builder.HasData(
                new Employee
                {
                    Id = 1,
                    FullName = "Mark Keeling",
                    PhoneNumber = "965522022",
                    JobId = 1
                },
                new Employee
                {
                    Id = 2,
                    FullName = "Destiny Hays",
                    PhoneNumber = "685255620",
                    JobId = 2
                },
                new Employee
                {
                    Id = 3,
                    FullName = "Lorcan Harrington",
                    PhoneNumber = "454751215",
                    JobId = 3
                },
                new Employee
                {
                    Id = 4,
                    FullName = "Alec Townsend",
                    PhoneNumber = "582256325",
                    JobId = 4
                },
                new Employee
                {
                    Id = 5,
                    FullName = "Jethro Cortes",
                    PhoneNumber = "215525555",
                    JobId = 5
                },
                new Employee
                {
                    Id = 6,
                    FullName = "Dru Black",
                    PhoneNumber = "126582825",
                    JobId = 6
                },
                new Employee
                {
                    Id = 7,
                    FullName = "Claudia Barker",
                    PhoneNumber = "784446225",
                    JobId = 7
                },
                new Employee
                {
                    Id = 8,
                    FullName = "Kunal Farrell",
                    PhoneNumber = "576652555",
                    JobId = 8
                },
                new Employee
                {
                    Id = 9,
                    FullName = "Shaun Burton",
                    PhoneNumber = "787522552",
                    JobId = 1
                });
        }
    }
}
