using System.ComponentModel.DataAnnotations;
using Techan.Models.Common;
using Techan.ViewModels.CommonVM;

namespace Techan.ViewModels.Sliders;

public class SliderUpdateVM: BaseVM
{
    [MinLength(10), MaxLength(128)]
    public string Title { get; set; }
    [MinLength(10), MaxLength(128)]
    public string LittleTitle { get; set; }
	[MinLength(10),MaxLength(128)]
	public string BigTitle { get; set; }
    [MinLength(10), MaxLength(128)]
    public string Offer { get; set; }
	public IFormFile? ImageFile { get; set; }
    [MinLength(10), MaxLength(128)]
    public string Link { get; set; }
	public string? ImagePath {  get; set; }
}
