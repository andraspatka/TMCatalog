// ------------------------------------------------------------------------------------------------------------------
// <copyright file="CatalogController.cs" company="DVSE GmbH">
//   Copyright (c) by DVSE Gesellschaft für Datenverarbeitung, Service und Entwicklung mbH. All rights reserved.
// </copyright>
// <author>Szilveszter Gorgicze</author>
// ------------------------------------------------------------------------------------------------------------------

namespace TMCatalog.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using TMCatalog.Model.DBContext;
    using TMCatalogClient.Model;

    using System.Data.Entity;

    public class CatalogController
    {
        private TMCatalogDB catalogDatabase;

        public CatalogController()
        {
            this.catalogDatabase = new TMCatalogDB();
        }

        public List<Manufacturer> GetManufacturers()
        {
            return this.catalogDatabase.Manufacturer.ToList();
        }
        public List<Model> GetModels(int manufacturerId)
        { // FIXME: Solution 1
            return this.catalogDatabase.Models.Where(x => x.ManufacturerId == manufacturerId)/*.AsNoTracking()*/.ToList();
        }

        public List<VehicleType> GetVehicleTypes(int modelId)
        { // FIXME: Solution 2
            //this.catalogDatabase = new TMCatalogDB();
            return this.catalogDatabase.VehicleTypes.
                /*Include("Model").Include("Model.Manufacturer").*/Include("FuelType").
                Where(x => x.ModelId == modelId).
                OrderBy(m => m.Model.Manufacturer.Description).
                ThenBy(m => m.Model.Description).
                ThenBy(m => m.Description).
                /*AsNoTracking().*/ //FIXME: Solution 3
                ToList();
        }

        public List<Product> GetProducts(int vehicleTypeId)
        {
            return this.catalogDatabase.VehicleTypeProducts.
                Where(vt => vt.VehicleTypeId == vehicleTypeId).
                Select(p => p.Product).
                ToList();
        }

        public List<Product> GetProductsV2(int vehicleTypeId)
        {
            List<Product> products = (from p in this.catalogDatabase.Products
                                      join v in this.catalogDatabase.VehicleTypeProducts on p.Id equals v.ProductId
                                      where v.VehicleTypeId == vehicleTypeId
                                      select p).ToList();
            return products;
        }

        public List<ProductGroup> GetProductGroups(int vehicleType)
        {
            IEnumerable<VehicleTypeProducts> vehicleTypeProducts = this.catalogDatabase.VehicleTypeProducts.
                Include("Product").Include("Product.ProductGroup").
                Where(v => v.VehicleTypeId == vehicleType);

            List<ProductGroup> productGroups = new List<ProductGroup>();

            if (vehicleTypeProducts.Count() > 0)
            {
                foreach (IGrouping<ProductGroup, VehicleTypeProducts> item in vehicleTypeProducts.GroupBy(p => p.Product.ProductGroup))
                {
                    ProductGroup productGroup = item.Key;
                    productGroup.Products = new List<Product>();

                    foreach (VehicleTypeProducts vtp in item)
                    {
                        productGroup.Products.Add(vtp.Product);
                    }
                    productGroups.Add(productGroup);
                }
            }

            return productGroups;
        }

        public List<Article> GetArticles(int vehicleTypeId, int productId)
        {
            return this.catalogDatabase.VehicleTypeArticles.
                Include("Article").Include("Article.Product").
                Where(vta => vta.VehicleTypeId == vehicleTypeId && vta.Article.ProductId == productId).
                Select(a => a.Article).ToList();
        }

        public Stock GetArticleStock(int articleId)
        {
            return this.catalogDatabase.Stocks.FirstOrDefault(s => s.ArticleId == articleId);
        }
    }
}
