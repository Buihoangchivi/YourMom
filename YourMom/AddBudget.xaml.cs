using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public Budget budget = new Budget();

        public class Global
        {
            public static string tempMoneyFund;
            
            public static DateTime tempStartingDate;
            public static DateTime tempEndDate;
            public static string tempNote;
            public static string tempBudgetType;
            public static string tempColorScheme;
            //public static List<TempTransaction> tempTransaction;
        }


        public AddBudget(string colorScheme)
        {
            
            InitializeComponent();
            ColorScheme = colorScheme;
            this.DataContext = this;
            
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

        

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var noti = MessageBox.Show("Are you really want to cancel?",
                    "Notification",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
            if (noti == MessageBoxResult.Yes)
            {
                BudgetCategorySelected.Global.lul = 0;
                this.Close();
            }
            else
            {
                // Do nothing
            }
        }

        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void Money_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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

        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Money.Text == "" || StartingDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null || Category == null)
            {
                var noti = MessageBox.Show("Please enter all required information.",
                    "Notification",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }
            else if (StartingDatePicker.SelectedDate > EndDatePicker.SelectedDate)
            {
                var noti = MessageBox.Show("Time range illegal.",
                    "Notification",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                budget.ID = "";
                budget.MoneyFund = Math.Round(double.Parse(Money.Text), 2);                
                //DateTime? datepicker = DatePicker.SelectedDate;
                //transaction.Date = datepicker.Value.ToString();

                budget.StartingDate = (DateTime)StartingDatePicker.SelectedDate;
                budget.EndDate = (DateTime)EndDatePicker.SelectedDate;
                budget.Note = Note.Text;
                budget.Name = Category.Name;
                //TransactionInfoList.Add(transaction);

                BudgetCategorySelected.Global.lul = 0;
                this.Close();
            }
        }

        private void SelectCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            // LƯu lại các thông tin đã nhập
            Global.tempMoneyFund = Money.Text;           
            if (StartingDatePicker.SelectedDate != null )
            {
                Global.tempStartingDate = StartingDatePicker.SelectedDate.Value;
            }
            if (EndDatePicker.SelectedDate != null)
            {
                Global.tempEndDate = EndDatePicker.SelectedDate.Value;
            }

            Global.tempNote = Note.Text;
            Global.tempColorScheme = _colorScheme;

            BudgetCategorySelected categorySelect = new BudgetCategorySelected(ColorScheme);
            categorySelect.Show();
            this.Close();
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


            if (Global.tempMoneyFund != null && Global.tempMoneyFund != "")
            {
                Money.Text = Global.tempMoneyFund;
            }

            

            if (Global.tempStartingDate == default(DateTime))
            {
                DateTime? myTime = null;

                StartingDatePicker.SelectedDate = myTime;
            }
            else
            {
                StartingDatePicker.SelectedDate = Global.tempStartingDate;
            }

            if (Global.tempEndDate == default(DateTime))
            {
                DateTime? myTime = null;

                EndDatePicker.SelectedDate = myTime;
            }
            else
            {
                EndDatePicker.SelectedDate = Global.tempEndDate;
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
            //SaveButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            //CancelButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            //Category = _category;
        }

        
    }
}
