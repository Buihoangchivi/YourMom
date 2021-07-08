using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

        public event PropertyChangedEventHandler PropertyChanged;


        public AddBudget(string ColorScheme)
        {
            //ColorScheme = ConfigurationManager.AppSettings["ColorScheme"];
            InitializeComponent();
            ColorScheme = ColorScheme;
            SaveButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            CancelButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
        }

        private string _colorScheme = "";           //Màu nền hiện tại
        public string ColorScheme
        {
            get
            {
                return _colorScheme;
            }
            set
            {
                _colorScheme = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ColorScheme"));
                }
            }
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
            
        //    SaveButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
        //    CancelButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
        //}

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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
