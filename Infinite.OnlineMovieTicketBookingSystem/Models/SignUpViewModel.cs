using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Infinite.OnlineMovieTicketBookingSystem.Models
{
    public class SignUpViewModel
    {
        
        
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Please enter your name")]

            public string UserName { get; set; }

            [Required(ErrorMessage = "Please enter your Email")]

            [Display(Name = "Email Address")]

            public string Email { get; set; }

            [Required(ErrorMessage = "Please enter your Mobile Number")]
            [DataType(DataType.PhoneNumber)]
            public string? MobileNo { get; set; }


            [Required(ErrorMessage = "Please enter your Password")]
            [DataType(DataType.Password)]
            //[RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please Enter valid Password")]
            public string Password { get; set; }



            [Required(ErrorMessage = "please confirm your password")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
            public string ConfirmPassword { get; set; }


            [Required(ErrorMessage = "please select any of these")]

            public string Roles { get; set; }



        
    }
}
