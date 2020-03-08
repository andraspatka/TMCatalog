// ------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="DVSE GmbH">
//   Copyright (c) by DVSE Gesellschaft für Datenverarbeitung, Service und Entwicklung mbH. All rights reserved.
// </copyright>
// <author>Szilveszter Gorgicze</author>
// ------------------------------------------------------------------------------------------------------------------

namespace TMCatalog.ViewModel
{
  using TMCatalog.Common.MVVM;
  using TMCatalog.Logic;

  public class MainWindowViewModel : ViewModelBase
   {

    public static CatalogController CatalogController;

    public MainWindowViewModel()
    {
      CatalogController = new CatalogController();
      this.CloseCommand = new RelayCommand(this.CloseCommandExecute);
    }

    public RelayCommand CloseCommand { get; set; }

    public void CloseCommandExecute()
    {
      ViewService.CloseDialog(this);
    }
  }
}
