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
        #region Events
        internal event Action<KeyEventArgs> NotifyLastKeyPressedChanged;
        //public event Action NotifyUpdateFrequencyChanged;
        internal event Action<GameStates> NotifyGameStateChanged;
        #endregion
        #region Fields 
        private KeyEventArgs lastKeyPressed;
        //private double updateFrequencySec;
        private GameStates gameState;

        #endregion

        #region Properties
        internal SnakeField Field { get; set; }
        internal ScoreCounter ScoreCounter { get; set; }
        public DispatcherTimer Timer { get; set; }
        private KeyEventArgs LastKeyPressed
        {
            get => lastKeyPressed;
            set { lastKeyPressed = value; NotifyLastKeyPressedChanged?.Invoke(value); }
        }
        //public double UpdateFrequencySec
        //{
        //    get => updateFrequencySec;
        //    set { updateFrequencySec = value; NotifyUpdateFrequencyChanged?.Invoke(); }
        //}
        internal GameStates GameState
        {
            get => gameState; set{ gameState = value; NotifyGameStateChanged?.Invoke(value); }
        }
        #endregion

        public SnakeGameWindow()
        {
            InitializeComponent();

            Field = new SnakeField();
            ScoreCounter = new ScoreCounter();
            Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.2), };
            GameState = GameStates.InGame;
            BindFieldToGrid();
            SubscribeToEvents();

            //UpdateFrequencySec = 0.25;
            Timer.Start();

        }

        private void BindFieldToGrid()
        {
            for (int i = 0; i < Field.FieldSize; i++)
            {
                for (int j = 0; j < Field.FieldSize; j++)
                {
                    Grid innerGrid = new Grid();
                    innerGrid.SetValue(Grid.RowProperty, i);
                    innerGrid.SetValue(Grid.ColumnProperty, j);
                    Binding myBinding = new Binding
                    {
                        Source = Field[i, j],
                        Path = new PropertyPath($"CellType"),
                        Converter = new SnakeFieldConverter(),
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    SnakeField.Children.Add(innerGrid);
                    innerGrid.SetBinding(Grid.BackgroundProperty, myBinding);

                }
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            LastKeyPressed = e;
        }

        private void LastKeyPressedChanged_Handler(KeyEventArgs e)
        {
            TryChangeSnakeDirection(e);
            TryChangeGameState(e);
        }

        //private void UpdateFrequencyChanged_Handler()
        //{
        //    Timer.Interval = TimeSpan.FromSeconds(UpdateFrequencySec);
        //}

        private void StateGameChanged_Handler(GameStates state)
        {
            switch(state)
            {
                case GameStates.Paused:
                    PauseGame();
                    break;
                case GameStates.InGame:
                    RunGame();
                    break;
                    
            }
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (GameState == GameStates.InGame)
            {
                Field.SnakeFieldUpdate();
            }
        }

        private void SubscribeToEvents()
        {
            Field.MySnake.NotifySnakeIsDead += () => { GameState = GameStates.NotInGame; };
            Field.MySnake.NotifySnakeIsDead += () => { SnakeGameFileManager.SaveScoreToFile(ScoreCounter); };
            Field.MyFood.NotifyFoodIsEaten += (Food eatenFood) => { ScoreCounter.AddToScore(eatenFood.ScoreValue); };
            NotifyLastKeyPressedChanged += LastKeyPressedChanged_Handler;
            //NotifyUpdateFrequencyChanged += UpdateFrequencyChanged_Handler;
            NotifyGameStateChanged += StateGameChanged_Handler;
            Timer.Tick += DispatcherTimer_Tick;

        }

        private void TryChangeSnakeDirection(KeyEventArgs keyEvent)
        {
            switch (keyEvent.Key)
            {
                case Key.Left or Key.A:
                    Field.MySnake.SnakeDirection = Snake.MovementDirections.Left;
                    break;
                case Key.Right or Key.D:
                    Field.MySnake.SnakeDirection = Snake.MovementDirections.Right;
                    break;
                case Key.Up or Key.W:
                    Field.MySnake.SnakeDirection = Snake.MovementDirections.Up;
                    break;
                case Key.Down or Key.S:
                    Field.MySnake.SnakeDirection = Snake.MovementDirections.Down;
                    break;
            }
        }
        private void TryChangeGameState(KeyEventArgs keyEvent)
        {
            switch (keyEvent.Key)
            {
                case Key.Space when GameState==GameStates.InGame:
                    GameState = GameStates.Paused;
                    break;
                case Key.Space when GameState == GameStates.Paused:
                    GameState = GameStates.InGame;
                    break;
            }
        }

        private void PauseGame()
        {
            Timer.Stop();
            SnakeField.Effect = new BlurEffect();
        }
        private void RunGame()
        {
            Timer.Start();
            SnakeField.Effect = null;
        }
    }
    enum GameStates
    {
        InGame, NotInGame, Paused
    }
}

