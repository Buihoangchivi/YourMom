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
        public Transaction transaction = new Transaction();

        //private string tempAmount, tempStakeholder, tempDate, tempNote, tempTransactionType;

        public delegate void AddTransactionDelegate(Transaction transaction, Category category);
        public event AddTransactionDelegate Handler;


        public class Global
        {
            public static string tempAmount;
            public static string tempStakeholder;
            public static DateTime tempDate;
            public static string tempNote;
            public static string tempTransactionType;
            public static string tempColorScheme;
            public static List<TempTransaction> tempTransaction;
        }

        public AddTransaction(string colorScheme)
        {

            InitializeComponent();
            ColorScheme = colorScheme;
            this.DataContext = this;

        }

        //private string _category = "";           //Loại giao dịch lấy ra
        //public string Category
        //{
        //    get
        //    {
        //        return _category;
        //    }
        //    set
        //    {
        //        _category = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("Category"));
        //        }
        //    }
        //}

        private Category category;
        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Category"));
                }
            }
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
                if (_colorScheme == "")
                {

                    _colorScheme = value;

                }
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
                CategorySelect.Global.lol = 0;
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

            if (e.DataObject.GetDataPresent(typeof(string)))
            {

                var text = (string)e.DataObject.GetData(typeof(string));
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Money.Text == "" || DatePicker.SelectedDate == null || Category == null)
            {
                var noti = MessageBox.Show("Please enter all required information.",
                    "Notification",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }
            else
            {
                var datepicker = DatePicker.SelectedDate;

                transaction = new Transaction()
                {

                    ID = Guid.NewGuid().ToString(),
                    Amount = Math.Round(double.Parse(Money.Text), 2),
                    Stakeholder = $" with {Stakeholder.Text}",
                    Date = datepicker.Value,
                    Note = Note.Text,
                    TransactionType = Category.ID

                };

                if (Handler != null)
                {

                    Handler(transaction, category);

                }

                CategorySelect.Global.lol = 0;
                this.Close();
            }
        }

        private void SelectCategoryButton_Click(object sender, RoutedEventArgs e)
        {

            // LƯu lại các thông tin đã nhập
            Global.tempAmount = Money.Text;
            Global.tempStakeholder = Stakeholder.Text;
            DateTime? datepicker = DatePicker.SelectedDate;
            if (datepicker != null)
            {
                Global.tempDate = datepicker.Value;
            }

            Global.tempNote = Note.Text;
            Global.tempColorScheme = _colorScheme;


            CategorySelect categorySelect = new CategorySelect(ColorScheme);

            categorySelect.Handler += Screen_Handler;

            categorySelect.Show();
            //this.Close();

        }

        private void Screen_Handler(Category category)
        {

            Category = category;
            if (category != null)
            {
                string source = category.ImagePath;
                CategoryImage.Source = new BitmapImage(new Uri($"{source}",
                       UriKind.Relative));
                CategorySelectItem.Text = category.Name;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Category != null)
            {
                string source = Category.ImagePath;
                CategoryImage.Source = new BitmapImage(new Uri($"{source}",
                       UriKind.Relative));
                CategorySelectItem.Text = Category.Name;
            }


            if (Global.tempAmount != null && Global.tempAmount != "")
            {
                Money.Text = Global.tempAmount;
            }

            if (Global.tempStakeholder != null)
            {

                if (Global.tempStakeholder.IndexOf(" with ") == 0)
                {

                    Stakeholder.Text = Global.tempStakeholder.Substring(6);

                }
                else
                {

                    Stakeholder.Text = Global.tempStakeholder;

                }
                
            }

            if (Global.tempDate == default(DateTime))
            {
                DateTime? myTime = null;

                DatePicker.SelectedDate = myTime;
            }
            else
            {
                DatePicker.SelectedDate = Global.tempDate;
            }

            if (Global.tempNote != null)
            {
                Note.Text = Global.tempNote;
            }

            if (Global.tempColorScheme != null)
            {
                ColorScheme = Global.tempColorScheme;
            }

            //ColorScheme = _colorScheme;
            SaveButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            CancelButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            //Category = _category;            

        }

    }
}

