using System.ComponentModel.DataAnnotations;

namespace project1.ViewModels
{
    public class AuthorFormVm
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "max is 50")]
        public string Name { get; set; } = null!;
    }
}
