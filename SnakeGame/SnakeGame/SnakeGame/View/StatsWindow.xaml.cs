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
            List<string> statStringsFromFile = new List<string>(SnakeGameFileManager.GetStatistics());
            statStringsFromFile.RemoveAll((p) => { return p == ""; });
            items = new List<StatsItem>();
            foreach (var item in statStringsFromFile)
            {
                items.Add(new StatsItem(item));
            }
            ListToStatDisplayBinding();
        }
        public void ListToStatDisplayBinding()
        {
            for (int i = 0; i < items.Count; i++)
            {
                StatsDisplay.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < StatsDisplay.ColumnDefinitions.Count; j++)
                {
                    TextBlock innerTextCell = new TextBlock();
                    innerTextCell.HorizontalAlignment=HorizontalAlignment.Center;
                    innerTextCell.SetValue(Grid.RowProperty, i);
                    innerTextCell.SetValue(Grid.ColumnProperty, j);
                    Binding textCellBind = new Binding()
                    {
                        Source = items[i],
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    };
                    if (j == 0)
                        textCellBind.Path = new PropertyPath("GameDifficulty");
                    if(j == 1)
                        textCellBind.Path = new PropertyPath("GameDate");
                    if(j==2)
                        textCellBind.Path = new PropertyPath("Score");
                    innerTextCell.SetBinding(TextBlock.TextProperty, textCellBind);
                    StatsDisplay.Children.Add(innerTextCell);
                }

            }
            //    for (int i = 0; i < items.Count; ++i)
            //    {
            //        TextBlock dateText = new TextBlock();
            //        Binding dateBinding = new Binding()
            //        {
            //            Source = items[i],
            //            Path = new PropertyPath($"GameDate"),
            //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            //        };
            //        dateText.SetBinding(TextBlock.TextProperty, dateBinding);
            //        dateText.SetValue(DockPanel.DockProperty, Dock.Left);

            //        TextBlock scoreText = new TextBlock();
            //        Binding scoreBinding = new Binding()
            //        {
            //            Source = items[i],
            //            Path = new PropertyPath($"Score"),
            //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            //        };
            //        scoreText.SetBinding(TextBlock.TextProperty, scoreBinding);
            //        scoreText.SetValue(DockPanel.DockProperty, Dock.Right);

            //        DockPanel stackPanelLine = new DockPanel();
            //        stackPanelLine.Children.Add(dateText);
            //        stackPanelLine.Children.Add(scoreText);

            //        StatsDisplay.Children.Add(stackPanelLine);
            //    }
        }
    }

}
