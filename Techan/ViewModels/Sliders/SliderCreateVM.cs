using Techan.Models.Common;

namespace Techan.ViewModels.Sliders
{
	public class SliderCreateVM
	{
		public string Title { get; set; }
		public string LittleTitle { get; set; }
		public string BigTitle { get; set; }
		public string Offer { get; set; }
		public IFormFile ImageFile { get; set; }
		public string Link { get; set; }
	}
}
