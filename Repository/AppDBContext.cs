using Data;
using Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            //var ConnectionString = @"Data Source=.;Initial Catalog=CTA;user id=sa;password=abc_123;TrustServerCertificate=True;MultipleActiveResultSets=true";
            //optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            new ComplaintMap(modelBuilder.Entity<Complaint>());
            new DemandMap (modelBuilder.Entity<Demand>());
            new StatusMap(modelBuilder.Entity<Status>());
            new UserTypeMap(modelBuilder.Entity<UserType>());



            modelBuilder.Entity<Status>()
             .HasData(
                 new Status
                 {
                     ID = 1,
                     IsDeleted = false,
                     Name = "Pending",
                 },
                 new Status
                 {
                     ID = 2,
                     IsDeleted = false,
                     Name = "Approved",
                 },
                 new Status
                 {
                     ID = 3,
                     IsDeleted = false,
                     Name = "Rejected",
                 }
             );

           

        }
    }
}
