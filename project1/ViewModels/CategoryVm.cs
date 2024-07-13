using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace project1.ViewModels
{
    public class CategoryVm
    {
        public int Id { get; set; }
        [MaxLength(30,ErrorMessage ="max is 30")]
        [Required(ErrorMessage="the name is required")]
        [Remote("CheckName",null,ErrorMessage ="exists")]
        public string Name { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
