namespace TWebApiSearch.Models
{
    public class SearchPanelInformation
    {
        public int Id { get; set; }
        public string SearchText { get; set; }
        public string SearchDetails { get; set; }
        public string ControllerName { get; set; }
        public string ViewName { get; set; }
        public string ViewTitle { get; set; }
    }
}