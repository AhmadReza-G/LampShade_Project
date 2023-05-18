﻿using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastracture.EFCore;
using BlogManagement.Infrastracture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Configuration;
public class BlogManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IArticleApplication, ArticleApplication>();

        services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
        services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

        services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));
    }
}
