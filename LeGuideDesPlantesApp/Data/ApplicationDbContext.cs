using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeGuideDesPlantesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            _ = builder.Entity<ApplicationUser>(b =>
            {

            });

            _ = builder.Entity<ApplicationRole>(b =>
            {

            });


        }


        public DbSet<Models.HuilesEssentiel>? HuilesEssentiel { get; set; }
        public DbSet<Models.PlantesAromatiques>? PlantesAromatiques { get; set; }
        public DbSet<Models.PlantesSauvages>? PlantesSauvages { get; set; }
        public DbSet<Models.Pays>? Pays { get; set; }
        public DbSet<Models.Arbres>? Arbres { get; set; }
        public DbSet<Models.Categorie>? Categories { get; set; }


    }

}