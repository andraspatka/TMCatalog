using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCatalog.Common.Interfaces;
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
        private List<Article> articles;
        private Dictionary<int, IStock> stockDictionary;
        private Article selectedArticle;


        public ArticleVM()
        {
            this.AddToBasketCommand = new RelayCommand(this.AddToBasketExecute, this.AddToBasketCanExecute);
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

        public RelayCommand AddToBasketCommand { get; private set; }

        public Article SelectedArticle
        {
            get
            {
                return this.selectedArticle;
            }
            set
            {
                this.selectedArticle = value;
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
                    this.selectedTreeViewItem = value;
                    this.GetArticles(this.selectedTreeViewItem as Product);
                    this.RaisePropertyChanged();
                } 
                else
                {
                    this.Articles = new List<Article>();
                }
            }
        }

        public List<Article> Articles
        {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
                this.RaisePropertyChanged();
            }
        }

        public Dictionary<int, IStock> StockDictionary
        {
            get
            {
                return this.stockDictionary;
            }
            set
            {
                this.stockDictionary = value;
                this.RaisePropertyChanged();
            }
        }

        private void GetArticles(Product product)
        {
            this.StockDictionary = new Dictionary<int, IStock>();
            this.Articles = Data.Catalog.GetArticles(this.VehicleType.Id, product.Id);
            if (this.Articles?.Count > 0)
            {
                foreach (Article article in this.Articles)
                {
                    Stock stock = Data.Catalog.GetArticleStock(article.Id);
                    if (stock != null)
                    {
                        this.StockDictionary[article.Id] = stock;
                    }
                }
            }
        }

        private void AddToBasketExecute()
        {

        }

        private bool AddToBasketCanExecute()
        {
            if (this.SelectedArticle != null)
            {
                Stock stock = this.StockDictionary[this.SelectedArticle.Id] as Stock;
                return stock.Quantity > 0;
            }
            return false;
        }
        
    }
}
