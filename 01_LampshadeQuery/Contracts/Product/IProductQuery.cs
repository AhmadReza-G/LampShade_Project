﻿namespace _01_LampshadeQuery.Contracts.Product;
public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
}
