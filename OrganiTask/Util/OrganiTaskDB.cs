using System.Data.Entity;

using OrganiTask.Entities;
using Task = OrganiTask.Entities.Task; // para resolver la ambigüedad con System.Threading.Tasks.Task

namespace OrganiTask.Util
{
    class OrganiTaskDB : DbContext
    {
        public OrganiTaskDB() : base("name=organitaskEntities") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
    }
}
