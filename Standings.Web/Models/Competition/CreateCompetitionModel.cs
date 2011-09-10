using System.ComponentModel.DataAnnotations;

namespace Standings.Web.Models.Competition
{
    public class CreateCompetitionModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long. It can be up to {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
    }
}