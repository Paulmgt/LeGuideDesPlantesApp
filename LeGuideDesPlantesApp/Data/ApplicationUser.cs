using Microsoft.AspNetCore.Identity;

namespace LeGuideDesPlantesApp.Data
{
    public class ApplicationUser : IdentityUser
    {

        public int Id { get; set; }

        public string? Nom { get; set; }

        public string? Adresse { get; set; }

        public string? Ville { get; set; }

        public string? Pays { get; set; }

        public ICollection<ApplicationRole>? Role { get; set; }


    }

    public class ApplicationRole : IdentityRole
    {


        public int Id { get; set; }

        public int? UserId { get; set; }

        public ICollection<ApplicationUser>? User { get; set; }


    }


}
