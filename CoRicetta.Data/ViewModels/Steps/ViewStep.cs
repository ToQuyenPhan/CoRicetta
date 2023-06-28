using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Steps
{
    public class ViewStep
    {
        public int Id { get; set; }

        [JsonPropertyName("step_number")]
        public int StepNumber { get; set; }
        public string Description { get; set; }
    }
}
