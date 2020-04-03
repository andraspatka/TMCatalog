using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TMCatalog.Common.Interfaces;

namespace TMCatalog.Common.Converters
{
    public class ArticleStockConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 2 && values[0] is int && values[1] is Dictionary<int, IStock> && parameter is string)
            {
                Dictionary<int, IStock> stockDictionary = values[1] as Dictionary<int, IStock>;
                IStock stock;
                if (stockDictionary.TryGetValue((int) values[0], out stock))
                {
                    switch ((string)parameter)
                    {
                        case "Quantity":
                            return stock.Quantity > 0 ? "Available" : "Out of stock";

                        case "Price":
                            return $"{stock.Price} Ron";
                    }
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
