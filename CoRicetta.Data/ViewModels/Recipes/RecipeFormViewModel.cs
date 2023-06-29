using CoRicetta.Data.Enum;

namespace CoRicetta.Data.ViewModels.Recipes
{
    public class RecipeFormViewModel
    {
        public string RecipeName { get; set; }
        public Level Level { get; set; }
        public int PrepareTime { get; set; }
        public int CookTime { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int[] Categories { get; set; }
        public string[] Steps { get; set; }
        public int[] Ingredients { get; set; }
        public int[] Quantities { get; set; }
    }
}
