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

namespace MProject
{
    public struct Point
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public int gridWidth = 10;
        //public int gridHeight = 10;

        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

        public List<Point> PointsInRoute { get; set; }

        private Grid dynamicGrid;

        public MainWindow()
        {
            dynamicGrid = new Grid();
            PointsInRoute = new List<Point>();
            GridHeight = 10;
            GridWidth = 10;
            DataContext = this;
            InitializeComponent();
        }

        private void SetWidthHeightButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the Grid
            dynamicGrid = new Grid();

            var mainGrid = Content as Grid;

            var firstSubGrid = mainGrid.Children[0] as Grid;
            var stackPanel = firstSubGrid.Children[0] as StackPanel;
            var widthTB = stackPanel.Children[1] as TextBox;
            var heightTB = stackPanel.Children[3] as TextBox;

            try
            {
                GridWidth = int.Parse(widthTB.Text);
                GridHeight = int.Parse(heightTB.Text);
            }
            catch (Exception)
            {
                return;
            }

            var secondSubGrid = mainGrid.Children[1] as Grid;
            var x = secondSubGrid.ActualHeight;
            var y = secondSubGrid.ActualWidth;

            dynamicGrid.HorizontalAlignment = HorizontalAlignment.Center;
            dynamicGrid.VerticalAlignment = VerticalAlignment.Center;
            dynamicGrid.ShowGridLines = true;
            dynamicGrid.Background = new SolidColorBrush(Colors.Coral);

            // Create Columns
            for (int i = 0; i < GridWidth; i++)
            {
                dynamicGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(y/GridWidth)});
            }

            // Create Rows
            for (int i = 0; i < GridHeight; i++)
            {
                dynamicGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(x/GridHeight)});
            }

            AddElementsToGrid(GridWidth, GridHeight);

            // Display grid into a Window
            Warehouse.Children.Clear();
            Warehouse.Children.Add(dynamicGrid);
        }

        private void AddElementsToGrid(int gridWidth, int gridHeight)
        {
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    Button b = new Button();
                    Grid g = new Grid();
                    SolidColorBrush brush = new SolidColorBrush();
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            brush.Color = Colors.Black;
                        else
                            brush.Color = Colors.WhiteSmoke;
                    }
                    else
                    {
                        if (j % 2 != 0)
                            brush.Color = Colors.Black;
                        else
                            brush.Color = Colors.WhiteSmoke;
                    }


                    g.Background = brush;
                    //Grid.SetRow(g, i);
                    //Grid.SetColumn(g, j);
                    //dynamicGrid.Children.Add(g);

                    b.Name = $"_{i}_{j}";
                    b.Foreground = new SolidColorBrush(Colors.Aqua);
                    b.Background = new SolidColorBrush(Colors.CornflowerBlue);
                    b.Click += SetPointInRoute;


                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    dynamicGrid.Children.Add(b);
                }
            }
        }

        private void SetPointInRoute(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            var br = new SolidColorBrush(Colors.Red);
            var pointStr = b.Name.Split('_');
            var p = new Point { x = Int32.Parse(pointStr[1]), y = Int32.Parse(pointStr[2]) };

            if (b.Background.ToString().Equals(br.ToString()))
            {
                b.Background = new SolidColorBrush(Colors.CornflowerBlue);
                PointsInRoute.Remove(p);
            }

            else
            {
                b.Background = br;
                PointsInRoute.Add(p);
            }

        }
    }
}
