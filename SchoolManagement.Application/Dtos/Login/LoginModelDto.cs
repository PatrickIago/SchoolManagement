using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.DTOs.Login;
public class LoginModelDto
{
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Passaword is required")]
    public string? Password { get; set; }
}
