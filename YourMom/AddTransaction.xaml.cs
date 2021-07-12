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
        public TempTransaction transaction = new TempTransaction();

        //private string tempAmount, tempStakeholder, tempDate, tempNote, tempTransactionType;
        

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


        private List<TempTransaction> _transactionInfoList = new List<TempTransaction>();
        public List<TempTransaction> TransactionInfoList
        {
            get
            {
                return _transactionInfoList;
            }
            set
            {
                _transactionInfoList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TransactionInfoList"));
                }
            }
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
            //TransactionInfoList.Add(transaction);

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
                transaction.ID = Guid.NewGuid().ToString();
                transaction.Amount = Math.Round(double.Parse(Money.Text), 2);
                transaction.Stakeholder = Stakeholder.Text;
                DateTime? datepicker = DatePicker.SelectedDate;
                transaction.Date = datepicker.Value.ToString();
                transaction.Note = Note.Text;
                transaction.TransactionType = Category.Name;
                TransactionInfoList.Add(transaction);

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
            Global.tempTransaction = TransactionInfoList;

          
            CategorySelect categorySelect = new CategorySelect(ColorScheme);
            categorySelect.Show();
            this.Close();
            
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
            if (Global.tempTransaction != null)
            {
                TransactionInfoList = Global.tempTransaction;
            }
            
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
                Stakeholder.Text = Global.tempStakeholder;
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

