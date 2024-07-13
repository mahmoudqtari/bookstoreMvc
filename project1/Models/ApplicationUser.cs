using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(30)]
        public string address { get; set; }
        public bool isDeleted { get; set; }=false;
        public DateTime createdOn {  get; set; } = DateTime.Now;
        public DateTime ureatedOn { get; set; } = DateTime.Now;
    }
}
