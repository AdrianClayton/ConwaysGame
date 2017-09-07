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
    /// Interaction logic for ControlsViewer.xaml
    /// </summary>
    public partial class ControlsViewer : UserControl
    {
        GridViewer associatedGrid;

        public string AssociatedGridName
        {
            get { return associatedGrid.Name; }
            set {if (associatedGrid != null && associatedGrid.controls == this)
                {
                    associatedGrid.controls = null;
                }
                associatedGrid = (GridViewer)Application.Current.MainWindow.FindName(value);
                associatedGrid.controls = this;
            }
        }

        public ControlsViewer()
        {
            InitializeComponent();
        }

        private void SurvivalCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked(sender, e, SpawnOrSurviveRule.SurviveRule, true);
        }

        private void SpawnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked(sender, e, SpawnOrSurviveRule.SpawnRule, true);
        }

        private void SurvivalCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked(sender, e, SpawnOrSurviveRule.SurviveRule, false);
        }

        private void SpawnCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked(sender, e, SpawnOrSurviveRule.SpawnRule, false);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e, SpawnOrSurviveRule rule, bool Checked)
        {
            CheckBox checkbox = (CheckBox)sender;
            int value = Int32.Parse(checkbox.Content.ToString());

            SingleRule newRule = new SingleRule(value, rule);

            associatedGrid.ProcessRuleChange(newRule, Checked);
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            associatedGrid.ProgressState();
        }
    }
}
