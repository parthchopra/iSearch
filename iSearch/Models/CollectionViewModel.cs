using System.ComponentModel.DataAnnotations;

namespace iSearch.Models
{
    public class CollectionViewModel
	{
        [Required(ErrorMessage = "Please enter your search query")]
        [Display(Name = "Search Query")]
        [StringLength(50)]
        public string SearchQuery { get; set; } = string.Empty;
        public IEnumerable<SingleCollectionViewModel> Collections { get; set; } = new List<SingleCollectionViewModel>();
    }
}

 