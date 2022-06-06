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
            switch(value )
            {
                case Cell.CellTypes.EmptyCell:
                    return Brushes.AliceBlue;
                case Cell.CellTypes.FoodCell:
                    return Brushes.Red;
                case Cell.CellTypes.SnakeCell:
                    return Brushes.Green;
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
