using Techan.ViewModels.Products;
using Techan.ViewModels.Sliders;

namespace Techan.HomeViewModels;

public class HomeVM
{
    public List<SliderGetVM> Sliders {  get; set; }    
    public List<ProductListVM> Products {  get; set; }   
}