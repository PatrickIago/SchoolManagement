using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.DTOs.Login;
public class RegisterModelDto
{
    [Required(ErrorMessage = "User Name is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
