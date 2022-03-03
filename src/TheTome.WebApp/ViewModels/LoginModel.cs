using System.ComponentModel.DataAnnotations;

namespace TheTome.WebApp.ViewModels;

public class LoginModel
{
    [Required(ErrorMessage = "Email not specified")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password not specified")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}