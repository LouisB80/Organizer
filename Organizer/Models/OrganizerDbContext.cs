using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class OrganizerDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}