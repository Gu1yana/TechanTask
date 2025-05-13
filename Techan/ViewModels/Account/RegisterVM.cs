using System.ComponentModel.DataAnnotations;

namespace Techan.ViewModels.Account;

public class RegisterVM
{
    [MaxLength(64)]
	public string FullName {  get; set; }
	[MaxLength(64)]
	public string UserName { get; set; }
	[MaxLength(128), DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	[MinLength(8), MaxLength(128), DataType(DataType.Password)]	
	public string Password { get; set; }
	[MinLength(8), MaxLength(128), DataType(DataType.Password), Compare(nameof(Password))]
	public string ConfirmPassword { get; set; }
}
