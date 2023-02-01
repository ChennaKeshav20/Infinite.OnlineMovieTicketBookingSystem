using System.ComponentModel.DataAnnotations;

namespace Infinite.OnlineMovieTicketBookingSystem.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
