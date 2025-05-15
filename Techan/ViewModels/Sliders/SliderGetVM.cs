using Techan.Models.Common;
using Techan.ViewModels.CommonVM;

namespace Techan.ViewModels.Sliders
{
    public class SliderGetVM: BaseVM
    {
        public string Title { get; set; }
        public string LittleTitle { get; set; }
        public string BigTitle { get; set; }
        public string Offer { get; set; }
        public string ImagePath { get; set; }
        public string Link { get; set; }
    }
}
