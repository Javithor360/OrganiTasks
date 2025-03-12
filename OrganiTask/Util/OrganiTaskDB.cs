using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganiTask.Entities;

namespace OrganiTask.Util
{
    class OrganiTaskDB : DbContext
    {
        public OrganiTaskDB() : base("name=organitaskEntities") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
