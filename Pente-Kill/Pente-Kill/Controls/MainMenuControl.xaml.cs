using System;
using System.Collections.Generic;
using System.IO;
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
        public MainWindow Window { get; set; }        
        public MainMenuControl(MainWindow main)
        {
            InitializeComponent();
            Window = main;
            Window.Grid.Children.Clear();
            Window.Width = 300;
            Window.Height = 300;
            Window.Grid.Children.Add(this);            
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            new PlayField(Window, playGridSize, false);
        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream io = new FileStream(@"SavePente/Game.ser", FileMode.Open))
            {
                new PlayField(Window).LoadGame(io);
            }
        
        }
        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
            new RulesControl(Window);
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            new SettingsControl(Window).BoardSize = PlayGridSize;        }

        public void UpdateGridSize(int gridSliderSizeSelection)
        {
            playGridSize = gridSliderSizeSelection;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
