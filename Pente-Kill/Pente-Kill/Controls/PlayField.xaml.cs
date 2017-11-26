using Pente_Kill.Models;
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
    /// Interaction logic for PlayField.xaml
    /// </summary>
    public partial class PlayField : UserControl
    {
        public MainWindow Main { get; set; }
        public Label[,] Pieces { get; set; } = null;
        private const int gameBoardSize = 550;
        public PlayField()
        {
            InitializeComponent();
            CreateGrid(9);
        }

        public PlayField(MainWindow window)
        {
            Main = window;
            Main.Grid.Children.Clear();
            Main.Width = 850;
            Main.Height = 850;
            CreateGrid(19);
        }
        /// <summary>
        /// Creates and fills to a game board
        /// </summary>
        /// <param name="gridSize">The number of pieces along the edges of the wanted upon creation.</param>
        private void CreateGrid(int gridSize)
        {
            Thickness boardSeparation = new Thickness(1);
            Thickness borderThickness = new Thickness(0);
            int column = 0, row = 0, boardSize = gameBoardSize/(gridSize + 1), pieceSize = (gameBoardSize - boardSize) / gridSize;
            foreGrid.ColumnDefinitions.Clear();
            foreGrid.RowDefinitions.Clear();
            backGrid.ColumnDefinitions.Clear();
            backGrid.RowDefinitions.Clear();
            foreGrid.Margin = new Thickness(boardSize/2);
            intersections.Margin = new Thickness(boardSize/4);

            for (int pieceCount = 0; pieceCount < gridSize; pieceCount++)
            {
                foreGrid.ColumnDefinitions.Add(new ColumnDefinition());
                foreGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int boardCount = 0; boardCount < gridSize + 1; boardCount++)
            {
                backGrid.ColumnDefinitions.Add(new ColumnDefinition());
                backGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int gridBoard = 0; gridBoard < Math.Pow(gridSize + 1, 2); gridBoard++)
            {
                Label boardSquare = new Label();
                boardSquare.Height = boardSize;
                boardSquare.Width = boardSize;
                boardSquare.Background = Brushes.GreenYellow;
                boardSquare.Margin = boardSeparation;
                boardSquare.BorderThickness = borderThickness;
                Grid.SetColumn(boardSquare, column);
                Grid.SetRow(boardSquare, row);
                backGrid.Children.Add(boardSquare);
                column = (column == gridSize) ? 0 : column + 1;
                row = (column == 0) ? row + 1 : row;
            }
            column = 0;
            row = 0;
            for (int gridPosition = 0; gridPosition < Math.Pow(gridSize, 2); gridPosition++)
            {
                Pieces = new Label[gridSize, gridSize];
                Pieces[column, row] = new Label();
                Pieces[column, row].Height = pieceSize;
                Pieces[column, row].Width = pieceSize;
                Pieces[column, row].Background = Brushes.Violet;
                Pieces[column, row].Margin = boardSeparation;
                Pieces[column, row].BorderThickness = borderThickness;
                Pieces[column, row].MouseLeftButtonDown += PlacePiece;
                Grid.SetColumn(Pieces[column, row], column);
                Grid.SetRow(Pieces[column, row], row);
                foreGrid.Children.Add(Pieces[column, row]);
                column = (column == gridSize - 1) ? 0 : column + 1;
                row = (column == 0) ? row + 1 : row;
            }
        }

        public void PopulateGrids()
        {
            //for (int i = 0; i < TileGrid.Columns; i++)
            //{
            //    for (int j = 0; j < TileGrid.Rows; j++)
            //    {
            //        Label l = new Label();
            //        l.Background = Brushes.LightGray;
            //        l.BorderBrush = Brushes.DarkGray;
            //        if (i == 0 && j == 0)
            //            l.BorderThickness = new Thickness(0, 0, 1, 1);
            //        else if (i == 0 && (j != 0 && j != TileGrid.Rows))
            //            l.BorderThickness = new Thickness(1, 0, 1, 1);
            //        else if (i == 0 && j == TileGrid.Rows)
            //            l.BorderThickness = new Thickness(1, 0, 0, 1);
            //        else if ((i != 0 && i != TileGrid.Columns) && j == 0)
            //            l.BorderThickness = new Thickness(0, 1, 1, 1);
            //        else if (i == TileGrid.Columns && j == 0)
            //            l.BorderThickness = new Thickness(0, 1, 1, 0);
            //        else if (i == TileGrid.Columns && (j != 0 && j != TileGrid.Rows))
            //            l.BorderThickness = new Thickness(1, 1, 1, 0);
            //        else if (i == TileGrid.Columns && j == TileGrid.Rows)
            //            l.BorderThickness = new Thickness(1, 1, 0, 0);
            //        else if ((i != 0 && i != TileGrid.Columns) && j == TileGrid.Rows)
            //            l.BorderThickness = new Thickness(1, 1, 0, 1);
            //        else
            //            l.BorderThickness = new Thickness(1);
            //        TileGrid.Children.Add(l);

            //    }
            //}
            //Double margin = TileGrid.Width / 40;
            //PlacementGrid.Margin = new Thickness(margin);
            //for (int i = 0; i < PlacementGrid.Columns; i++)
            //{
            //    for (int j = 0; j < PlacementGrid.Rows; j++)
            //    {
            //        Label l = new Label()
            //        {
            //            Background = Brushes.Blue
            //        };
            //        l.MouseLeftButtonDown += PlacePiece;
            //        PlacementGrid.Children.Add(l);
            //    }
            //}
        }

        private void PlacePiece(object sender, MouseButtonEventArgs e)
        {
            Shape piece = new Ellipse()
            {
                Width = 10,
                Height = 10
            };
            switch (currentPlayer)
            {
                case true:
                    piece.Fill = Brushes.Black;
                    break;
                case false:
                    piece.Fill = Brushes.White;
                    break;
            }
            currentPlayer = !currentPlayer;
            Label position = (Label)sender;
            position.Content = piece;
        }

        private static bool currentPlayer = false;
    }
}

