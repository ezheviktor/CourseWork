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
            SnakeGame.MainWindow window = new SnakeGame.MainWindow();
            window.Show();
            //SnakeGame.Model.SnakeField fieldTemp = new Model.SnakeField();
            //fieldTemp.TestCheckBorderCrossing();
        }
    }
}
