using SnakeGame.f_ViewModel;
using SnakeGame.Model;
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

        public DifficultyWindow()
        {
            InitializeComponent();
            ShowPrevSetDifficulty();
        }

        private void ShowPrevSetDifficulty()
        {
            GameDifficulties prevSetDif;
            try
            {
                prevSetDif = SnakeGameFileManager.GetDifficultyFromFile();

            }
            catch (Exception)
            {
                prevSetDif = GameDifficulties.Easy;
            }
            foreach (var option in DifficultyOptions.Children)
            {
                if (option is RadioButton choice && choice.Content.ToString().Trim() == prevSetDif.ToString())
                {
                    choice.SetValue(RadioButton.IsCheckedProperty, true);
                }
            }
        }

        private void SetDifficulty_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            string difficulty = null;
            foreach (var option in DifficultyOptions.Children)
            {
                if (option is RadioButton choice && choice.IsChecked == true)
                {
                    difficulty = choice.Content.ToString().Trim().ToLower();
                }
            }
            if (difficulty != null)
            {
                if (difficulty == "hard")
                    SnakeGameFileManager.SaveDifficultyToFile(GameDifficulties.Hard);
                if (difficulty == "medium")
                    SnakeGameFileManager.SaveDifficultyToFile(GameDifficulties.Medium);
                if (difficulty == "easy")
                    SnakeGameFileManager.SaveDifficultyToFile(GameDifficulties.Easy);
            }

        }
    }
}
