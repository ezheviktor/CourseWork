﻿using SnakeGame.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            SnakeField snakeField = new SnakeField();
            snakeField.CheckBorderCrossing();
            SnakeGameWindow snake = new SnakeGameWindow();
            snake.Show();
        }

        private void StatsWindowClick(object sender, RoutedEventArgs e)
        {
            StatsWindow stats = new StatsWindow();
            stats.Show();
        }
    }
}
