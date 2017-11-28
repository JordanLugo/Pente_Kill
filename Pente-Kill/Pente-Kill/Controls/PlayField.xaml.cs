using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Windows.Threading;

namespace Pente_Kill.Controls
{
    /// <summary>
    /// Interaction logic for PlayField.xaml
    /// </summary>
    public partial class PlayField : UserControl
    {
        public MainWindow Main { get; set; }
        public Ellipse[,] Pieces { get; set; } = null;
        private List<Ellipse> placedPieces = new List<Ellipse>();
        private const int gameBoardSize = 550;
        private bool playerOne = true;
        private int turn = 0, gridSize, playerOnePairsCaptured = 0, playerTwoPairsCaptured = 0, timerTicks = 21;
        private bool capture = false, computerPlayer = false;
        private DispatcherTimer timer = new DispatcherTimer();

        public PlayField()
        {
            InitializeComponent();
            gridSize = 10;
            CreateGrid();
            PlayGame();
        }

        public PlayField(MainWindow window, int boardSize, bool computerPlayer)
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            Main = window;
            Main.Width = 850;
            Main.Height = 850;
            Main.Grid.Children.Clear();
            Main.Grid.Children.Add(this);
            this.computerPlayer = computerPlayer;
            gridSize = boardSize;
            CreateGrid();
            PlayGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timerTicks > 0)
            {
                timerTicks--;
                TimerLabel.Content = (timerTicks >= 10) ? $"0:{timerTicks}" : $"0:0{timerTicks}";
                
            }
            else
            {
                EndTurn();
                PlayGame();
            }
        }

        /// <summary>
        /// Creates and fills to a game board
        /// </summary>
        /// <param name="gridSize">The number of pieces along the edges of the wanted upon creation.</param>
        private void CreateGrid()
        {
            Pieces = new Ellipse[gridSize, gridSize];
            Thickness boardSeparation = new Thickness(1);
            Thickness borderThickness = new Thickness(0);
            int column = 0, row = 0, boardSize = (gameBoardSize - gridSize * 2 )/ (gridSize + 1), pieceSize = ((gameBoardSize - (gridSize - 1) * 2) - boardSize) / gridSize;
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
                Pieces[row, column] = new Ellipse();
                Pieces[row, column].Height = pieceSize - 3;
                Pieces[row, column].Width = pieceSize - 3;
                Pieces[row, column].Fill = Brushes.Transparent;
                Grid.SetColumn(Pieces[row, column], column);
                Grid.SetRow(Pieces[row, column], row);
                foreGrid.Children.Add(Pieces[row, column]);
                column = (column == gridSize - 1) ? 0 : column + 1;
                row = (column == 0) ? row + 1 : row;
            }
        }

        private void PlayGame()
        {
            if (!computerPlayer)
            {
                Turn(playerOne);
            }
            else
            {
                if (!playerOne)
                {
                    //Computers Move
                }
            }
        }

        private void EndTurn()
        {
            timerTicks = 21;
            if (turn == 0)
            {
                TurnOne(false);
            }
            else if (turn == 1)
            {
                TurnTwo(false);
            }
            else
            {
                AllOtherTurns(false, Brushes.Transparent);
            }
            playerOne = !playerOne;
            turn++;
        }

        private void Color(Ellipse piece, Brush color, double opacity, bool addRemove)
        {
            if (!placedPieces.Contains(piece))
            {
                piece.Fill = color;
                piece.Opacity = opacity;
                if (addRemove)
                {
                    piece.MouseLeftButtonDown += PlacePiece;
                }
                else
                {
                    piece.MouseLeftButtonDown -= PlacePiece;
                }
            }
        }

        private void TurnOne(bool startEnd)
        {
            Brush color = startEnd ? Brushes.Black : Brushes.Transparent;
            int test = gridSize / 2;
            if (gridSize % 2 == 0)
            {
                Color(Pieces[test - 1, test - 1], color, startEnd ? .5 : 1, startEnd);
                Color(Pieces[test, test - 1], color, startEnd ? .5 : 1, startEnd);
                Color(Pieces[test - 1, test], color, startEnd ? .5 : 1, startEnd);
                Color(Pieces[test, test], color, startEnd ? .5 : 1, startEnd);
            }
            else
            {
                Color(Pieces[test, test], color, startEnd ? .5 : 1, startEnd);
            }
        }

        private void AllOtherTurns(bool startEnd, Brush color)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    Color(Pieces[row, column], color, startEnd ? .5 : 1, startEnd);
                }
            }
        }

        private void TurnTwo(bool startEnd)
        {
            for (int row = 3; row < gridSize - 3; row++)
            {
                for (int column = 3; column < gridSize - 3; column++)
                {
                    Color(Pieces[row, column], startEnd ? Brushes.White : Brushes.Transparent, startEnd ? .5 : 1, startEnd);
                }
            }
        }
        
        private void Turn(bool player)
        {
            timer.Start();
            Brush color = player ? Brushes.Black : Brushes.White;
            if (turn == 0)
            {
                TurnOne(true);
            }
            else if (turn == 1)
            {
                TurnTwo(true);
            }
            else
            {
                AllOtherTurns(true, color);
            }
        }

        private int CheckForCapture(int pieceRow, int pieceColumn, int rowMod, int columnMod, int count, Brush color)
        {
            int jumpCount = count;

            if (pieceColumn + columnMod > -1 && pieceRow + rowMod > -1 && pieceColumn + columnMod < gridSize && pieceRow + rowMod < gridSize && Pieces[pieceRow + rowMod, pieceColumn + columnMod].Fill != color && Pieces[pieceRow + rowMod, pieceColumn + columnMod].Fill != Brushes.Transparent)
            {
                jumpCount++;
                jumpCount = CheckForCapture(pieceRow + rowMod, pieceColumn + columnMod, rowMod, columnMod, jumpCount, color);
            }
            else if (pieceColumn + columnMod > -1 && pieceRow + rowMod > -1 && pieceColumn + columnMod < gridSize && pieceRow + rowMod < gridSize && Pieces[pieceRow + rowMod, pieceColumn + columnMod].Fill == color && jumpCount == 2)
            {
                capture = true;
            }
            if (capture && Pieces[pieceRow, pieceColumn].Fill != color)
            {
                placedPieces.Remove(Pieces[pieceRow, pieceColumn]);
                Pieces[pieceRow, pieceColumn].Fill = Brushes.Transparent;
            }
            else
            {
                jumpCount = 0;
                capture = false;
            }

            return jumpCount;
        }

        private int CheckPieceRowCount(int row, int column, int rowMod, int columnMod, Brush color)
        {
            if (row > -1 && row < gridSize && column > -1 && column < gridSize && Pieces[row, column].Fill == color)
            {
                return CheckPieceRowCount(row + rowMod, column + columnMod, rowMod, columnMod, color) + 1;
            }
            else
            {
                return 0;
            }
        }

        private bool CheckForSpecificNumberOfPiecesInARow(int placedPieceRow, int placedPieceColumn, int rowDirectionMod, int columnDirectionMod, int pieceCountLimit, bool foundEnd = false, bool special = false)
        {
            bool count = false;
            if (placedPieceRow + rowDirectionMod > -1 && placedPieceRow + rowDirectionMod < gridSize && placedPieceColumn + columnDirectionMod > -1 && placedPieceColumn + columnDirectionMod < gridSize && Pieces[placedPieceRow + rowDirectionMod, placedPieceColumn + columnDirectionMod].Fill == Pieces[placedPieceRow, placedPieceColumn].Fill && !foundEnd)
            {
                return CheckForSpecificNumberOfPiecesInARow(placedPieceRow + rowDirectionMod, placedPieceColumn + columnDirectionMod, rowDirectionMod, columnDirectionMod, pieceCountLimit, foundEnd, special);
            }
            else
            {
                foundEnd = true;
            }
            if (foundEnd)
            {
                count = placedPieceRow - (rowDirectionMod * (pieceCountLimit)) > -1 && placedPieceRow - (rowDirectionMod * (pieceCountLimit)) < gridSize && placedPieceColumn - (columnDirectionMod * (pieceCountLimit)) > -1 && placedPieceColumn - (columnDirectionMod * (pieceCountLimit)) < gridSize && Pieces[placedPieceRow - (rowDirectionMod * (pieceCountLimit)), placedPieceColumn - (columnDirectionMod * (pieceCountLimit))].Fill == Brushes.Transparent;
            }
            if (special)
            {
                if (!count)
                {
                    count = placedPieceRow + rowDirectionMod > -1 && placedPieceRow + rowDirectionMod < gridSize && placedPieceColumn + columnDirectionMod > -1 && placedPieceColumn + columnDirectionMod < gridSize && Pieces[placedPieceRow + rowDirectionMod, placedPieceColumn + columnDirectionMod].Fill == Brushes.Transparent;
                }
            }
            else if (count)
            {
                count = placedPieceRow + rowDirectionMod > -1 && placedPieceRow + rowDirectionMod < gridSize && placedPieceColumn + columnDirectionMod > -1 && placedPieceColumn + columnDirectionMod < gridSize && Pieces[placedPieceRow + rowDirectionMod, placedPieceColumn + columnDirectionMod].Fill == Brushes.Transparent;
            }

            return count;
        }

        private bool CheckForPieceGroupings(int pieceCount, int placedPieceRow, int placedPieceColumn, int rowDirectionMod, int columnDirectionMod)
        {
            bool win = false;
            switch (pieceCount)
            {
                case 5:
                    win = true;
                    break;
                case 4:
                    if (CheckForSpecificNumberOfPiecesInARow(placedPieceRow, placedPieceColumn, rowDirectionMod, columnDirectionMod, 4, special: true))
                    {
                        MessageBox.Show($"{(!playerOne ? "Black" : "White")} has achieved Tessera");
                    }
                    break;
                case 3:
                    if (CheckForSpecificNumberOfPiecesInARow(placedPieceRow, placedPieceColumn, rowDirectionMod, columnDirectionMod, 3))
                    {
                        MessageBox.Show($"{(!playerOne ? "Black" : "White")} has achieved Tria");
                    }
                    break;
            }
            return win;
        }

        private bool CheckForWin(int row, int column)
        {
            bool win = false;

            if (!win)
            {
                win = CheckForPieceGroupings(CheckPieceRowCount(row, column, -1, 0, Pieces[row, column].Fill) + CheckPieceRowCount(row, column, 1, 0, Pieces[row, column].Fill) - 1, row, column, -1, 0);
            }
            if (!win)
            {
                win = CheckForPieceGroupings(CheckPieceRowCount(row, column, -1, 1, Pieces[row, column].Fill) + CheckPieceRowCount(row, column, 1, -1, Pieces[row, column].Fill) - 1, row, column, -1, 1);
            }
            if (!win)
            {
                win = CheckForPieceGroupings(CheckPieceRowCount(row, column, 0, 1, Pieces[row, column].Fill) + CheckPieceRowCount(row, column, 0, -1, Pieces[row, column].Fill) - 1, row, column, 0, 1);
            }
            if (!win)
            {
                win = CheckForPieceGroupings(CheckPieceRowCount(row, column, 1, 1, Pieces[row, column].Fill) + CheckPieceRowCount(row, column, -1, -1, Pieces[row, column].Fill) - 1, row, column, 1, 1);
            }
            if (playerOnePairsCaptured == 5 || playerTwoPairsCaptured == 5 && !win)
            {
                win = true;
            }

            return win;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> saveData = new List<object>();
            saveData.Add(Pieces);
            saveData.Add(placedPieces);
            saveData.Add(playerOne);
            saveData.Add(computerPlayer);
            saveData.Add(turn);
            saveData.Add(gridSize);
            saveData.Add(playerOnePairsCaptured);
            saveData.Add(playerTwoPairsCaptured);
        }

        private void PlacePiece(object sender, MouseButtonEventArgs e)
        {
            Ellipse piece = (Ellipse)sender;
            bool found = false;
            int arrayRow = 0, arrayColumn = 0;
            List<int> directionCount = new List<int>();
            if (piece.Opacity == .5)
            {

                for (int row = 0; row < gridSize && !found; row++)
                {
                    for (int column = 0; column < gridSize && !found; column++)
                    {
                        if (Pieces[row, column].Equals(piece))
                        {
                            found = true;
                            Color(piece, playerOne ? Brushes.Black : Brushes.White, 1, false);                
                            placedPieces.Add(piece);
                            EndTurn();
                            arrayRow = row;
                            arrayColumn = column;
                        }
                    }
                }
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, -1, 0, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, -1, 1, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, 0, 1, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, 1, 1, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, 1, 0, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, 1, -1, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, 0, -1, 0, Pieces[arrayRow, arrayColumn].Fill));
                directionCount.Add(CheckForCapture(arrayRow, arrayColumn, -1, -1, 0, Pieces[arrayRow, arrayColumn].Fill));

                foreach (int count in directionCount)
                {
                    if (count == 2)
                    {
                        if (playerOne)
                        {
                            playerOnePairsCaptured++;
                        }
                        else
                        {
                            playerTwoPairsCaptured++;
                        }
                    }
                }
                if (!CheckForWin(arrayRow, arrayColumn))
                {
                    PlayGame();                
                }
                else
                {
                    
                    timer.Stop();
                }
            }

        }
        public void LoadGame(FileStream saveDataFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            List<object> allDataDeserialized = (List<object>)formatter.Deserialize(saveDataFile);

            Pieces = (Ellipse[,])allDataDeserialized.ElementAt(0);
            placedPieces = (List<Ellipse>)allDataDeserialized.ElementAt(1);
            playerOne = (bool)allDataDeserialized.ElementAt(2);
            computerPlayer = (bool)allDataDeserialized.ElementAt(3);
            turn = (int)allDataDeserialized.ElementAt(4);
            gridSize = (int)allDataDeserialized.ElementAt(5);
            playerOnePairsCaptured = (int)allDataDeserialized.ElementAt(6);
            playerTwoPairsCaptured = (int)allDataDeserialized.ElementAt(7);

            CreateGrid();
            PlayGame();
        }
    }
}