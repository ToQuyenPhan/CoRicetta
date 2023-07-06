using CoRicetta.Data.Enum;

namespace CoRicetta.Data.ViewModels.Ingredients
{
    public class IngredientFormModel
    {
        public string IngredientName { get; set; }
        public string Measurement { get; set; }
        public int Calories { get; set; }
        public IngredientStatus Status { get; set; }
    }
}
