using System.ComponentModel.DataAnnotations;

namespace InfoTrackTechTest.Models
{
    public class SearchData
    {
        [Required]
        public string SearchTerm { get; set; }

        [Required]
        public string SearchUrl { get; set; }
    }
}
