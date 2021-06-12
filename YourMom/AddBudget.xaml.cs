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
using System.Windows.Shapes;

namespace YourMom
{
    /// <summary>
    /// Interaction logic for AddBudget.xaml
    /// </summary>
    public partial class AddBudget : Window
    {
        public AddBudget()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var noti = MessageBox.Show("Are you really want to cancel?",
                    "Notification",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
            if (noti == MessageBoxResult.Yes)
            {
                this.Close();
            }
            else
            {
                // Do nothing
            }
        }

        private void Money_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Money_Pasting(object sender, DataObjectPastingEventArgs e)
        {

        }

        private void TimeButton1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TimeButton2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
