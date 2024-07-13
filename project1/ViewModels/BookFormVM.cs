using Microsoft.AspNetCore.Mvc.Rendering;
using project1.Models;
using System.ComponentModel.DataAnnotations;

namespace project1.ViewModels
{
    public class BookFormVM
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; } = null!;
        [Display(Name ="Author")]
        public int AuthorId { get; set; }
        public List<SelectListItem>? authors { get; set; }
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public IFormFile? imageUrl { get; set; }
        public string Description { get; set; } = null!;
        public List<int> SelectedCategory { get; set; } =  new List<int>();
        public List<SelectListItem>? categories { get; set; }
    }
}
