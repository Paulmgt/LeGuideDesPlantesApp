namespace LeGuideDesPlantesApp.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        public int? HuilesEssentielId { get; set; }

        public ICollection<HuilesEssentiel>? HuilesEssentiel { get; set; }

        public int? ArbresId { get; set; }

        public ICollection<Arbres>? Arbres { get; set; }
        public int? PlantesAromatiquesId { get; set; }

        public ICollection<PlantesAromatiques>? PlantesAromatiques { get; set; }
        public int? PlantesSauvagesId { get; set; }

        public ICollection<PlantesSauvages>? PlantesSauvages { get; set; }




    }
}
