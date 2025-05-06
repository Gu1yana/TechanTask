using System.ComponentModel.DataAnnotations;
using Techan.Models.Common;

namespace Techan.Models;

public class Category:BaseEntity
{
	[MinLength(3),MaxLength(64)]
	public string Name { get; set; }
}