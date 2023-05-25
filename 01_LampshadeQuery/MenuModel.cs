using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery;
public class MenuModel
{
    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    public List<ProductCategoryQueryModel> ProductCategories { get; set; }
}
