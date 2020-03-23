using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCatalog.Common.MVVM;
using TMCatalog.Logic;
using TMCatalogClient.Model;

namespace TMCatalog.ViewModel.UserControlls
{
    public class ArticleVM : ViewModelBase
    {
        private string selectedVehicleDescription;
        private VehicleType vehicleType;
        private List<Product> products;
        public ArticleVM()
        {
        }

        public VehicleType VehicleType
        {
            get
            {
                return this.vehicleType;
            }
            set
            {
                this.vehicleType = value;
                this.SelectedVehicleDescription = $"{this.vehicleType.Model.Manufacturer.Description} - " +
                    $"{this.vehicleType.Model.Description} - {this.vehicleType.Description}";

                this.Products = Data.Catalog.GetProductsV2(this.vehicleType.Id);

            }
        }

        public string SelectedVehicleDescription
        {
            get
            {
                return this.selectedVehicleDescription;
            }
            set
            {
                this.selectedVehicleDescription = value;
                this.RaisePropertyChanged();
            }
        }

        public List<Product> Products
        {
            get
            {
                return this.products;
            }

            set
            {
                this.products = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
