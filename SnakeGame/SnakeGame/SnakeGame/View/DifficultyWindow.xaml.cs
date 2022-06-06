using SnakeGame.f_ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DifficultyWindow.xaml
    /// </summary>
    public partial class DifficultyWindow : Window
    {
        private ViewModel viewModel;

        public DifficultyWindow()
        {
            InitializeComponent();
        }

        private void SetDifficulty_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            foreach(var option in DifficultyOptions.Children)
            {
                if(option is RadioButton choice && choice.IsChecked==true)
                {
                    switch (choice.Content.ToString().Trim().ToLower())
                    {
                        //case "hard":
                        //    viewModel.GameDifficulty = GameDifficulties.Hard;
                        //    break;
                        //case "medium":
                        //    viewModel.ChangeDifficulty(Model.Difficulty.Medium);
                        //    break;
                        //case "easy":
                        //    viewModel.ChangeDifficulty(Model.Difficulty.Easy);
                        //    break;
                    }
                        
                }
            }

        }
    }
}
