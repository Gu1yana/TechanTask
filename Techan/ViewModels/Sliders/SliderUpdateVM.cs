using Techan.Models.Common;

namespace Techan.ViewModels.Sliders;

public class SliderUpdateVM:BaseEntity
{
	public string Title { get; set; }
	public string LittleTitle { get; set; }
	public string BigTitle { get; set; }
	public string Offer { get; set; }
	public string ImageUrl { get; set; }
	public string Link { get; set; }
}
