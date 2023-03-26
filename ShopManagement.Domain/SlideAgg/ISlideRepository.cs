using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.SlideAgg;
public interface ISlideRepository : IRepository<long, Slide>
{
    EditSlide GetDetails(long id);
    List<SlideViewModel> GetList();
}
