using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;

namespace StudentValidation.Models
{

    //Context class
    public class StudentContext : DbContext
    {
        public StudentContext() : base() { }

        public DbSet<StudentDetails> StudentDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}