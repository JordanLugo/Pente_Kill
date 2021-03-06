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
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {

        public int BoardSize { get; set; }

        public MainWindow Window { get; set; }

        public SettingsControl(MainWindow main)
        {
            InitializeComponent();
            Window = main;
            Window.Width = 300;
            Window.Height = 300;
            Window.Grid.Children.Clear();
            Window.Grid.Children.Add(this);
        }

        private void HomeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            new MainMenuControl(Window).UpdateGridSize(BoardSize);
        }

        private void BoardSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider gridSizeSlider = (Slider)sender;
            BoardSize = (int)gridSizeSlider.Value;
        }
    }
}
