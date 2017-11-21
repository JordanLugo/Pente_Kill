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

namespace Pente_Kill.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public MainWindow Main { get; set; }
        public UserControl1()
        {
            InitializeComponent();
            PopulateGrids();
        }

        public UserControl1(MainWindow window)
        {
            Main = window;
            Main.Grid.Children.Clear();
            Main.Width = 850;
            Main.Height = 850;
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
            Double margin = TileGrid.Width / 40;
            PlacementGrid.Margin = new Thickness(margin);
            for (int i = 0; i < PlacementGrid.Columns; i++)
            {
                for (int j = 0; j < PlacementGrid.Rows; j++)
                {
                    Label l = new Label()
                    {
                        Background = Brushes.Blue
                    };
                    l.MouseLeftButtonDown += PlacePiece;
                    PlacementGrid.Children.Add(l);
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
