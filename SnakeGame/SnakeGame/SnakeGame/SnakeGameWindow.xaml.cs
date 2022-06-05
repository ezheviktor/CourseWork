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
        #region Fields and properties
        private KeyEventArgs lastKeyPressed;
        private KeyEventArgs LastKeyPressed
        {
            get => lastKeyPressed;
            set { lastKeyPressed = value; LastKeyPressedChanged = true; }
        }
        private bool LastKeyPressedChanged { get; set; }

        private enum GameStates
        {
            InGame, NotInGame, Paused
        }
        private GameStates GameState { get; set; }
        private SnakeField Field { get; set; }
        private ScoreCounter Counter { get; set; }
        private SnakeGameFileManager FileManager { get; set; }

        /// <summary>
        private DispatcherTimer DispatcherTimer { get; set; }
        private double UpdateFrequencySec { get; set; }
        /// </summary>
        #endregion

        public SnakeGameWindow()
        {
            InitializeComponent();
            LastKeyPressedChanged = false;
            Field = new SnakeField();
            Counter = new ScoreCounter();
            FileManager = new SnakeGameFileManager();
            GameState = GameStates.NotInGame;

            /////////////////////////////////
            UpdateFrequencySec = 0.25;
            DispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(UpdateFrequencySec)};
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            /////////////////////////////////

            for (int i = 0; i < Field.FieldSize; i++)
            {
                for (int j = 0; j < Field.FieldSize; j++)
                {
                    Grid myGrid = new Grid();
                    myGrid.SetValue(Grid.RowProperty, i);
                    myGrid.SetValue(Grid.ColumnProperty, j);
                    Binding myBinding = new Binding
                    {
                        Source = Field[i,j],
                        Path = new PropertyPath($"CellType"),
                        Converter = new SnakeFieldConverter(),
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    SnakeField.Children.Add(myGrid);
                    myGrid.SetBinding(Grid.BackgroundProperty, myBinding);

                }
            }
        }








        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (GameState == GameStates.InGame)
            {
                if (LastKeyPressedChanged)
                {
                    Snake.MovementDirections? newDirection = GetDirection(LastKeyPressed);
                    if (newDirection != null)
                        Field.MySnake.SnakeDirection = (Snake.MovementDirections)newDirection;
                }
                Field.SnakeFieldUpdate();

            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            GameState = GameStates.InGame;

            Field.MySnake.NotifySnakeIsDead += () => { GameState = GameStates.NotInGame; };
            Field.MySnake.NotifySnakeIsDead += () => { FileManager.SaveScoreToFile(Counter); };
            Field.MyFood.NotifyFoodIsEaten += (Food eatenFood) => { Counter.AddToScore(eatenFood.ScoreValue); };
            DispatcherTimer.Start();

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            LastKeyPressed = e;
        }

        //needed refactoring( method can retuen null in some cases)
        private Snake.MovementDirections? GetDirection(KeyEventArgs keyEvent)
        {
            switch (keyEvent.Key)
            {
                case Key.Left:
                    return Snake.MovementDirections.Left;
                case Key.Right:
                    return Snake.MovementDirections.Right;
                case Key.Up:
                    return Snake.MovementDirections.Up;
                case Key.Down:
                    return Snake.MovementDirections.Down;
                default: return null;
            }
        }


    }
}

