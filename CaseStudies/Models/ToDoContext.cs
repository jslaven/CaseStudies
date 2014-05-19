using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaseStudies.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("DefaultConnection")
        {

        }

        public DbSet<ToDoTask> Tasks { get; set; }
    }
}