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
    /// Interaction logic for WinControl.xaml
    /// </summary>
    public partial class WinControl : UserControl
    {
        public MainWindow Winder { get; set; }
        public WinControl(MainWindow window, bool isBlackWin)
        {
            InitializeComponent();
            winLabel.Content = (isBlackWin) ? "Congrats Black, you win!!!" : "Congrats White, you win!!!";
            Winder.Width = 300;
            Winder.Height = 300;
            Winder.Grid.Children.Clear();
            Winder.Grid.Children.Add(this);
        }
    }
}
