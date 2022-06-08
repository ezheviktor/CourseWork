using SnakeGame.f_ViewModel;
using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for SnakeGameWindow.xaml
    /// </summary>
    public partial class SnakeGameWindow : Window
    {
        #region Fields 
        private ViewModel viewModel;
        #endregion

        #region Constructors
        public SnakeGameWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();

            BindFieldToGrid();
            BindScoreToWindow();
            DifficultyDisplayer.Text = viewModel.GameDifficulty.ToString();
            Closing += SnakeGameWindow_Closing;

        }
        #endregion

        #region Properties
        #endregion

        #region Events
        #endregion

        #region Handlers
        private void GameWindow_ContentRendered(object sender, EventArgs e)
        {
            viewModel.GameState = GameStates.InGame;
        }
        private void SnakeGameWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.GameState == GameStates.InGame)
                viewModel.GameState = GameStates.NotInGame;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs keyEvent)
        {
            switch (keyEvent.Key)
            {
                case Key.Left or Key.A:
                    viewModel.ChangeSnakeDirection(MovementDirections.Left);
                    break;
                case Key.Right or Key.D:
                    viewModel.ChangeSnakeDirection(MovementDirections.Right);
                    break;
                case Key.Up or Key.W:
                    viewModel.ChangeSnakeDirection(MovementDirections.Up);
                    break;
                case Key.Down or Key.S:
                    viewModel.ChangeSnakeDirection(MovementDirections.Down);
                    break;

                case Key.Space when viewModel.GameState == GameStates.InGame:
                    viewModel.GameState = GameStates.Paused;
                    SnakeField.Effect = new BlurEffect();
                    break;
                case Key.Space when viewModel.GameState == GameStates.Paused:
                    viewModel.GameState = GameStates.InGame;
                    SnakeField.Effect = null;
                    break;
                case Key.Space when viewModel.GameState == GameStates.NotInGame:
                    viewModel.GameState = GameStates.InGame;
                    break;
            }
        }
        #endregion

        #region Methods
        private void BindScoreToWindow()
        {
            Binding binding = new Binding
            {
                Source = viewModel.ScoreCounter,
                Path = new PropertyPath($"Score"),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            ScoreDisplayer.SetBinding(TextBlock.TextProperty, binding);
        }
        private void BindFieldToGrid()
        {
            for (int i = 0; i < viewModel.Field.FieldSize; i++)
            {
                for (int j = 0; j < viewModel.Field.FieldSize; j++)
                {
                    Grid innerGrid = new Grid();
                    innerGrid.SetValue(Grid.RowProperty, i);
                    innerGrid.SetValue(Grid.ColumnProperty, j);
                    Binding myBinding = new Binding
                    {
                        Source = viewModel.Field[i, j],
                        Path = new PropertyPath($"CellType"),
                        Converter = new SnakeFieldConverter(),
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    SnakeField.Children.Add(innerGrid);
                    innerGrid.SetBinding(Grid.BackgroundProperty, myBinding);
                }
            }
        }
        #endregion
    }

}

