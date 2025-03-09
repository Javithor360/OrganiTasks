using OrganiTask.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util
{
    class OrganiTaskDB : DbContext
    {
        public OrganiTaskDB() : base("name=organitaskEntities") { }
        public DbSet<Usuario> Users { get; set; }
        public DbSet<Tablero> Dashboards { get; set; }
    }
}
