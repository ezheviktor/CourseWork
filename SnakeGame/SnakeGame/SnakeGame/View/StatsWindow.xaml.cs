using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        private List<StatsItem> items;
        public StatsWindow()
        {
            InitializeComponent();
            items = SnakeGameFileManager.GetStatistics();
            ListToStatDisplayBinding();
        }

        public void ListToStatDisplayBinding()
        {
            for (int i = 0; i < items.Count; i++)
            {
                StatsDisplay.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < StatsDisplay.ColumnDefinitions.Count; j++)
                {
                    TextBlock innerCellText = new TextBlock();
                    innerCellText.HorizontalAlignment=HorizontalAlignment.Center;
                    innerCellText.SetValue(Grid.RowProperty, i);
                    innerCellText.SetValue(Grid.ColumnProperty, j);
                    Binding cellBind = new Binding()
                    {
                        Source = items[i],
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    };
                    if (j == 0)
                        cellBind.Path = new PropertyPath("GameDifficulty");
                    if(j == 1)
                    {
                        cellBind.Path = new PropertyPath("GameDate");
                        cellBind.Converter = new DateTimeConverter();
                    }
                    if(j==2)
                        cellBind.Path = new PropertyPath("GameScore");
                    innerCellText.SetBinding(TextBlock.TextProperty, cellBind);
                    StatsDisplay.Children.Add(innerCellText);
                }

            }

        }
    }

}
