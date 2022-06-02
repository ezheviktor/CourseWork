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

        private bool IsInGame { get; set; }
        private SnakeField Field { get; set; }
#endregion

        public SnakeGameWindow()
        {
            InitializeComponent();
            IsInGame = false;
            Field = new SnakeField();
            LastKeyPressedChanged = false;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            IsInGame = true;

            Field.MySnake.NotifySnakeIsDead += () => { IsInGame = false; };

            while (IsInGame)
            {
                if(LastKeyPressedChanged)
                {
                    Snake.MovementDirections? newDirection = GetDirection(LastKeyPressed);
                    if (newDirection != null)
                        Field.MySnake.SnakeDirection = (Snake.MovementDirections)newDirection;
                }
                Field.SnakeFieldUpdate();

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
                    break;
                case Key.Right:
                    return Snake.MovementDirections.Right;
                    break;
                case Key.Up:
                    return Snake.MovementDirections.Up;
                    break;
                case Key.Down:
                    return Snake.MovementDirections.Down;
                    break;
                default: return null;
            }
        }

    }
}
