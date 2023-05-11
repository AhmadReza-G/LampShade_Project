﻿
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Slide;
using _01_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration;
public class ShopManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductApplication, ProductApplication>();

        services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
        services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

        services.AddTransient<ISlideRepository, SlideRepository>();
        services.AddTransient<ISlideApplication, SlideApplication>();

        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICommentApplication, CommentApplication>();

        services.AddTransient<ISlideQuery, SlideQuery>();
        services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
        services.AddTransient<IProductQuery, ProductQuery>();

        services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

    }
}
