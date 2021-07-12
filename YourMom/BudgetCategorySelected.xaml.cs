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
    /// Interaction logic for BudgetCategorySelected.xaml
    /// </summary>
    public partial class BudgetCategorySelected : Window
    {
        public BudgetCategorySelected(string colorScheme)
        {
            InitializeComponent();
            CategoryList.ItemsSource = temp;
            ColorScheme = colorScheme;
            DataContext = this;
        }
        // Dùng static để lưu vị trí của category nếu bấm nút tắt
        public class Global
        {

            public static int lul;

        }

        static bool check = false;

        List<Category> expenseCategories = new List<Category>
            {
                new Category
                {
                    ID = "1",
                    Name = "Ăn uống",
                    ImagePath = "Images/category_foodndrink.png",
                },
                new Category
                {
                    ID = "2",
                    Name = "Nhà hàng",
                    ImagePath = "Images/category_restaurent.png",
                    Space = "          "

                },
                new Category
                {
                    ID = "3",
                    Name = "Cà phê" ,
                    ImagePath = "Images/category_coffee.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "4",
                    Name = "Hóa đơn và tiện ích" ,
                    ImagePath = "Images/category_bills_utilities.png",

                },
                new Category
                {
                    ID = "5",
                    Name = "Hóa đơn điện thoại" ,
                    ImagePath = "Images/category_telephone_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "6",
                    Name = "Hóa đơn nước" ,
                    ImagePath = "Images/category_water_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "7",
                    Name = "Hóa đơn điện" ,
                    ImagePath = "Images/category_electric_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "8",
                    Name = "Hóa đơn ga" ,
                    ImagePath = "Images/category_gas_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "9",
                    Name = "Hóa đơn TV" ,
                    ImagePath = "Images/category_TVbill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "10",
                    Name = "Hóa đơn Internet" ,
                    ImagePath = "Images/category_internet_bill.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "11",
                    Name = "Thuê nhà" ,
                    ImagePath = "Images/category_rent.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "12",
                    Name = "Di chuyển" ,
                    ImagePath = "Images/category_move.png",

                },
                new Category
                {
                    ID = "13",
                    Name = "Taxi" ,
                    ImagePath = "Images/category_taxi.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "14",
                    Name = "Gửi xe" ,
                    ImagePath = "Images/category_parking.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "15",
                    Name = "Xăng dầu" ,
                    ImagePath = "Images/category_petroleum.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "16",
                    Name = "Bảo dưỡng" ,
                    ImagePath = "Images/category_maintenance.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "17",
                    Name = "Mua sắm" ,
                    ImagePath = "Images/category_shopping.png",

                },
                new Category
                {
                    ID = "18",
                    Name = "Quần áo" ,
                    ImagePath = "Images/category_clothes.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "19",
                    Name = "Giày dép" ,
                    ImagePath = "Images/category_shoes.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "20",
                    Name = "Phụ kiện" ,
                    ImagePath = "Images/category_accessories.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "21",
                    Name = "Thiết bị điện tử" ,
                    ImagePath = "Images/category_electronic_device.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "22",
                    Name = "Bạn bè và người yêu" ,
                    ImagePath = "Images/category_friends_lovers.png",

                },




                new Category
                {
                    ID = "23",
                    Name = "Giải trí" ,
                    ImagePath = "Images/category_entertainment.png",

                },
                new Category
                {
                    ID = "24",
                    Name = "Phim ảnh" ,
                    ImagePath = "Images/category_movie.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "25",
                    Name = "Trò chơi" ,
                    ImagePath = "Images/category_game.png",
                    Space = "          "
                },



                new Category
                {
                    ID = "26",
                    Name = "Du lịch" ,
                    ImagePath = "Images/category_travel.png",

                },




                new Category
                {
                    ID = "27",
                    Name = "Sức khỏe" ,
                    ImagePath = "Images/category_health.png",

                },
                new Category
                {
                    ID = "28",
                    Name = "Thể thao" ,
                    ImagePath = "Images/category_sport.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "29",
                    Name = "Khám chữa bệnh" ,
                    ImagePath = "Images/category_healthcare.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "30",
                    Name = "Thuốc" ,
                    ImagePath = "Images/category_medicine.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "31",
                    Name = "Chăm sóc cá nhân" ,
                    ImagePath = "Images/caregory_personal_care.png",
                    Space = "          "
                },



                new Category
                {
                    ID = "32",
                    Name = "Quà tặng và quyên góp" ,
                    ImagePath = "Images/category_gifts_donations.png",

                },
                new Category
                {
                    ID = "33",
                    Name = "Cưới hỏi" ,
                    ImagePath = "Images/category_wedding.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "34",
                    Name = "Tang lễ " ,
                    ImagePath = "Images/category_funeral.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "35",
                    Name = "Từ thiện" ,
                    ImagePath = "Images/category_charity.png",
                    Space = "          "
                },





                new Category
                {
                    ID = "36",
                    Name = "Gia đình" ,
                    ImagePath = "Images/category_family.png",

                },
                new Category
                {
                    ID = "37",
                    Name = "Con cái" ,
                    ImagePath = "Images/category_children.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "38",
                    Name = "Sửa chữa nhà cửa" ,
                    ImagePath = "Images/category_house_repair.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "39",
                    Name = "Dịch vụ gia đình" ,
                    ImagePath = "Images/category_home_service.png",
                    Space = "          "
                },
                new Category
                {
                    ID = "40",
                    Name = "Vật nuôi" ,
                    ImagePath = "Images/category_pet.png",
                    Space = "          "
                },


                new Category
                {
                    ID = "41",
                    Name = "Giáo dục" ,
                    ImagePath = "Images/category_education.png",

                },
                new Category
                {
                    ID = "42",
                    Name = "Sách" ,
                    ImagePath = "Images/category_book.png",
                    Space = "          "
                },




                new Category
                {
                    ID = "43",
                    Name = "Đầu tư" ,
                    ImagePath = "Images/category_invest.png",

                },
                new Category
                {
                    ID = "44",
                    Name = "Kinh doanh" ,
                    ImagePath = "Images/category_business.png",

                },
                new Category
                {
                    ID = "45",
                    Name = "Bảo hiểm" ,
                    ImagePath = "Images/category_insurrance.png",

                },
                new Category
                {
                    ID = "46",
                    Name = "Chi phí" ,
                    ImagePath = "Images/category_cost.png",

                },
                new Category
                {
                    ID = "47",
                    Name = "Rút tiền" ,
                    ImagePath = "Images/category_withdrawal.png",

                },
                new Category
                {
                    ID = "48",
                    Name = "Các chi phí khác" ,
                    ImagePath = "Images/category_other_costs.png",

                },





            };

        List<Category> debtLoanCategories = new List<Category>
        {
            new Category
                {
                    ID = "55",
                    Name = "Cho vay",
                    ImagePath = "Images/category_loan.png",
                },
            new Category
                {
                    ID = "56",
                    Name = "Trả nợ",
                    ImagePath = "Images/category_pay.png",
                },

        };

        List<Category> temp = new List<Category>();


        private Button clickedButton;
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

        private void CloseListDetailBudget_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

            AddBudget add = new AddBudget(ColorScheme);
            if ((!check && Global.lul > 0) || check)
            {
                add.Category = temp[Global.lul];
            }

            add.Show();

        }



        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {

            temp = expenseCategories;
            CategoryList.ItemsSource = temp;
            //clickedButton = ExpensesButton;
            searchComboBox.ItemsSource = temp;

            //Chuyển nút hiện tại sang trạng thái không được chọn
            ChangeButtonStatus(RevenueDash, RevenueTextBlock, false);
            //Chuyển nút tiếp theo sang trạng thái được chọn
            ChangeButtonStatus(ExpensesDash, ExpensesTextBlock, true);

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

        private void CategorySelecttButton_Click(object sender, RoutedEventArgs e)
        {

            var selected = sender as Button;
            check = false;
            Category categoryInfo = selected.DataContext as Category;
            Global.lul = 0;
            foreach (var category in temp)
            {
                if (category.ID == categoryInfo.ID)
                {
                    //transactionType = budget.Name;
                    check = true;
                    break;
                }
                Global.lul++;
            }

            AddBudget add = new AddBudget(ColorScheme);
            add.Category = temp[Global.lul];

            add.Show();
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

            temp = expenseCategories;

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

        private void Debt_Loan_Button_Click(object sender, RoutedEventArgs e)
        {
            temp = debtLoanCategories;
            CategoryList.ItemsSource = temp;
            searchComboBox.ItemsSource = temp;

            //Chuyển nút hiện tại sang trạng thái không được chọn
            ChangeButtonStatus(RevenueDash, RevenueTextBlock, true);
            //Chuyển nút tiếp theo sang trạng thái được chọn
            ChangeButtonStatus(ExpensesDash, ExpensesTextBlock, false);
        }

    }
}


