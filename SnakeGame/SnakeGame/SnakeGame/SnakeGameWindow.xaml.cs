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
        //private bool IsInGame { get; set; }
        private SnakeField Field { get; set; }
        private ScoreCounter Counter { get; set; }
        private SnakeGameFileManager FileManager { get; set; }
        #endregion

        public SnakeGameWindow()
        {
            InitializeComponent();
            LastKeyPressedChanged = false;
            Field = new SnakeField();
            Counter = new ScoreCounter();
            FileManager = new SnakeGameFileManager();
            //this.Resources.Add("Field", Field);


            //IsInGame = false;
            GameState=GameStates.NotInGame;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //IsInGame = true;
            GameState = GameStates.InGame;

            Field.MySnake.NotifySnakeIsDead += () => { /*IsInGame = false;*/ GameState = GameStates.NotInGame; };
            Field.MySnake.NotifySnakeIsDead += () => { FileManager.SaveScoreToFile(Counter); };
            Field.MyFood.NotifyFoodIsEaten += (Food eatenFood) => { Counter.AddToScore(eatenFood.ScoreValue); };

            while (GameState == GameStates.InGame)
            {
                if(LastKeyPressedChanged)
                {
                    Snake.MovementDirections? newDirection = GetDirection(LastKeyPressed);
                    if (newDirection != null)
                        Field.MySnake.SnakeDirection = (Snake.MovementDirections)newDirection;
                }
                Field.SnakeFieldUpdate();
                Field.TestFieldDebuggerDisplay();//////

            }
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
