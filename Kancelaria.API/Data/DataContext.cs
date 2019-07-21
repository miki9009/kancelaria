using Microsoft.EntityFrameworkCore;
using Kancelaria.API.Models;

namespace Kancelaria.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Value> Values {get;set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases {get;set;}

        public DbSet<CaseDetail> CasesDetails {get;set;}
    }
}