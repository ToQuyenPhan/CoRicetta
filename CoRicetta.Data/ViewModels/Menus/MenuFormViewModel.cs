using CoRicetta.Data.Enum;

namespace CoRicetta.Data.ViewModels.Menus
{
    public class MenuFormViewModel
    {
        public int? MenuId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public MenuStatus Status { get; set; }
    }
}
