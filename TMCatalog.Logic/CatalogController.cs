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
        {
            return this.catalogDatabase.Models.Where(x => x.ManufacturerId == manufacturerId).ToList();
        }

        public List<VehicleType> GetVehicleTypes(int modelId)
        {
            return this.catalogDatabase.VehicleTypes.
                Include("Model").Include("Model.Manufacturer").Include("FuelType").
                Where(x => x.ModelId == modelId).
                OrderBy(m => m.Model.Manufacturer.Description).
                ThenBy(m => m.Model.Description).
                ThenBy(m => m.Description).
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
    }
}
