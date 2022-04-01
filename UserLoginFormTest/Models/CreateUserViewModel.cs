namespace UserLoginFormTest.ViewModels;

public class CreateUserViewModel
{
    [Required]
    [Display(Name = "Email"), MaxLength(100)]
    [EmailAddress (ErrorMessage = "Email address is invalid")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Password")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}

