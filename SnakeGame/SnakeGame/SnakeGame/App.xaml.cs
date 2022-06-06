using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
        }

        //private void TryDisplaySnakeGameWindow(Type type)
        //{
        //    if (type == typeof(SnakeGameWindow))
        //    {
        //        snakeGameWindow.Show();
        //        mainWindow.Close();
        //        statsWindow.Close();
        //        difficultyWindow.Close();
        //    }
        //}
    }

}
