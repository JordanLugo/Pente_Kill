﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pente_Kill.Controls
{
    /// <summary>
    /// Interaction logic for MainMenuControl.xaml
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        public MainMenuControl(MainWindow main)
        {
            InitializeComponent();
            main.Grid.Children.Clear();
            main.Width = 300;
            main.Height = 300;
            main.Grid.Children.Add(this);            
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
