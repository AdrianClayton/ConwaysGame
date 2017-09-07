using GameObjects;
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

namespace ConwayFrontEnd
{
    /// <summary>
    /// Interaction logic for GridSegment.xaml
    /// </summary>
    public partial class GridSegment : UserControl
    {
        public const int SEGMENTSIZE = 100;
        public const int GRIDNUMBER = 5;

        public GridViewer Master
        {
            get { return Heirarchy.FindParent<GridViewer>(this); }
        }

        public int Y;
        public int X;

        public GridSegment()
        {

            // Create the Map
            Grid DynamicGrid = new Grid();

            for (int i=0; i < GRIDNUMBER; i++)
            {
                // Create Columns and Rows
                ColumnDefinition gridCol = new ColumnDefinition();
                RowDefinition gridRow = new RowDefinition();

                DynamicGrid.ColumnDefinitions.Add(gridCol);
                DynamicGrid.RowDefinitions.Add(gridRow);

                for (int j= 0; j < GRIDNUMBER; j++)
                {
                    // Create Cells
                    Button button = new Button();
                    button.Name = "Cell_" + i+ "_" + j;

                    // Add Cells to correct position
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    
                    button.Click += Button_Click;

                    button.Background = Brushes.White;
                    DynamicGrid.Children.Add(button);
                }
            }

            // Display grid into a Window
            Content = DynamicGrid;

            Height = Width = SEGMENTSIZE;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int x, y;
            getButtonName(button, out x, out y);

            Coordinate coordinateToChange = ButtonToCoordinate(x, y);

            if (button.Background == Brushes.White)
            {
                Master.AddLivingCell(coordinateToChange);
                button.Background = Brushes.Black;
            }
            else
            {
                Master.RemoveLivingCell(coordinateToChange);
                button.Background = Brushes.White;
            }


        }

        private static void getButtonName(Button button, out int x, out int y)
        {
            string[] nameArray = button.Name.Split('_');
            x = Int32.Parse(nameArray[1]);
            y = Int32.Parse(nameArray[2]);
        }

        internal void RefreshCells()
        {
            Grid DynamicGrid = (Grid)Content;
            foreach (Button button in DynamicGrid.Children)
            {
                int x, y;
                getButtonName(button, out x, out y);

                Coordinate coordinate = ButtonToCoordinate(x, y);

                if(Master.IsCellLiving(coordinate))
                    button.Background = Brushes.Black;
                else
                    button.Background = Brushes.White;
            }
        }

        internal Coordinate ButtonToCoordinate(int x, int y)
        {
            int coordinateX = X * GRIDNUMBER + x;
            int coordinateY = Y * GRIDNUMBER + y;
            
            Coordinate coordinate = new Coordinate(coordinateX, coordinateY);
            return coordinate;
        }
    }
}
