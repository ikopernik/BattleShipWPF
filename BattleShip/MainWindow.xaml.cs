using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM mainVM;
        public MainWindow()
        {
            InitializeComponent();
            InitGrids();
        }

        private void InitGrids()
        {
            mainVM = new MainVM();
            DataContext = mainVM;
            InitGrid(playerGrid, mainVM.PlayerMap);
            InitGrid(enemyGrid, mainVM.EnemyMap);
        }

        private void InitGrid(Grid grid, BattleMap battleMap)
        {
            for (int i = 0; i < 10; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ContentControl contentControl = new ContentControl();
                    contentControl.DataContext = mainVM;
                    contentControl.ContentTemplate = (DataTemplate)this.FindResource("CellTemplate");
                    
                    // Bind the Content property to your data item
                    Binding binding = new Binding();
                    binding.Source = battleMap[i, j];
                    contentControl.SetBinding(ContentControl.ContentProperty, binding);

                    // Add ContentControl to the second column
                    Grid.SetColumn(contentControl, i);
                    Grid.SetRow(contentControl, j);

                    grid.Children.Add(contentControl);
                }
            }
        }
    }
}