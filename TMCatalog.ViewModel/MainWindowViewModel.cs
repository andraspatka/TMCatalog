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

        public MainWindowViewModel()
        {
            this.CloseCommand = new RelayCommand(this.CloseCommandExecute);
            this.VehicleSearchVM = new VehicleSearchVM();
        }

        public VehicleSearchVM VehicleSearchVM { get; }
    public RelayCommand CloseCommand { get; set; }

    public void CloseCommandExecute()
    {
      ViewService.CloseDialog(this);
    }
  }
}
