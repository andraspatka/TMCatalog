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
        private List<ProductGroup> productGroups;
        private object selectedTreeViewItem;


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

                this.ProductGroups = Data.Catalog.GetProductGroups(this.vehicleType.Id);

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

        public List<ProductGroup> ProductGroups
        {
            get
            {
                return this.productGroups;
            }
            set
            {
                this.productGroups = value;
                this.RaisePropertyChanged();
            }
        }

        public object SelectedTreeViewItem
        {
            get
            {
                return this.selectedTreeViewItem;
            }
            set
            {
                if (value is Product)
                {
                    this.selectedTreeViewItem = SelectedTreeViewItem;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
