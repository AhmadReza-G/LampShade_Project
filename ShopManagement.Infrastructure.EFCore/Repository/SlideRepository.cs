using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository;
public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
{
    private readonly ShopContext _context;

    public SlideRepository(ShopContext context) : base(context)
    {
        _context = context;
    }
    public EditSlide GetDetails(long id)
    {
        return _context.Slides.Select(x => new EditSlide
        {
            Id = x.Id,
            Text = x.Text,
            Heading = x.Heading,
            BtnText = x.BtnText,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Title = x.Title
        }).FirstOrDefault(x => x.Id == id);
    }
    public List<SlideViewModel> GetList()
    {
        return _context.Slides.Select(x => new SlideViewModel
        {
            Id = x.Id,
            Picture = x.Picture,
            Heading = x.Heading,
            Title = x.Title,
            CreationDate = x.CreationDate.ToString(),
            IsRemoved = x.IsRemoved
        }).OrderByDescending(x => x.Id).ToList();
    }
}