using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery.Contracts.Slide;
public interface ISlideQuery
{
    List<SlideQueryModel> GetSlides();
}
