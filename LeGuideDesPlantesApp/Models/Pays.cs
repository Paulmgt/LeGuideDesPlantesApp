using System.ComponentModel.DataAnnotations;

namespace LeGuideDesPlantesApp.Models
{

    public class Pays
    {
        public int Id { get; set; }

        [Required]
        public string? NomPays { get; set; }

        public int? HuilesEssentielId { get; set; }
        public ICollection<HuilesEssentiel>? HuilesEssentiels { get; set; }

        public int? PlantesAromatiquesId { get; set; }
        public ICollection<PlantesAromatiques>? PlantesAromatiques { get; set; }

        public int? PlantesSauvagesId { get; set; }
        public ICollection<PlantesSauvages>? PlantesSauvages { get; set; }

        public int? ArbresId { get; set; }
        public ICollection<Arbres>? Arbres { get; set; }
    }
}
