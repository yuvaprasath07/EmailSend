using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class EntityDbcontext : DbContext
    {
        public EntityDbcontext(DbContextOptions<EntityDbcontext> options) : base(options) { }
        public DbSet<EntityModel> EmployeeEmail { get; set; }
    }
}
