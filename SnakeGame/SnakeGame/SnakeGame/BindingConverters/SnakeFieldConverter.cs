using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SnakeGame
{
    internal class SnakeFieldConverter : IValueConverter
    {

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException();
            Tuple<int, int> coords = parameter as Tuple<int, int>;
            switch (value)
            {
                case CellTypes.EmptyCell:
                    {
                        if ((coords.Item1+coords.Item2) % 2 == 0)
                            return Brushes.LightGreen/*YellowGreen*/;
                        else if ((coords.Item1 + coords.Item2) % 2 == 1)
                            return Brushes.LightCyan/*MediumSeaGreen*/;
                        else
                            return Brushes.AliceBlue;
                    }
                case CellTypes.FoodCell:
                    return Brushes.Red;
                case CellTypes.SnakeCell:
                    return Brushes.DarkOliveGreen;
                default: return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
        #endregion
    }
}
