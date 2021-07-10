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
using System.Text.RegularExpressions;
using System.Configuration;
using System.ComponentModel;

namespace YourMom
{
    /// <summary>
    /// Interaction logic for AddTransaction.xaml
    /// </summary>
    public partial class AddTransaction : Window
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public AddTransaction(string colorScheme)
        {
            
            InitializeComponent();
            ColorScheme = colorScheme;
            SaveButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(colorScheme);
            CancelButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(colorScheme);
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

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

       

        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void Money_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TimeButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? datepicker = DatePicker1.SelectedDate;
            MessageBox.Show(datepicker.Value.ToString());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
