using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestProject.Models;

namespace Alfatraining.Vertrag.Db
{
    public class EFDbContext : DbContext
    {
        public DbSet<People> People { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
            

        }
        public List<People> RepoGetUserId(int id)
        {
            var sql = @"select *  from People where AnimalId =" + id;
            var result = People.FromSqlRaw(sql).ToList();
            return result;
        }

    }
}
