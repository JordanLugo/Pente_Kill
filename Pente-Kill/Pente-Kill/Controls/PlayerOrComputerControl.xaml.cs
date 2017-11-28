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
    /// Interaction logic for PlayerOrComputerControl.xaml
    /// </summary>
    public partial class PlayerOrComputerControl : UserControl
    {
        public MainWindow Maine { get; set; }
        public int BoardSize { get; set; }
        public PlayerOrComputerControl(MainWindow window, int boardSize)
        {
            InitializeComponent();
            Maine = window;
            BoardSize = boardSize;
            Maine.Height = 500;
            Maine.Width = 500;
            Maine.Grid.Children.Clear();
            Maine.Grid.Children.Add(this);
        }

        private void ComputerButton_Click(object sender, RoutedEventArgs e)
        {
            new PlayField(Maine, BoardSize, true);
        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            new PlayField(Maine, BoardSize, false);

        }
    }
}
