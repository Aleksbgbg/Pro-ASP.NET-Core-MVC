namespace SportsStore.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class Login
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}