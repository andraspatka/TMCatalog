using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCatalog.Common.MVVM;
using TMCatalogClient.Model;

namespace TMCatalog.ViewModel.UserControlls
{
    public class ShoppingBasketVM : ViewModelBase
    {
        private ObservableCollection<Stock> shoppingBasketCollection = new ObservableCollection<Stock>();
        private Stock selectedStock;
        public ShoppingBasketVM()
        {
            this.IncreaseQuantity = new RelayCommand(this.IncreaseQuantityExecute, this.IncreaseDecreaseCanExecute);
            this.DecreaseQuantity = new RelayCommand(this.DecreaseQuantityExecute, this.IncreaseDecreaseCanExecute);
            this.SendOrderCommand = new RelayCommand(this.SendOrderCommandExecute, this.SendOrderCommandCanExecute);
        }

        public ObservableCollection<Stock> ShoppingBasketCollection
        {
            get
            {
                return this.shoppingBasketCollection;
            }
            set
            {
                this.shoppingBasketCollection = value;
                this.RaisePropertyChanged();
            }
        }

        public Stock SelectedStock
        {
            get
            {
                return this.selectedStock;
            }
            set
            {
                this.selectedStock = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand IncreaseQuantity { get; private set; }
        public RelayCommand DecreaseQuantity { get; private set; }
        public RelayCommand SendOrderCommand { get; private set; }

        public void AddStockWithArticleToBasket(Stock stock)
        {
            Stock existentStockItem = this.ShoppingBasketCollection.FirstOrDefault(s => s.ArticleId == stock.ArticleId);

            if (existentStockItem != null)
            {
                existentStockItem.Quantity++;
            }
            else
            {
                this.ShoppingBasketCollection.Add(new Stock(stock));
            }
        }

        private void IncreaseQuantityExecute()
        {
            this.SelectedStock.Quantity++;
        }

        private void DecreaseQuantityExecute()
        {
            this.SelectedStock.Quantity--;

            if (this.SelectedStock.Quantity == 0)
            {
                this.ShoppingBasketCollection.Remove(this.SelectedStock);
                this.SelectedStock = null;
            }
        }

        private bool IncreaseDecreaseCanExecute()
        {
            return this.SelectedStock != null;
        }

        private void SendOrderCommandExecute()
        {//TODO: Decrease stock

        }

        private bool SendOrderCommandCanExecute()
        {
            return this.ShoppingBasketCollection.Count > 0;
        }

    }
}
