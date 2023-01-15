using System.ComponentModel.DataAnnotations;

namespace LeGuideDesPlantesApp.Models
{

    public class PlantesSauvages
    {
        public int Id { get; set; }

        [Required]
        public string? Nom { get; set; }

        public string? Description { get; set; }

        public string? Photos { get; set; }

        public string? Habitat { get; set; }

        public string? Danger { get; set; }

        public string? BienFait { get; set; }

        public string? Culture { get; set; }

        public string? Taille { get; set; }

        public string? Rusticite { get; set; }

        public string? Maladies { get; set; }

        public string? PeriodeDeFleuraison { get; set; }

        public string? Arosage { get; set; }

        public string? Voisinage { get; set; }

        public string? PrefenrenceTerrain { get; set; }

        public string? Entretiens { get; set; }

        public HuilesEssentiel? HuilesEssentiel { get; set; }

        public ICollection<Pays>? Pays { get; set; }

        public string? RecetteSimple { get; set; }

        public string? SearchString { get; set; }


    }
}
