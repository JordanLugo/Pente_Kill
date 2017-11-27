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
    /// Interaction logic for RulesControl.xaml
    /// </summary>
    public partial class RulesControl : UserControl
    {
        public MainWindow Window { get; set; }
        public RulesControl(MainWindow main)
        {
            InitializeComponent();
            Window = main;
            Window.Height = 300;
            Window.Width = 300;
            Window.Grid.Children.Clear();
            Window.Grid.Children.Add(this);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            new MainMenuControl(Window);
        }
    }
}
