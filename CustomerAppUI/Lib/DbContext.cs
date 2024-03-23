using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentCourseManagement.Models
{
    public class DbContext : IdentityDbContext<IdentityUser>
    {
        public DbContext() : base("AppCS")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
    }
}