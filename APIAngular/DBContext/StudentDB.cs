using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APIAngular.DBContext
{
    public class StudentDB : DbContext
    {
        public StudentDB():base("name=Student")
        { 
        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<User> Users { get; set; }
    }
}