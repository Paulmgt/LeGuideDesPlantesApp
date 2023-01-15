using System.ComponentModel.DataAnnotations;

namespace LeGuideDesPlantesApp.Models
{

    public class HuilesEssentiel
    {
        public int Id { get; set; }

        [Required]
        public string? Nom { get; set; }

        public string? NomLatin { get; set; }

        public string? Photos { get; set; }

        public string? Famille { get; set; }

        public string? MethodeExtraction { get; set; }

        public string? ComposantPrincipal { get; set; }

        public string? Conservation { get; set; }

        public string? ProprietePrincipal { get; set; }

        public string? ActionSurLeCorps { get; set; }

        public string? Circulation { get; set; }

        public string? Digestion { get; set; }

        public string? MusclesEtArticulations { get; set; }

        public string? PeauEtCheveux { get; set; }

        public string? ProblemesFeminins { get; set; }

        public string? GrossesEtEnfants { get; set; }

        public string? SensualitePourCouples { get; set; }

        public string? Precaution { get; set; }

        public int? PlantesSauvagesId { get; set; }

        public PlantesSauvages? PlantesSauvages { get; set; }

        public int? PlantesAromatiquesId { get; set; }

        public PlantesAromatiques? PlantesAromatiques { get; set; }

        public ICollection<Pays>? Pays { get; set; }

        public string? SearchString { get; set; }



    }
}
