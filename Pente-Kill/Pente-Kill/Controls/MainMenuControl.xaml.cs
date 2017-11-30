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

        public int PlayGridSize { get; set; } = 9;

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
            new PlayerOrComputerControl(Window, PlayGridSize);
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
            int num = new SettingsControl(Window).BoardSize;
            PlayGridSize = num;
        }

        public void UpdateGridSize(int gridSliderSizeSelection)
        {
            PlayGridSize = gridSliderSizeSelection;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
