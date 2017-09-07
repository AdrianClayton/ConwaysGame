using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameObjects;

namespace ConwayFrontEnd
{
    /// <summary>
    /// Interaction logic for GridViewer.xaml
    /// </summary>
    public partial class GridViewer : UserControl
    {
        enum Directions { Up, Down, Left, Right };

        double elementWidth = GridSegment.SEGMENTSIZE;
        double elementHeight = GridSegment.SEGMENTSIZE;

        int currentSegmentNumberHorizontal = 0;
        int currentSegmentNumberVertical = 0;

        GameController gridController;
        Map gridMap;
        RuleCollection rules;

        internal ControlsViewer controls;

        double canvasWidth
        {
            get { return canvasGrid.RenderSize.Width; }
        }
        double canvasHeight
        {
            get { return canvasGrid.RenderSize.Height; }
        }

        public GridViewer()
        {
            InitializeComponent();
            gridMap = new Map();
            rules = new RuleCollection();
            gridController = new GameController(gridMap, rules);
            
        }

        
        private void GridViewer_KeyDown(object sender, KeyEventArgs e)
        {
            const int SPEED = 5;
            Directions direction;

            switch (e.Key)
            {
                case Key.Left:
                    direction = Directions.Left;
                    break;
                case Key.Right:
                    direction = Directions.Right;
                    break;
                case Key.Up:
                    direction = Directions.Up;
                    break;
                case Key.Down:
                    direction = Directions.Down;
                    break;
                default:
                    return;
            }

            foreach (GridSegment element in canvasGrid.Children)
            {
                MoveElement(direction, SPEED, element);
            }
            e.Handled = true;

        }

        private void GridViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            int newSegmentNumberHorizontal = (int)Math.Ceiling(canvasWidth / elementWidth);
            int newSegmentNumberVertical = (int)Math.Ceiling(canvasHeight / elementHeight);


            if (canvasGrid.Children.Count == 0)
            {
                AddSegments(0, 0, newSegmentNumberHorizontal, newSegmentNumberVertical, 0, 0, 0, 0);
            }

            else
            {
                IInputElement element = canvasGrid.InputHitTest(new Point(0, 0));
                GridSegment firstSegment = Heirarchy.FindParent<GridSegment>((DependencyObject) element);

                if (newSegmentNumberHorizontal < currentSegmentNumberHorizontal)
                {
                    for( int i = currentSegmentNumberHorizontal; i > newSegmentNumberHorizontal; i--)
                    {
                        IEnumerable<GridSegment> segments = (from c in canvasGrid.Children.Cast<GridSegment>() where c.Y == (firstSegment.Y + i) select c);
                        RemoveSegmentsList(segments);
                    }
                }else
                {
                    AddSegments(currentSegmentNumberHorizontal + 1, 0, newSegmentNumberHorizontal, currentSegmentNumberVertical, firstSegment.Y, firstSegment.X, Canvas.GetLeft(firstSegment), Canvas.GetTop(firstSegment));
                }
                if (newSegmentNumberVertical < currentSegmentNumberVertical)
                {
                    for (int i = currentSegmentNumberVertical; i > newSegmentNumberVertical; i--)
                    {
                        IEnumerable<GridSegment> segments = (from c in canvasGrid.Children.Cast<GridSegment>() where c.X == (firstSegment.X + i) select c);
                        RemoveSegmentsList(segments);
                    }
                }
                else
                {
                    AddSegments(0, currentSegmentNumberVertical + 1, newSegmentNumberHorizontal, newSegmentNumberVertical, firstSegment.Y, firstSegment.X, Canvas.GetLeft(firstSegment), Canvas.GetTop(firstSegment));
                }
            }

            currentSegmentNumberHorizontal = newSegmentNumberHorizontal;
            currentSegmentNumberVertical = newSegmentNumberVertical;


            e.Handled = true;
        }

        internal void ProgressState()
        {
            gridController.ProgressState();
            gridMap = gridController.Map;
            foreach (GridSegment segment in canvasGrid.Children)
            {
                segment.RefreshCells();
            }
        }

        /// <summary>
        /// Removes a list of Segments from the GridViewer
        /// </summary>
        /// <param name="segments">IEnumerable list of segments to be removed</param>
        private void RemoveSegmentsList(IEnumerable<GridSegment> segments)
        {
            for (int j = segments.Count(); j > 0; j--)
            {
                canvasGrid.Children.Remove(segments.ElementAt(j - 1));
            }
        }

