using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            //Validation//
            builder
                .HasMany(x => x.Employees)
                .WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId);
            builder.Property(x => x.Description).IsRequired();
            //Seed-Data//
            builder.HasData(
                new Job
                {
                    Id = 1,
                    Description = "Project Manager"
                },
                new Job 
                {
                    Id = 2,
                    Description = "Program Manager"
                },
                new Job 
                {
                    Id = 3,
                    Description = "Data Analyst"
                },
                new Job 
                {
                    Id = 4,
                    Description = "Inspector"
                },
                new Job 
                {
                    Id = 5,
                    Description = "Operations Manager"
                },
                new Job 
                {
                    Id = 6,
                    Description = "Civil Engineering Intern"
                },
                new Job 
                {
                    Id = 7,
                    Description = "Assistant Process Engineer"
                },
                new Job 
                {
                    Id = 8,
                    Description = "ACCOUNTANT"
                });
        }
    }
}
