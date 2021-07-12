using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CategorySelect.xaml
    /// </summary>
    public partial class CategorySelect : Window
    {
        // Dùng static để lưu vị trí của category nếu bấm nút tắt
        public class Global
        {
            public static int lol;
            
        }

        static bool check = false;

        List<Category> expenseCategories = new List<Category>
            {
                new Category
                {
                    ID = "1_0",
                    Name = "Food & Drink",
                    ImagePath = "Images/category_foodndrink.png",
                },
                new Category
                {
                    ID = "1_0_0",
                    Name = "Restaurent",
                    ImagePath = "Images/category_restaurent.png",
                    Space = "          "

                },
                new Category
                {
                    ID = "1_0_1",
                    Name = "Coffee" ,
                    ImagePath = "Images/category_coffee.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "1_1",
                    Name = "Bills & Utilities" ,
                    ImagePath = "Images/category_bills_utilities.png",

                },
                new Category
                {
                    ID = "1_1_0",
                    Name = "Phone" ,
                    ImagePath = "Images/category_telephone_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_1_1",
                    Name = "Water" ,
                    ImagePath = "Images/category_water_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_1_2",
                    Name = "Electricity" ,
                    ImagePath = "Images/category_electric_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_1_3",
                    Name = "Gas" ,
                    ImagePath = "Images/category_gas_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_1_4",
                    Name = "Television" ,
                    ImagePath = "Images/category_TVbill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_1_5",
                    Name = "Internet" ,
                    ImagePath = "Images/category_internet_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_1_6",
                    Name = "Rentals" ,
                    ImagePath = "Images/category_rent.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "1_2",
                    Name = "Transport" ,
                    ImagePath = "Images/category_move.png",

                },
                new Category
                {
                    ID = "1_2_0",
                    Name = "Taxi" ,
                    ImagePath = "Images/category_taxi.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_2_1",
                    Name = "Parking" ,
                    ImagePath = "Images/category_parking.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_2_2",
                    Name = "Petrol" ,
                    ImagePath = "Images/category_petroleum.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_2_3",
                    Name = "Maintenance" ,
                    ImagePath = "Images/category_maintenance.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "1_3",
                    Name = "Shopping" ,
                    ImagePath = "Images/category_shopping.png",

                },
                new Category
                {
                    ID = "1_3_0",
                    Name = "Clothing" ,
                    ImagePath = "Images/category_clothes.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_3_1",
                    Name = "Shoes" ,
                    ImagePath = "Images/category_shoes.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_3_2",
                    Name = "Accessories" ,
                    ImagePath = "Images/category_accessories.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_3_3",
                    Name = "Electronic Device" ,
                    ImagePath = "Images/category_electronic_device.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "1_4",
                    Name = "Friends & Lovers" ,
                    ImagePath = "Images/category_friends_lovers.png",

                },




                new Category
                {
                    ID = "1_5",
                    Name = "Entertainment" ,
                    ImagePath = "Images/category_entertainment.png",

                },
                new Category
                {
                    ID = "1_5_0",
                    Name = "Movies" ,
                    ImagePath = "Images/category_movie.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_5_1",
                    Name = "Games" ,
                    ImagePath = "Images/category_game.png",
                    Space = "          "
                },



                new Category
                {
                    ID = "1_6",
                    Name = "Travel" ,
                    ImagePath = "Images/category_travel.png",

                },




                new Category
                {
                    ID = "1_7",
                    Name = "Health & Fitness" ,
                    ImagePath = "Images/category_health.png",

                },
                new Category
                {
                    ID = "1_7_0",
                    Name = "Sport" ,
                    ImagePath = "Images/category_sport.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_7_1",
                    Name = "Khám chữa bệnh" ,
                    ImagePath = "Images/category_healthcare.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_7_2",
                    Name = "Medicine" ,
                    ImagePath = "Images/category_medicine.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_7_3",
                    Name = "Personal Care" ,
                    ImagePath = "Images/caregory_personal_care.png",
                    Space = "          "
                },



                new Category
                {
                    ID = "1_8",
                    Name = "Gifts & Donations" ,
                    ImagePath = "Images/category_gifts_donations.png",

                },
                new Category
                {
                    ID = "1_8_0",
                    Name = "Wedding" ,
                    ImagePath = "Images/category_wedding.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_8_1",
                    Name = "Funeral" ,
                    ImagePath = "Images/category_funeral.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_8_2",
                    Name = "Charity" ,
                    ImagePath = "Images/category_charity.png",
                    Space = "          "
                },





                new Category
                {
                    ID = "1_9",
                    Name = "Home" ,
                    ImagePath = "Images/category_family.png",

                },
                new Category
                {
                    ID = "1_9_0",
                    Name = "Children & Babies" ,
                    ImagePath = "Images/category_children.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_9_1",
                    Name = "Home Improvement" ,
                    ImagePath = "Images/category_house_repair.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_9_2",
                    Name = "Home Sevices" ,
                    ImagePath = "Images/category_home_service.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "1_9_3",
                    Name = "Pets" ,
                    ImagePath = "Images/category_pet.png",
                    Space = "          "
                },


                new Category
                {
                    ID = "1_10",
                    Name = "Education" ,
                    ImagePath = "Images/category_education.png",

                },
                new Category
                {
                    ID = "1_10_0",
                    Name = "Books" ,
                    ImagePath = "Images/category_book.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "1_11",
                    Name = "Investment" ,
                    ImagePath = "Images/category_invest.png",

                },
                new Category
                {
                    ID = "1_12",
                    Name = "Bussiness" ,
                    ImagePath = "Images/category_business.png",

                },
                new Category
                {
                    ID = "1_13",
                    Name = "Insurance" ,
                    ImagePath = "Images/category_insurrance.png",

                },
                new Category
                {
                    ID = "1_14",
                    Name = "Fees & Charges" ,
                    ImagePath = "Images/category_cost.png",

                },
                new Category
                {
                    ID = "1_15",
                    Name = "Withdrawal" ,
                    ImagePath = "Images/category_withdrawal.png",

                },
                new Category
                {
                    ID = "1_16",
                    Name = "Others" ,
                    ImagePath = "Images/category_other_costs.png",

                },

            };

        List<Category> incomeCategories = new List<Category>
        {
            new Category
                {
                    ID = "0_0",
                    Name = "Award",
                    ImagePath = "Images/category_bonus.png",
                },
            new Category
                {
                    ID = "0_1",
                    Name = "Interest Money",
                    ImagePath = "Images/category_interest.png",
                },
            new Category
                {
                    ID = "0_2",
                    Name = "Salary",
                    ImagePath = "Images/category_salary.png",
                },
            new Category
                {
                    ID = "0_3",
                    Name = "Gifts",
                    ImagePath = "Images/category_awarded.png",
                },
            new Category
                {
                    ID = "0_4",
                    Name = "Selling",
                    ImagePath = "Images/category_sell_things.png",
                },
            new Category
                {
                    ID = "0_5",
                    Name = "Others",
                    ImagePath = "Images/category_other_income.png",
                },
        };

        List<Category> temp = new List<Category>();

        public delegate void ChooseCategoryDelegate(Category category);
        public event ChooseCategoryDelegate Handler;

        public event PropertyChangedEventHandler PropertyChanged;
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

        private Button clickedButton;

        public CategorySelect(string colorScheme)
        {
            InitializeComponent();

            ColorScheme = colorScheme;
            CategoryList.ItemsSource = temp;
            DataContext = this;
        }

        private void CloseListDetailBudget_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AddTransaction add = new AddTransaction(ColorScheme);
            if ((!check && Global.lol > 0) || check)
            {
                add.Category = temp[Global.lol];
            }
            
            add.Show();
        }

        //Thay đổi trạng thái của nút
        private void ChangeButtonStatus(TextBlock dash, TextBlock textBlock, bool isSelected)
        {

            //Trường hợp nút được chọn
            if (isSelected)
            {

                textBlock.Foreground = ChangeHexToBrushColor(ColorScheme);
                textBlock.FontSize = 19;
                dash.Background = ChangeHexToBrushColor(ColorScheme);

            }
            else //Trường hợp nút không được chọn
            {

                textBlock.Foreground = ChangeHexToBrushColor("#757575");
                textBlock.FontSize = 15;
                dash.Background = Brushes.White;

            }

        }

        private SolidColorBrush ChangeHexToBrushColor(string hex)
        {

            var color = (SolidColorBrush)new BrushConverter().ConvertFromString(hex);
            return color;

        }

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            temp = expenseCategories;
            CategoryList.ItemsSource = temp;          
            //clickedButton = ExpensesButton;
            searchComboBox.ItemsSource = temp;

            //Chuyển nút hiện tại sang trạng thái không được chọn
            ChangeButtonStatus(IncomeDash, IncomeTextBlock, false);
            //Chuyển nút tiếp theo sang trạng thái được chọn
            ChangeButtonStatus(ExpensesDash, ExpensesTextBlock, true);

        }

        private void IncomeButton_Click(object sender, RoutedEventArgs e)
        {
            temp = incomeCategories;
            CategoryList.ItemsSource = temp;
            searchComboBox.ItemsSource = temp;

            //Chuyển nút hiện tại sang trạng thái không được chọn
            ChangeButtonStatus(IncomeDash, IncomeTextBlock, true);
            //Chuyển nút tiếp theo sang trạng thái được chọn
            ChangeButtonStatus(ExpensesDash, ExpensesTextBlock, false);
        }

        private void CategorySelecttButton_Click(object sender, RoutedEventArgs e)
        {
            
            var selected = sender as Button;
            check = false;
            Category categoryInfo = selected.DataContext as Category;
            Global.lol = 0;
            foreach (var category in temp)
            {
                if (category.ID == categoryInfo.ID)
                {
                    //transactionType = budget.Name;
                    check = true;
                    break;
                }
                Global.lol++;
            }
            
            //AddTransaction add = new AddTransaction(ColorScheme);
            //add.Category = temp[Global.lol];

            if (Handler != null)
            {

                Handler(temp[Global.lol]);

            }
            
            //add.Show();
            this.Close();
        }

        /*tim kiem*/
        private string ConvertToUnSign(string input)
        {
            if (input != null)
            {
                input = input.Trim();
                for (int i = 0x20; i < 0x30; i++)
                {
                    input = input.Replace(((char)i).ToString(), " ");
                }
                Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
                string str = input.Normalize(NormalizationForm.FormD);
                string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
                while (str2.IndexOf("?") >= 0)
                {
                    str2 = str2.Remove(str2.IndexOf("?"), 1);
                }
                return str2;
            }
            else
            {
                var res = "";
                return res;
            }
        }

        private void DeleteTextInSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "";
            searchTextBox.Focus();
        }

        private void DeleteTextInSearchButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DeleteTextInSearchButton_Click(null, null);
            }
            else
            {
                //Do nothing
            }
        }

        private void searchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                searchComboBox.Focus();
                searchComboBox.SelectedIndex = 0;
                searchComboBox.IsDropDownOpen = true;
            }
            if (e.Key == Key.Escape)
            {
                searchComboBox.IsDropDownOpen = false;

            }
        }

        private void searchTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            

            if (e.Text != "\u001b")  //khác escapes
            {
                searchComboBox.IsDropDownOpen = true;
            }
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                string fullText = ConvertToUnSign(searchTextBox.Text.Insert(searchTextBox.CaretIndex, (e.Text)));
                searchComboBox.ItemsSource = temp.Where(s => ConvertToUnSign(s.Name).IndexOf(fullText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                if (searchComboBox.Items.Count == 0)
                {
                    SearchNotificationComboBox.IsDropDownOpen = true;
                    searchComboBox.IsDropDownOpen = false;
                }
            }
            else if (!string.IsNullOrEmpty(e.Text))
            {
                searchComboBox.ItemsSource = temp.Where(s => ConvertToUnSign(s.Name).IndexOf(ConvertToUnSign(e.Text),
                    StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
            }
            else
            {
                searchComboBox.ItemsSource = temp;
            }
        }

        private void PreviewKeyUp_EnhanceTextBoxSearch(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                

                searchComboBox.IsDropDownOpen = true;

                if (!string.IsNullOrEmpty(searchTextBox.Text))
                {
                    searchComboBox.ItemsSource = temp.Where(s => ConvertToUnSign(s.Name).IndexOf(ConvertToUnSign(searchTextBox.Text), StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                }
                else
                {
                    searchComboBox.ItemsSource = temp;
                }
            }
        }

        private void Pasting_EnhanceTextSearch(object sender, DataObjectPastingEventArgs e)
        {
            searchComboBox.IsDropDownOpen = true;

            string pastedText = (string)e.DataObject.GetData(typeof(string));
            string fullText = searchTextBox.Text.Insert(searchTextBox.CaretIndex, (pastedText));

            if (!string.IsNullOrEmpty(fullText))
            {
                searchComboBox.ItemsSource = temp.Where(s => ConvertToUnSign(s.Name).IndexOf(ConvertToUnSign(fullText), StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                if (searchComboBox.Items.Count == 0)
                {
                    SearchNotificationComboBox.IsDropDownOpen = true;
                    searchComboBox.IsDropDownOpen = false;
                }
            }
            else
            {
                searchComboBox.ItemsSource = temp;
            }
        }

        private void SearchTripButton_Click(object sender, RoutedEventArgs e)
        {
            CategorySelecttButton_Click(sender, null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            clickedButton = ExpensesButton;
            
            if (clickedButton == ExpensesButton)
            {
                temp = expenseCategories;
            }
            else if (clickedButton == IncomeButton)
            {
                temp = incomeCategories;
            }

            searchComboBox.ItemsSource = temp;
            CategoryList.ItemsSource = temp;
        }

        private void searchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = searchComboBox.SelectedIndex;
            if (index >= 0)
            {
                var selectedFood = searchComboBox.SelectedItem as Category;
                string textSelected = selectedFood.Name;
                searchTextBox.Text = textSelected;
            }
        }

        private void searchComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                Button button = new Button();
                button.DataContext = searchComboBox.SelectedItem as Category;
                button.Content = "button";
                SearchTripButton_Click(button, null);
            }
        }

        

        //private void searchTextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    searchComboBox.Focus();
        //    //searchComboBox.SelectedIndex = 0;
        //    searchComboBox.IsDropDownOpen = true;
            
            
        //}
    }
}
