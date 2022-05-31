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
    /// Interaction logic for SnakeGameWindow.xaml
    /// </summary>
    public partial class SnakeGameWindow : Window
    {
        public SnakeGameWindow()
        {
            InitializeComponent();
            SnakeGameField myField = new SnakeGameField(GameField);

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SnakeGameField myField = new SnakeGameField(GameField);

        }
    }
}
