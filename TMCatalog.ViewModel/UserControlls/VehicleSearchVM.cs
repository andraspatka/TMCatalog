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
    public class VehicleSearchVM : ViewModelBase
    {
        private List<Manufacturer> manufacturers;
        private List<TMCatalogClient.Model.Model> models;
        private int manufacturerID;
        private int modelId;
        private List<VehicleType> vehicleList;
        private VehicleType selectedVehicle;
        public VehicleSearchVM()
        {
            this.Manufacturers = Data.Catalog.GetManufacturers();
            this.ManufacturerId = this.Manufacturers?.FirstOrDefault()?.Id ?? -1;
            this.OpenArticleTabCommand = new RelayCommand(this.OpenArticleTabExecute);
        }

        public List<Manufacturer> Manufacturers
        {
            get
            {
                return manufacturers;
            }
            set
            {
                this.manufacturers = value;
                this.RaisePropertyChanged();
            }
        }
        public int ManufacturerId
        {
            get
            {
                return this.manufacturerID;
            }
            set
            {
                this.manufacturerID = value;
                this.RaisePropertyChanged();
                this.Models = Data.Catalog.GetModels(this.manufacturerID);
            }
        }
        public List<TMCatalogClient.Model.Model> Models
        {
            get
            {
                return this.models;
            }
            set
            {
                this.models = value;
                this.RaisePropertyChanged();
            }
        }

        public int ModelId
        {
            get
            {
                return this.modelId;
            }
            set
            {
                this.modelId = value;
                this.RaisePropertyChanged();
                this.VehicleList = Data.Catalog.GetVehicleTypes(this.modelId);
            }
        }

        public List<VehicleType> VehicleList
        {
            get
            {
                return this.vehicleList;
            }

            set
            {
                this.vehicleList = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand OpenArticleTabCommand
        {
            get;
            private set;
        }

        public VehicleType SelectedVehicle
        {
            get
            {
                return this.selectedVehicle;
            }
            set
            {
                this.selectedVehicle = value;
                this.RaisePropertyChanged();
            }
        }

        private void OpenArticleTabExecute()
        {
            if (this.SelectedVehicle != null)
            {
                MainWindowViewModel.Instance.SetAndOpenArticle(this.SelectedVehicle);
            }
        }
    }
}