        /// <summary>
        /// Adds Segments within a range specified and at a displacement specified
        /// </summary>
        /// <param name="horizontalStartNumber">Beginning point in horizontal range</param>
        /// <param name="verticalStartNumber">Beginning point in vertical range</param>
        /// <param name="horizontalEndNumber">end point in horizontal range</param>
        /// <param name="verticalEndNumber">end point in vertical range</param>
        /// <param name="x">Segment X value from which to begin</param>
        /// <param name="y">Segment Y value from which to begin</param>
        /// <param name="leftMargin">left displacement</param>
        /// <param name="topMargin">top displacement</param>
        private void AddSegments(int horizontalStartNumber, int verticalStartNumber, int horizontalEndNumber, int verticalEndNumber, int y, int x, double leftMargin, double topMargin)
        {
            for (int i = horizontalStartNumber; i <= horizontalEndNumber; i++)
            {
                for (int j = verticalStartNumber; j <= verticalEndNumber; j++)
                {
                    GridSegment _segmentToAdd = new GridSegment();
                    _segmentToAdd.X = (x + j);
                    _segmentToAdd.Y = (y + i);

                    canvasGrid.Children.Add(_segmentToAdd);

                    double left = elementWidth * i + leftMargin;
                    double top = elementHeight * j + topMargin;

                    Canvas.SetLeft(_segmentToAdd, left);
                    Canvas.SetTop(_segmentToAdd, top);

                    _segmentToAdd.RefreshCells();
                }
            }
        }


        /// <summary>
        /// Moves a single GridSegment in a direction by a distance specified in pixels 
        /// </summary>
        /// <param name="direction">Direction in which to move</param>
        /// <param name="distance">Integer number of pixels to move by</param>
        /// <param name="element">GridSegment to move</param>
        private void MoveElement(Directions direction, int distance, GridSegment element)
        {
            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);
            bool overEdge = false;

            switch (direction)
            {
                case Directions.Left:
                    left -= distance;
                    if (left <= -elementWidth)
                    {
                        left += (currentSegmentNumberHorizontal + 1) * elementWidth;
                        element.Y += (currentSegmentNumberHorizontal + 1);
                        overEdge = true;
                    }
                    break;
                case Directions.Right:
                    left += distance;
                    if (left > currentSegmentNumberHorizontal * elementWidth)                                          //Necessary to ensure that the 'Extra' segments are always to the right and don't overlap with the 'first' segment
                    {
                        left -= (currentSegmentNumberHorizontal + 1) * elementWidth;
                        element.Y -= (currentSegmentNumberHorizontal + 1);
                        overEdge = true;
                    }
                    break;
                case Directions.Up:
                    top -= distance;
                    if (top <= -elementHeight)
                    {
                        top += (currentSegmentNumberVertical + 1) * elementHeight;
                        element.X += (currentSegmentNumberVertical + 1);
                        overEdge = true;
                    }
                    break;
                case Directions.Down:
                    top += distance;
                    if (top > currentSegmentNumberVertical * elementHeight)                                          //Necessary to ensure that the 'Extra' segments are always to the bottom and don't overlap with the 'first' segment
                    {
                        top -= (currentSegmentNumberVertical + 1) * elementHeight;
                        element.X -= (currentSegmentNumberVertical + 1);
                        overEdge = true;
                    }
                    break;
            }
            if (overEdge)
            {
                element.RefreshCells();
            }

            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);
        }

        internal void AddLivingCell(Coordinate coordinate)
        {
            gridMap.AddCellAtCoordinate(coordinate);
            gridController.Map = gridMap;
        }


        internal void RemoveLivingCell(Coordinate coordinate)
        {
            gridMap.RemoveCellAtCoordinate(coordinate);
            gridController.Map = gridMap;
        }

        internal bool IsCellLiving(Coordinate coordinate)
        {
            LivingCell cell = gridMap.FindCellAtCoordinate(coordinate);
            return (cell != null);
        }

        internal void ProcessRuleChange(SingleRule newRule, bool added)
        {
            if (added && !rules.Contains(newRule))
            {
                rules.Add(newRule);
            }
            else if (!added && rules.Contains(newRule))
            {
                rules.Remove(newRule);
            }
            gridController.ActiveRules = rules;
        }
    }
}
