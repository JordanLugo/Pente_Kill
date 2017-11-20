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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pente_Kill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateGrids();
        }

        public void PopulateGrids()
        {
            for (int i = 0; i < TileGrid.Columns; i++)
            {
                for (int j = 0; j < TileGrid.Rows; j++)
                {
                    Label l = new Label();
                    l.Background = Brushes.LightGray;
                    l.BorderBrush = Brushes.DarkGray;
                    if (i == 0 && j == 0)
                        l.BorderThickness = new Thickness(0, 0, 1, 1);
                    else if (i == 0 && (j != 0 && j != TileGrid.Rows))
                        l.BorderThickness = new Thickness(1, 0, 1, 1);
                    else if (i == 0 && j == TileGrid.Rows)
                        l.BorderThickness = new Thickness(1, 0, 0, 1);
                    else if ((i != 0 && i != TileGrid.Columns) && j == 0)
                        l.BorderThickness = new Thickness(0, 1, 1, 1);
                    else if (i == TileGrid.Columns && j == 0)
                        l.BorderThickness = new Thickness(0, 1, 1, 0);
                    else if (i == TileGrid.Columns && (j != 0 && j != TileGrid.Rows))
                        l.BorderThickness = new Thickness(1, 1, 1, 0);
                    else if (i == TileGrid.Columns && j == TileGrid.Rows)
                        l.BorderThickness = new Thickness(1, 1, 0, 0);
                    else if ((i != 0 && i != TileGrid.Columns) && j == TileGrid.Rows)
                        l.BorderThickness = new Thickness(1, 1, 0, 1);
                    else
                        l.BorderThickness = new Thickness(1);
                    TileGrid.Children.Add(l);

                }
            }
            PlacementGrid.Margin = new Thickness(40);
            for (int i = 0; i < PlacementGrid.Columns; i++)
            {
                for (int j = 0; j < PlacementGrid.Rows; j++)
                {
                    Label l = new Label()
                    {
                        Background = Brushes.Transparent
                    };
                    l.MouseLeftButtonDown += PlacePiece;
                }
            }
        }

        private void PlacePiece(object sender, MouseButtonEventArgs e)
        {
            Shape ea = new Ellipse()
            {
                Width = 10,
                Height = 10
            };
            switch (currentPlayer)
            {
                case true:
                    ea.Fill = Brushes.Black;
                    break;
                case false:
                    ea.Fill = Brushes.White;
                    break;
            }
            currentPlayer = !currentPlayer;
        }

        private static bool currentPlayer = false;
    }
}