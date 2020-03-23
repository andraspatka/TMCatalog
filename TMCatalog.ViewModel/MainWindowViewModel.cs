// ------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="DVSE GmbH">
//   Copyright (c) by DVSE Gesellschaft für Datenverarbeitung, Service und Entwicklung mbH. All rights reserved.
// </copyright>
// <author>Szilveszter Gorgicze</author>
// ------------------------------------------------------------------------------------------------------------------

namespace TMCatalog.ViewModel
{
    using System.Collections.Generic;
    using TMCatalog.Common.MVVM;
    using TMCatalog.Logic;
    using TMCatalog.ViewModel.UserControlls;
    using TMCatalogClient.Model;

    public class MainWindowViewModel : ViewModelBase
    {

        private int selectedTabIndex;

        public MainWindowViewModel()
        {
            Instance = this;
            this.CloseCommand = new RelayCommand(this.CloseCommandExecute);
            this.VehicleSearchVM = new VehicleSearchVM();
            this.ArticleVM = new ArticleVM();
            this.SelectedTabIndex = 0;
        }

        public static MainWindowViewModel Instance { get; private set; }

        public VehicleSearchVM VehicleSearchVM { get; }

        public ArticleVM ArticleVM { get; }
        public RelayCommand CloseCommand { get; set; }

        public int SelectedTabIndex
        {
            get
            {
                return this.selectedTabIndex;
            }
            set
            {
                this.selectedTabIndex = value;
                this.RaisePropertyChanged();
            }
        }
        public void CloseCommandExecute()
        {
            ViewService.CloseDialog(this);
        }

        public void SetAndOpenArticle(VehicleType selectedVehicle)
        {
            this.SelectedTabIndex = 1;
            this.ArticleVM.VehicleType = selectedVehicle;
        }
    }
}
