using System.ComponentModel.DataAnnotations;

namespace University.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "SSN is required.")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "User Type is required.")]
        public string UserType { get; set; }


    }
}
