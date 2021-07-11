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

        List<Category> incomeCategories = new List<Category>
        {
            new Category
                {
                    ID = "49",
                    Name = "Thưởng",
                    ImagePath = "Images/category_bonus.png",
                },
            new Category
                {
                    ID = "50",
                    Name = "Tiền lãi",
                    ImagePath = "Images/category_interest.png",
                },
            new Category
                {
                    ID = "51",
                    Name = "Lương",
                    ImagePath = "Images/category_salary.png",
                },
            new Category
                {
                    ID = "52",
                    Name = "Được tặng",
                    ImagePath = "Images/category_awarded.png",
                },
            new Category
                {
                    ID = "53",
                    Name = "Bán đồ",
                    ImagePath = "Images/category_sell_things.png",
                },
            new Category
                {
                    ID = "54",
                    Name = "Thu nhập khác",
                    ImagePath = "Images/category_other_income.png",
                },
        };


        

        public CategorySelect()
        {
            InitializeComponent();
            

            CategoryList.ItemsSource = expenseCategories;
        }

        private void CloseListDetailBudget_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AddTransaction add = new AddTransaction();
            if ((!check && Global.lol > 0) || check)
            {
                add.Category = expenseCategories[Global.lol];
            }
            
            add.Show();
        }

        

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.ItemsSource = expenseCategories;
        }

        private void IncomeButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.ItemsSource = incomeCategories;
        }

        private void CategorySelecttButton_Click(object sender, RoutedEventArgs e)
        {

            var temp = sender as Button;
            check = false;
            Category categoryInfo = temp.DataContext as Category;
            Global.lol = 0;
            foreach (var category in expenseCategories)
            {
                if (category.ID == categoryInfo.ID)
                {
                    //transactionType = budget.Name;
                    check = true;
                    break;
                }
                Global.lol++;
            }
            
            AddTransaction add = new AddTransaction();
            add.Category = expenseCategories[Global.lol];
            
            add.Show();
            this.Close();
        }

        
    }
}
