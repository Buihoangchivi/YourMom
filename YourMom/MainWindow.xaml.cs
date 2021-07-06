using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;


namespace YourMom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {

        private List<Transaction> transactionList;
        private Dictionary<string, Category> categoryList = new Dictionary<string, Category>();
        private Budget budgetInfo;

        List<DetailCategory> incomeList = new List<DetailCategory>();
        List<DetailCategory> expenseList = new List<DetailCategory>();
        List<DetailCategory> debtList = new List<DetailCategory>();
        List<DetailCategory> loanList = new List<DetailCategory>();

        ObservableCollection<TransactionList> transactionLists = new ObservableCollection<TransactionList>
        {
            new TransactionList()
            {
                Transactions = new ObservableCollection<Transaction>()
                {
                    new Transaction
                    {
                        ID = "gd1",
                        Amount = 1000000,
                        Date = new DateTime(2021, 7, 3)
                    },
                    new Transaction
                    {
                        ID = "gd2",
                        Amount = 200000,
                        Date = new DateTime(2021, 7, 2)
                    },
                    new Transaction
                    {
                        ID = "gd2",
                        Amount = 200000,
                        Date = new DateTime(2021, 7, 4)
                    },
                    new Transaction
                    {
                        ID = "gd2",
                        Amount = 2000000,
                        Date = new DateTime(2021, 7, 4)
                    },
                    new Transaction
                    {
                        ID = "gd2",
                        Amount = 200000,
                        Date = new DateTime(2021, 7, 30)
                    },
                },


                ImagePath = "Images/category_foodndrink.png",
                TransactionType = "Eating"

            },
            new TransactionList()
            {
                Transactions = new ObservableCollection<Transaction>()
                {
                    new Transaction
                    {
                        ID = "gd3",
                        Amount = 40000,
                        Date = new DateTime(2021, 1, 3)
                    },
                    new Transaction
                    {
                        ID = "gd4",
                        Amount = 50000,
                        Date = new DateTime(2021, 1, 4)
                    },
                },

                ImagePath = "Images/category_foodndrink.png",
                TransactionType = "Eating"
            },
            new TransactionList()
            {
                Transactions = new ObservableCollection<Transaction>()
                {
                    new Transaction
                    {
                        ID = "gd5",
                        Amount = 40000,
                        Date = new DateTime(2021, 1, 3)
                    },
                    new Transaction
                    {
                        ID = "gd6",
                        Amount = 50000,
                        Date = new DateTime(2021, 1, 4)
                    },
                },

                ImagePath = "Images/category_foodndrink.png",
                TransactionType = "Eating"
            },
            new TransactionList()
            {
                Transactions = new ObservableCollection<Transaction>()
                {
                    new Transaction
                    {
                        ID = "gd7",
                        Amount = 40000,
                        Date = new DateTime(2021, 1, 3)
                    },
                    new Transaction
                    {
                        ID = "gd8",
                        Amount = 50000,
                        Date = new DateTime(2021, 1, 4)
                    },
                    new Transaction
                    {
                        ID = "gd9",
                        Amount = 50000,
                        Date = new DateTime(2021, 1, 4)
                    },
                },

                ImagePath = "Images/category_foodndrink.png",
                TransactionType = "Ăn uống"
            }
            };

        List<Budget> budgetList = new List<Budget>
        {
            new Budget
            {
                ID = "1",
                ImagePath = "Images/category_foodndrink.png",
                Name = "Eating",
                MoneyFund = 9000000,

                StartingDate = new DateTime(2021,6,1).ToShortDateString(),
                EndDate = new DateTime(2021,6,30).ToShortDateString()
            },

            new Budget
            {
                ID = "2",
                ImagePath = "Images/category_foodndrink.png",
                Name = "Mua sắm",
                MoneyFund = 5000000,

                StartingDate = new DateTime(2021,6,1).ToShortDateString(),
                EndDate = new DateTime(2021,6,30).ToShortDateString()
            },

            new Budget
            {
                ID = "3",
                ImagePath = "Images/category_foodndrink.png",
                Name = "Đi chơi",
                MoneyFund = 5000000,

                StartingDate = new DateTime(2021,6,1).ToShortDateString(),
                EndDate = new DateTime(2021,6,27).ToShortDateString()
            },
            new Budget
            {
                ID = "4",
                ImagePath = "Images/category_foodndrink.png",
                Name = "Eating",
                MoneyFund = 5000000,
                
                StartingDate = new DateTime(2021,7,1).ToShortDateString(),
                EndDate =new DateTime(2021,7,31).ToShortDateString()
            }
        };

        // Danh sách ngân sách đang sử dụng
        List<Budget> runningBudgetList = new List<Budget> { };
        // Danh sách ngân sách đã quá hạn
        List<Budget> finishedBudgetList = new List<Budget> { };

        public MainWindow()
        {

            InitializeComponent();

            //Đọc dữ liệu
            ReadData();

            //Khởi tạo dữ liệu
            InitializeData();

            double temp;


            // Hàm xử lý ngân sách
            for (int i = 0; i < budgetList.Count; i++)
            {

                // Lấy số tiền đã chi tiêu cho ngân sách thông qua cách giao dịch(nên để vào lúc khởi tạo một ngân sách mới!!!)
                var end = DateTime.Parse(budgetList[i].EndDate);
                var start = DateTime.Parse(budgetList[i].StartingDate);
                double moneyTotal = 0;
                for (int j = 0; j < transactionLists.Count; j++)
                {
                    if (budgetList[i].Name == transactionLists[j].TransactionType)
                    {
                        for (int k = 0; k < transactionLists[j].Transactions.Count; k++)
                        {
                            if (transactionLists[j].Transactions[k].Date < end && transactionLists[j].Transactions[k].Date > start)
                            {
                                moneyTotal += transactionLists[j].Transactions[k].Amount;
                            }
                        }
                    }
                    
                }
                budgetList[i].SpentMoney = moneyTotal;

                // lấy số ngày còn lại trong ngân sách			

                DateTime currentdate = DateTime.Now;

                //Lấy thông tin ngày tháng kết thúc
                var endDate = DateTime.Parse(budgetList[i].EndDate);
                // Số ngày còn lại của ngân sách
                TimeSpan time = endDate - currentdate;
                int dayLeft = time.Days < 0 ? 0 : time.Days;

                budgetList[i].DaysLeft = dayLeft > 0 ? $"{time.Days} days left" : "Finished";              


                // số tiền dư còn lại cho ngân sách
                budgetList[i].Balance = budgetList[i].MoneyFund - budgetList[i].SpentMoney;



                // lấy tiến độ hiện tại, làm tròn 2 số sau dấu phẩy
                temp = Math.Round((double)(budgetList[i].MoneyFund - budgetList[i].Balance) / budgetList[i].MoneyFund * 100, 2);
                budgetList[i].Progress = temp;


                // định dạng lại ngày
                //convert = DateTime.Parse(budgetList[i].StartingDate);
                //budgetList[i].StartingDate = convert.ToString("dd-MM-yyyy");
                //convert = DateTime.Parse(budgetList[i].EndDate);
                //budgetList[i].EndDate = convert.ToString("dd-MM-yyyy");

                // số tiền nên chi hàng ngày
                budgetList[i].ShouldSpending_DayMoney = time.Days >= 0 ? Math.Round(budgetList[i].Balance / dayLeft, 2) : 0;

                // số tiền thực tế chi hàng ngày
                //DateTime startingdate = Convert.ToDateTime(budgetList[i].StartingDate);

                //Lấy thông tin ngày tháng kết thúc
                var startingDate = DateTime.Parse(budgetList[i].StartingDate);

                budgetList[i].RealitySpending_DayMoney = Math.Round(budgetList[i].SpentMoney / ((currentdate - startingDate).Days + 1), 2);

                // số tiền dự kiến chi tiêu
                budgetList[i].ExpectedSpendingMoney = budgetList[i].SpentMoney + budgetList[i].RealitySpending_DayMoney * dayLeft;

                if (time.Days < 0)
                {
                    finishedBudgetList.Add(budgetList[i]);

                }
                else
                {
                    runningBudgetList.Add(budgetList[i]);

                }


            }
            BudgetList.ItemsSource = runningBudgetList;

            // Hàm xử lý giao dịch
            for (int i = 0; i < transactionLists.Count; i++)
            {
                transactionLists[i].NumberOfTransactions = transactionLists[i].Transactions.Count;
                for (int j = 0; j < transactionLists[i].Transactions.Count; j++)
                {
                    transactionLists[i].TotalMoney += transactionLists[i].Transactions[j].Amount;
                }
            }

            TransactionList.ItemsSource = transactionLists;

        }

        List<DetailCategory> detailCategoryList = new List<DetailCategory>
            {
                new DetailCategory
                {
                    ID = "0",
                    Name = "Eating",
                    ImagePath = "Images\\category_foodndrink.png",
                    Amount = 150320
                },
                new DetailCategory
                {
                    ID = "1",
                    Name = "Shopping",
                    ImagePath = "Images\\category_shopping.png",
                    Amount = 342420
                },
                new DetailCategory
                {
                    ID = "2",
                    Name = "Bills",
                    ImagePath = "Images\\category_bills.png",
                    Amount = 60230
                },
                new DetailCategory
                {
                    ID = "3",
                    Name = "Entertainment",
                    ImagePath = "Images\\category_entertainment.png",
                    Amount = 24232
                },
                new DetailCategory
                {
                    ID = "0",
                    Name = "Eating",
                    ImagePath = "Images\\category_foodndrink.png",
                    Amount = 5430
                },
                new DetailCategory
                {
                    ID = "1",
                    Name = "Shopping",
                    ImagePath = "Images\\category_shopping.png",
                    Amount = 345325
                },
                new DetailCategory
                {
                    ID = "2",
                    Name = "Bills",
                    ImagePath = "Images\\category_bills.png",
                    Amount = 454243
                },
                new DetailCategory
                {
                    ID = "3",
                    Name = "Entertainment",
                    ImagePath = "Images\\category_entertainment.png",
                    Amount = 324523
                }
            };

        List<DetailCategory> detailCategoryList1 = new List<DetailCategory>
            {
                new DetailCategory
                {
                    ID = "10",
                    Name = "Clothes",
                    ImagePath = "Images\\category_clothes.png",
                    Amount = 150000
                },
                new DetailCategory
                {
                    ID = "11",
                    Name = "Shoes",
                    ImagePath = "Images\\category_shoes.png",
                    Amount = 300000
                },
                new DetailCategory
                {
                    ID = "12",
                    Name = "Accessories",
                    ImagePath = "Images\\category_accessories.png",
                    Amount = 760000
                },
                new DetailCategory
                {
                    ID = "13",
                    Name = "Electronic Device",
                    ImagePath = "Images\\category_electronic_device.png",
                    Amount = 100000
                }
            };

        List<DetailCategory> detailCategoryList2 = new List<DetailCategory>
            {
                new DetailCategory
                {
                    ID = "10",
                    Name = "January",
                    ImagePath = "Images\\january.png",
                    Amount = -1227.239723
                },
                new DetailCategory
                {
                    ID = "11",
                    Name = "February",
                    ImagePath = "Images\\february.png",
                    Amount = 25343
                },
                new DetailCategory
                {
                    ID = "12",
                    Name = "March",
                    ImagePath = "Images\\march.png",
                    Amount = 45536
                },
                new DetailCategory
                {
                    ID = "13",
                    Name = "April",
                    ImagePath = "Images\\april.png",
                    Amount = 23123
                },
                new DetailCategory
                {
                    ID = "10",
                    Name = "May",
                    ImagePath = "Images\\may.png",
                    Amount = 11472
                },
                new DetailCategory
                {
                    ID = "11",
                    Name = "June",
                    ImagePath = "Images\\june.png",
                    Amount = 45443
                },
                new DetailCategory
                {
                    ID = "12",
                    Name = "July",
                    ImagePath = "Images\\july.png",
                    Amount = 34535
                },
                new DetailCategory
                {
                    ID = "13",
                    Name = "August",
                    ImagePath = "Images\\august.png",
                    Amount = 23284
                },
                new DetailCategory
                {
                    ID = "10",
                    Name = "September",
                    ImagePath = "Images\\september.png",
                    Amount = 24024
                },
                new DetailCategory
                {
                    ID = "11",
                    Name = "October",
                    ImagePath = "Images\\october.png",
                    Amount = 27257
                },
                new DetailCategory
                {
                    ID = "12",
                    Name = "November",
                    ImagePath = "Images\\november.png",
                    Amount = 57567
                },
                new DetailCategory
                {
                    ID = "13",
                    Name = "December",
                    ImagePath = "Images\\december.png",
                    Amount = 45682
                }
            };

        Stack<DetailInfomation> detailStack = new Stack<DetailInfomation>();

        private void InitializeData()
        {

            var incomePairs = new Dictionary<string, double>();
            var expensePairs = new Dictionary<string, double>();
            var debtPairs = new Dictionary<string, double>();
            var loanPairs = new Dictionary<string, double>();

            //Tính tổng 4 nhóm giao dịch
            foreach (Transaction transaction in transactionList)
            {

                var type = transaction.TransactionType;
                switch (type[0] - '0')
                {

                    case 0: //Thu nhập
                        AddDataIntoDictionary(incomePairs, type, transaction.Amount, true);
                        break;
                    case 1: //Chi tiêu
                        AddDataIntoDictionary(expensePairs, type, transaction.Amount, true);
                        break;
                    case 2: //Đi vay
                        AddDataIntoDictionary(debtPairs, type, transaction.Amount, true);
                        break;
                    case 3: //Cho vay
                        AddDataIntoDictionary(loanPairs, type, transaction.Amount, true);
                        break;

                }

            }

            //Chuyển dữ liệu 4 nhóm giao dịch vào danh sách
            AddDataIntoList(incomePairs, incomeList);
            AddDataIntoList(expensePairs, expenseList);
            AddDataIntoList(debtPairs, debtList);
            AddDataIntoList(loanPairs, loanList);

        }

        //Hàm thêm dữ liệu vào từ điển
        private void AddDataIntoDictionary(Dictionary<string, double> valuePairs, string str, double amount, bool getOnlyParent)
        {

            string[] componentArray = GetTransactionType(str);
            string key = str;

            //Loại giao dịch con được cộng gộp vào loại giao dịch cha
            if (componentArray.Length == 3 && getOnlyParent == true)
            {

                key = $"{componentArray[0]}_{componentArray[1]}";

            }

            //Nếu đã tồn tại khóa thì tăng lượng tiền của khóa
            if (valuePairs.ContainsKey(key))
            {

                valuePairs[key] += amount;

            }
            else //khởi tạo khóa mới với lượng tiền đầu vào
            {

                valuePairs.Add(key, amount);

            }

        }

        //Hàm chuyển dữ liệu từ từ điển sang danh sách các loại giao dịch chung nhóm
        private void AddDataIntoList(Dictionary<string, double> valuePairs, List<DetailCategory> list)
        {

            foreach (var pair in valuePairs)
            {

                var key = pair.Key;

                //Khởi tạo và thêm dữ liệu vào danh sách
                list.Add(new DetailCategory
                {
                    Amount = pair.Value,
                    ID = key,
                    ImagePath = categoryList[key].ImagePath,
                    Name = categoryList[key].Name
                });

            }

        }

        //Đọc dữ liệu từ file xml
        private void ReadData()
        {
            List<TempTransaction> tempTransactionList;
            // Đọc dữ liệu các giao dịch từ data
            XmlSerializer xs = new XmlSerializer(typeof(List<TempTransaction>));
            try
            {
                using (var reader = new StreamReader(@"Data\Transaction.xml"))
                {
                    tempTransactionList = (List<TempTransaction>)xs.Deserialize(reader);
                }
            }
            catch
            {
                tempTransactionList = new List<TempTransaction>();
            }
            
            //Sao chép dữ liệu sang danh sách giao dịch sử dụng kiểu Datetime
            transactionList = new List<Transaction>();

            foreach (var transaction in tempTransactionList)
            {

                transactionList.Add(new Transaction
                {
                    Amount = transaction.Amount,
                    Date = DateTime.Parse(transaction.Date),
                    ID = transaction.ID,
                    Note = transaction.Note,
                    Stakeholder = transaction.Stakeholder,
                    TransactionType = transaction.TransactionType
                });

            }

            // Đọc dữ liệu các loại giao dịch từ data
            List<Category> categories;
            xs = new XmlSerializer(typeof(List<Category>));
            try
            {
                using (var reader = new StreamReader(@"Data\Category.xml"))
                {
                    categories = (List<Category>)xs.Deserialize(reader);
                }
            }
            catch
            {
                categories = new List<Category>();
            }

            foreach (var category in categories)
            {

                categoryList.Add(category.ID, category);

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //Tổng tiền và số dư
            double sum, left;
            //Định dạng lại số tiền ở dạng chuỗi và truyền vào màn hình
            Modal.MoneyConverter moneyConverter = new Modal.MoneyConverter();

            //Tính tổng số tiền nợ người khác
            sum = SumComponent(debtList);
            //Hiển thị số tiền đó vào khung tiền nợ
            DebtTextBlock.Text = (string)moneyConverter.Convert(sum, null, null, null);

            //Tính toán số dư
            if (debtList.Count == 0)
            {

                left = 0;

            }
            else
            {

                left = debtList[0].Amount;

                if (debtList.Count == 2)
                {

                    left -= debtList[1].Amount;

                }

            }

            //Hiển thị số tiền đó vào khung tiền nợ
            var money = (string)moneyConverter.Convert(left, null, null, null);
            DebtLeftTextBlock.Text = $"{money} left";

            //Tính tổng số tiền cho người khác vay
            sum = SumComponent(loanList);
            //Hiển thị số tiền đó vào khung tiền nợ
            LoanTextBlock.Text = (string)moneyConverter.Convert(sum, null, null, null);

            //Tính toán số dư
            if (loanList.Count == 0)
            {

                left = 0;

            }
            else
            {
                left = loanList[0].Amount;

                if (loanList.Count == 2)
                {

                    left -= loanList[1].Amount;

                }

            }

            //Hiển thị số tiền đó vào khung tiền nợ
            money = (string)moneyConverter.Convert(left, null, null, null);
            LoanLeftTextBlock.Text = $"{money} left";

        }

        //Tách chuỗi id thành mảng các id thành phần
        private string[] GetTransactionType(string id)
        {

            char[] separator = { '_' };

            string[] strlist = id.Split(separator);

            return strlist;

        }

        //---------------------------------------- Các hàm xử lý sự kiện --------------------------------------------//



        //Cài đặt để có thể di chuyển cửa sổ khi nhấn giữ chuột và kéo Title Bar
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.DockPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }


        //---------------------------------------- Xử lý cửa sổ --------------------------------------------//

        //Cài đặt nút đóng cửa sổ
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            /*SaveListFood();
			SaveListDish();
			var config = ConfigurationManager.OpenExeConfiguration(
				ConfigurationUserLevel.None);
			config.AppSettings.Settings["ColorScheme"].Value = ColorScheme;
			config.Save(ConfigurationSaveMode.Minimal);*/
            Application.Current.Shutdown();

        }

        //Cài đặt nút ẩn cửa sổ
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TransactionsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BudgetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DebtsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseDetailButton_Click(object sender, RoutedEventArgs e)
        {

            //Lấy thông tin khung hình chi tiết hiện tại ra khỏi stack
            detailStack.Pop();

            //Nếu stack rỗng thì quay về màn hình báo cáo chung
            if (detailStack.Count == 0)
            {

                //Ẩn báo cáo chi tiết
                DetailReportGrid.Visibility = Visibility.Collapsed;

                //Phóng to chiều rộng của khung báo cáo chung
                GeneralReportGrid.Width = 600;

                //Phóng to kích thước của 2 biểu đồ hình bánh thể hiện thu chi
                IncomeReportChart.Width = 250;
                IncomeReportChart.Height = 300;
                ExpenseReportChart.Width = 250;
                ExpenseReportChart.Height = 300;

                //Phóng to chiều rộng của 2 nút vay nợ
                DebtDockPanel.Width = 600;
                LoanDockPanel.Width = 600;

            }
            else //Còn màn hình trong stack
            {

                //Nạp dữ liệu màn hình tiếp theo
                DetailInfomation detailInfomation = detailStack.Peek();
                DetailReportGrid.DataContext = detailInfomation;

                //Thay đổi dữ liệu biểu đồ
                AddDataIntoReportPieChart(DynamicPieChart, detailInfomation.Components);
                AddDataIntoReportColumnChart(DynamicColumnChart, detailInfomation.Components);

                //Chỉnh số hàng
                ModifyRowNumber(detailInfomation.Components);

                //Nếu đây là màn hình cuối cùng thì đổi dấu lùi thành dấu đóng khung
                if (detailStack.Count == 1)
                {

                    var imgName = "Images/black_close.png";
                    ChangeImage(imgName, CloseFrameImage);

                }

            }



            /*List<Category> categoryList = new List<Category>
			{
				new Category
				{
					ID = "0",
					Name = "Eating",
					ImagePath = "Images\\Eating.jpg"
				},
				new Category
				{
					ID = "1",
					Name = "Shopping",
					ImagePath = "Images\\Shopping.jpg"
				},
				new Category
				{
					ID = "2",
					Name = "Bills",
					ImagePath = "Images\\Bills.jpg"
				},
				new Category
				{
					ID = "3",
					Name = "Entertainment",
					ImagePath = "Images\\Entertainment.jpg"
				},
			};*/

            /*List<DetailCategory> detailCategoryList = new List<DetailCategory>
			{
				new DetailCategory
				{
					ID = "0",
					Name = "Eating",
					ImagePath = "Images\\Eating.jpg",
					Amount = 1500000
				},
				new DetailCategory
				{
					ID = "1",
					Name = "Shopping",
					ImagePath = "Images\\Shopping.jpg",
					Amount = 300000
				},
				new DetailCategory
				{
					ID = "2",
					Name = "Bills",
					ImagePath = "Images\\Bills.jpg",
					Amount = 760000
				},
				new DetailCategory
				{
					ID = "3",
					Name = "Entertainment",
					ImagePath = "Images\\Entertainment.jpg",
					Amount = 100000
				}
			};*/

            /*List<Budget> budgetList = new List<Budget>
			{
				new Budget
				{
					ID = "0",
					Name = "Eating",
					ImagePath = "Images\\Eating.jpg",
					StartingDate = "6/7/2021 11:14:17 AM",
					EndDate = "7/20/2021 12:17:00 AM",
					MoneyFund = 2000000,
					Note = "Tiền ăn uống cho nửa tháng"
				},
				new Budget
				{
					ID = "1",
					Name = "Shopping",
					ImagePath = "Images\\Shopping.jpg",
					StartingDate = "5/7/2021 4:00:00 AM",
					EndDate = "6/7/2021 12:00:00 AM",
					MoneyFund = 2000000,
					Note = "Tiền mua sắm cho một tháng"
				},
				new Budget
				{
					ID = "2",
					Name = "Bills",
					ImagePath = "Images\\Bills.jpg",
					StartingDate = "3/10/2021 10:14:17 AM",
					EndDate = "3/12/2021 10:17:00 AM",
					MoneyFund = 2000000,
					Note = "Tiền hóa đơn cho 2 ngày"
				},
				new Budget
				{
					ID = "3",
					Name = "Entertainment",
					ImagePath = "Images\\Entertainment.jpg",
					StartingDate = "2/22/2020 16:00:00 AM",
					EndDate = "2/22/2021 16:00:00 AM",
					MoneyFund = 2000000,
					Note = "Tiền giải trí cho một năm"
				}
			};*/

            /*List<DebitBook> debitBookList = new List<DebitBook>
			{
				new DebitBook
				{
					ID = "14",
					Name = "Lending",
					ImagePath = "Images\\Lending.jpg",
					StartingDate = "6/7/2021 11:14:17 AM",
					EndDate = "7/20/2021 12:17:00 AM",
					Amount = 500000,
					Note = "Tiền mượn đi ăn sáng",
					Stakeholder = "Phạm Tấn" 
				},
				new DebitBook
				{
					ID = "15",
					Name = "Paying",
					ImagePath = "Images\\Paying.jpg",
					StartingDate = "5/7/2021 4:00:00 AM",
					EndDate = "6/7/2021 12:00:00 AM",
					Amount = 250000,
					Note = "Trả tiền mượn đi ăn sáng",
					Stakeholder = "Phạm Tấn"
				},
				new DebitBook
				{
					ID = "16",
					Name = "Borrowing",
					ImagePath = "Images\\Borrowing.jpg",
					StartingDate = "3/10/2021 10:14:17 AM",
					EndDate = "3/12/2021 10:17:00 AM",
					Amount = 60000,
					Note = "Cho mượn tiền đi đổ xăng",
					Stakeholder = "Bùi Văn Vĩ"
				},
				new DebitBook
				{
					ID = "17",
					Name = "DebtCollection",
					ImagePath = "Images\\DebtCollection.jpg",
					StartingDate = "2/22/2020 16:00:00 AM",
					Amount = 24000,
					Note = "Thu nợ tiền đi đổ xăng",
					Stakeholder = "Bùi Văn Vĩ"
				}
			};*/

            /*List<Transaction> transactionList = new List<Transaction>
			{
				new Transaction
				{
					ID = "abc111",
					Date = "6/7/2021 11:14:17 AM",
					TransactionType = "0",
					Amount = 50000,
					Note = "Tiền đi ăn sáng ở quán bún đậu mắm tôm",
					Stakeholder = "Phạm Tấn"
				},
				new Transaction
				{
					ID = "hyz423",
					Date = "3/10/2021 10:14:17 AM",
					TransactionType = "1",
					Amount = 230000,
					Note = "Tiền đi mua sắm ở siêu thị",
					Stakeholder = "Bùi Văn Vĩ"
				},
				new Transaction
				{
					ID = "xyz473",
					Date = "2/22/2020 16:00:00 AM",
					TransactionType = "2",
					Amount = 100000,
					Note = "Tiền điện tháng 7 ở phòng KTX",
					Stakeholder = "Nguyễn Huy Hải"
				},
				new Transaction
				{
					ID = "uhd723",
					Date = "3/12/2021 10:17:00 AM",
					TransactionType = "3",
					Amount = 96000,
					Note = "Tiền đi uống cà phê ở quán Highland",
					Stakeholder = "Nguyễn Công Phượng"
				}
			};*/

            /*List<Report> reports = new List<Report>
			{
				new Report
				{
					ID = "a28hdw23jou4",
					StartingDate = "6/7/2021 11:14:17 AM",
					EndDate = "7/20/2021 12:17:00 AM",
					Income = new List<DetailCategory>
					{
						new DetailCategory
						{
							ID = "25",
							Name = "Bonus",
							ImagePath = "Images\\Bonus.jpg",
							Amount = 1500000
						},
						new DetailCategory
						{
							ID = "26",
							Name = "Interest",
							ImagePath = "Images\\Interest.jpg",
							Amount = 300000
						},
						new DetailCategory
						{
							ID = "27",
							Name = "Salary",
							ImagePath = "Images\\Salary.jpg",
							Amount = 760000
						},
						new DetailCategory
						{
							ID = "28",
							Name = "Awarded",
							ImagePath = "Images\\Awarded.jpg",
							Amount = 100000
						}
					},
					Expense = new List<DetailCategory>
					{
						new DetailCategory
						{
							ID = "0",
							Name = "Eating",
							ImagePath = "Images\\Eating.jpg",
							Amount = 1500000
						},
						new DetailCategory
						{
							ID = "1",
							Name = "Shopping",
							ImagePath = "Images\\Shopping.jpg",
							Amount = 300000
						},
						new DetailCategory
						{
							ID = "2",
							Name = "Bills",
							ImagePath = "Images\\Bills.jpg",
							Amount = 760000
						},
						new DetailCategory
						{
							ID = "3",
							Name = "Entertainment",
							ImagePath = "Images\\Entertainment.jpg",
							Amount = 100000
						}
					},
					Debt = new List<DetailCategory>
					{
						new DetailCategory
						{
							ID = "15",
							Name = "Paying",
							ImagePath = "Images\\Paying.jpg",
							Amount = 300000
						},
						new DetailCategory
						{
							ID = "16",
							Name = "Borrowing",
							ImagePath = "Images\\Borrowing.jpg",
							Amount = 760000
						}
					},
					Loan = new List<DetailCategory>
					{
						new DetailCategory
						{
							ID = "14",
							Name = "Lending",
							ImagePath = "Images\\Lending.jpg",
							Amount = 1500000
						},
						new DetailCategory
						{
							ID = "17",
							Name = "DebtCollection",
							ImagePath = "Images\\DebtCollection.jpg",
							Amount = 100000
						}
					}
				}
			};

			XmlSerializer xs = new XmlSerializer(typeof(List<Report>));
			TextWriter writer = new StreamWriter(@"Data\Report.xml");
			xs.Serialize(writer, reports);
			writer.Close();*/

        }

        //Biểu đồ hình quạt về thu nhập
        private void IncomeReportChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            //Khởi tạo biểu đồ
            IncomeReportChart.Series = new SeriesCollection();
            ((DefaultTooltip)IncomeReportChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;

            //Truyền dữ liệu vào biểu đồ
            AddDataIntoReportPieChart(IncomeReportChart, incomeList);

            //Tính tổng số tiền
            double sum = SumComponent(incomeList);

            //Định dạng lại số tiền ở dạng chuỗi và truyền vào màn hình
            Modal.MoneyConverter moneyConverter = new Modal.MoneyConverter();
            IncomeTextBlock.Text = (string)moneyConverter.Convert(sum, null, null, null);

        }

        //Biểu đồ hình quạt về chi tiêu
        private void ExpenseReportChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            //Khởi tạo biểu đồ
            ExpenseReportChart.Series = new SeriesCollection();
            ((DefaultTooltip)ExpenseReportChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;

            //Truyền dữ liệu vào biểu đồ
            AddDataIntoReportPieChart(ExpenseReportChart, expenseList);

            //Tính tổng số tiền
            double sum = SumComponent(expenseList);

            //Định dạng lại số tiền ở dạng chuỗi và truyền vào màn hình
            Modal.MoneyConverter moneyConverter = new Modal.MoneyConverter();
            ExpenseTextBlock.Text = (string)moneyConverter.Convert(sum, null, null, null);

        }

        //Hàm thêm dữ liệu vào biểu đồ hình tròn
        private void AddDataIntoReportPieChart(PieChart pieChart, List<DetailCategory> list)
        {

            pieChart.Series = new SeriesCollection();
            ((DefaultTooltip)pieChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
            //Thêm các thành phần vào biểu đồ
            foreach (DetailCategory component in list)
            {

                pieChart.Series.Add(
                    new PieSeries()
                    {
                        Values = new ChartValues<double> { component.Amount },
                        Title = component.Name
                    }
                );

            }

        }

        //Hàm thêm dữ liệu vào biểu đồ hình cột
        private void AddDataIntoReportColumnChart(CartesianChart columnChart, List<DetailCategory> list)
        {

            columnChart.Series = new SeriesCollection();
            ((DefaultTooltip)columnChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
            columnChart.AxisY = new AxesCollection();

            //Thêm các thành phần vào biểu đồ
            foreach (var component in list)
            {

                columnChart.Series.Add(new ColumnSeries()
                {
                    Values = new ChartValues<double> { component.Amount },
                    Title = component.Name
                });

            }

        }

        private void CommonChartButton_Click(object sender, RoutedEventArgs e)
        {

            //Hiển thị khung báo cáo chi tiết thu nhập
            DetailReportGrid.Visibility = Visibility.Visible;

            //Thu nhỏ chiều rộng của khung báo cáo chung
            GeneralReportGrid.Width = 410;

            //Phóng to khung báo cáo chi tiết về thu nhập
            DetailReportGrid.Width = 600;

            //Thu nhỏ kích thước của 2 biểu đồ hình bánh thể hiện thu chi
            IncomeReportChart.Width = 200;
            IncomeReportChart.Height = 200;
            ExpenseReportChart.Width = 200;
            ExpenseReportChart.Height = 200;

            //Thu nhỏ chiều rộng của 2 nút vay nợ
            DebtDockPanel.Width = 410;
            LoanDockPanel.Width = 410;

            PieChartIconButton.Height = 46;
            PieChartIconTextBlock.Visibility = Visibility.Visible;
            //Hiển thị biểu đồ hình tròn
            DynamicPieChart.Visibility = Visibility.Visible;
            DynamicPieChartTextBlock.Visibility = Visibility.Visible;
            //Ẩn biểu đồ hình cột
            DynamicColumnChart.Visibility = Visibility.Collapsed;
            DynamicColumnChartTextBlock.Visibility = Visibility.Collapsed;

            var button = sender as Button;
            var buttonName = button.Name;
            string title = buttonName.Replace("Button", "");
            List<DetailCategory> list = new List<DetailCategory>();

            if (buttonName == "IncomeButton")
            {

                list = incomeList;

            }
            else if (buttonName == "ExpenseButton")
            {

                list = expenseList;

            }
            else
            {
                list = buttonName == "DebtButton" ? debtList : loanList;

            }

            //Xóa toàn bộ dữ liệu các màn hình
            detailStack.Clear();
            var imgName = "Images/black_close.png";
            ChangeImage(imgName, CloseFrameImage);

            //Hiển thị tiêu đề của khung báo cáo chi tiết
            DetailReportGrid.DataContext = AddDataIntoDetailReport(title, list);

        }

        //Hàm hiển thị biểu đồ hình tròn trong khung chi tiết báo cáo
        private void PieChartIconButton_Click(object sender, RoutedEventArgs e)
        {

            //Hiển thị biểu đồ hình bánh
            PieChartIconButton.Height = 46;
            PieChartIconTextBlock.Visibility = Visibility.Visible;

            //Ẩn biểu đồ hình cột
            ColumnChartIconButton.Height = 50;
            ColumnChartIconTextBlock.Visibility = Visibility.Collapsed;

            //Hiển thị biểu đồ hình tròn
            DynamicPieChart.Visibility = Visibility.Visible;
            DynamicPieChartTextBlock.Visibility = Visibility.Visible;
            //Ẩn biểu đồ hình cột
            DynamicColumnChart.Visibility = Visibility.Collapsed;
            DynamicColumnChartTextBlock.Visibility = Visibility.Collapsed;

        }

        //Hàm hiển thị biểu đồ hình cột trong khung chi tiết báo cáo
        private void ColumnChartIconButton_Click(object sender, RoutedEventArgs e)
        {

            //Hiển thị biểu đồ hình cột
            ColumnChartIconButton.Height = 46;
            ColumnChartIconTextBlock.Visibility = Visibility.Visible;

            //Ẩn biểu đồ hình cột
            PieChartIconButton.Height = 50;
            PieChartIconTextBlock.Visibility = Visibility.Collapsed;

            //Hiển thị biểu đồ hình cột
            DynamicColumnChart.Visibility = Visibility.Visible;
            DynamicColumnChartTextBlock.Visibility = Visibility.Visible;
            //Ẩn biểu đồ hình tròn
            DynamicPieChart.Visibility = Visibility.Collapsed;
            DynamicPieChartTextBlock.Visibility = Visibility.Collapsed;

        }

        //Hàm xử lý nút xem chi tiết danh sách loại

        //Hàm nạp dữ liệu cho khung báo cáo chi tiết
        private DetailInfomation AddDataIntoDetailReport(string title, List<DetailCategory> list)
        {

            DetailInfomation detailInfomation = new DetailInfomation
            {

                Components = list,
                Title = title,
                TotalMoney = SumComponent(list)

            };

            AddDataIntoReportPieChart(DynamicPieChart, list);
            AddDataIntoReportColumnChart(DynamicColumnChart, list);

            //Đưa thông tin chi tiết báo cáo hiện tại vào Stack
            detailStack.Push(detailInfomation);

            //Chỉnh số hàng
            ModifyRowNumber(list);

            return detailInfomation;

        }

        //Hàm điểu chỉnh số hàng của khung danh sách loại
        private void ModifyRowNumber(List<DetailCategory> list)
        {

            //Chỉnh chiều cao cho danh sách loại với số hàng tối đa là 5
            if (list.Count <= 5)
            {

                CategoryListView.Height = list.Count * 65;

            }
            else
            {

                CategoryListView.Height = 5 * 65;

            }

        }

        //Tính tổng số tiền của danh sách
        private double SumComponent(List<DetailCategory> list)
        {

            var sum = 0.0;

            foreach (DetailCategory component in list)
            {

                sum += component.Amount;

            }

            return sum;

        }

        //Thay đổi hình ảnh
        private void ChangeImage(string path, Image image)
        {

            //Lấy nguồn ảnh
            var img = new BitmapImage(new Uri(
                        path,
                        UriKind.Relative)
                );

            //Thiết lập ảnh chất lượng cao
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);

            //Thay đổi icon
            image.Source = img;

        }

        //Hàm bắt sự kiện nhấn chuột trái vào khung chi tiết loại
        private void CategoryListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //Chi tiết loại được nhấn vào
            var detailCategory = CategoryListView.SelectedItem as DetailCategory;

            //Trường hợp khung chi tiết loại nhóm cha
            if (detailStack.Count == 1)
            {

                var valuePairs = new Dictionary<string, double>();

                //Đọc qua tất cả các giao dịch để tìm các chi tiết loại nhóm con
                foreach (Transaction transaction in transactionList)
                {

                    var type = transaction.TransactionType;
                    var index = type.IndexOf(detailCategory.ID);

                    if (index == 0) //Nhóm con thì thêm dữ liệu vào từ điển
                    {

                        AddDataIntoDictionary(valuePairs, type, transaction.Amount, false);

                    }

                }

                //Nếu có từ 2 nhóm con trở lên thì hiển thị khung chi tiết loại nhóm con
                if (valuePairs.Count > 1)
                {

                    List<DetailCategory> detailList = new List<DetailCategory>();

                    //Chuyển dữ liệu 4 nhóm giao dịch vào danh sách
                    AddDataIntoList(valuePairs, detailList);

                    //Đổi tên tiêu đề khung báo cáo chi tiết
                    DetailReportGrid.DataContext = AddDataIntoDetailReport(detailCategory.Name, detailList);

                }
                else //Nếu chỉ có 1 nhóm con thì hiển thị biểu đồ theo tháng
                {

                    AddDataIntoMonthChart(detailCategory);

                }

            }
            else
            {

                //Nếu là nhóm con thì hiển thị biểu đồ theo tháng
                AddDataIntoMonthChart(detailCategory);

            }

            //Nếu là khung chi tiết loại thứ 2 trở đi thì đổi dấu đóng thành dấu quay lại
            var imgName = "Images/left_arrow.png";
            ChangeImage(imgName, CloseFrameImage);

            //Cuộn lên trên đầu của khung chi tiết báo cáo
            DetailScrollViewer.ScrollToTop();

        }

        //Thêm dữ liệu vào biểu đồ giao dịch theo tháng
        private void AddDataIntoMonthChart(DetailCategory detailCategory)
        {

            //Danh sách các giao dịch được lọc
            List<DetailCategory> detailList = new List<DetailCategory>();

            //Danh sách chứa tên và đường dẫn ảnh của tháng
            var monthList = new List<(string Name, string ImagePath)>
            {

                ("January", "Images\\january.png"),
                ("February", "Images\\february.png"),
                ("March", "Images\\march.png"),
                ("April", "Images\\april.png"),
                ("May", "Images\\may.png"),
                ("June", "Images\\june.png"),
                ("July", "Images\\July.png"),
                ("August", "Images\\august.png"),
                ("September", "Images\\september.png"),
                ("October", "Images\\october.png"),
                ("November", "Images\\november.png"),
                ("December", "Images\\december.png")

            };

            //Mảng lưu trữ số tiền của 12 tháng
            var amountPerMonth = new double[12];

            //Duyệt qua tất cả các giao dịch để tìm giao dịch phù hợp
            foreach (Transaction transaction in transactionList)
            {

                //Giao dịch phải thỏa điều kiện cùng ID với ID của tham số đầu vào
                if (transaction.TransactionType == detailCategory.ID)
                {

                    //Cộng dồn số tiền giao dịch trong tháng
                    amountPerMonth[transaction.Date.Month - 1] += transaction.Amount;

                }

            }

            //Duyệt qua 12 tháng trong năm
            for (int month = 0; month < 12; month++)
            {

                if (amountPerMonth[month] > 0) //Chỉ xử lý những tháng nào có giao dịch
                {

                    //Khởi tạo và thêm dữ liệu vào danh sách
                    detailList.Add(new DetailCategory
                    {
                        Amount = amountPerMonth[month],
                        ID = $"{month}",
                        ImagePath = monthList[month].ImagePath,
                        Name = monthList[month].Name
                    });

                }

            }

            //Đổi tên tiêu đề khung báo cáo chi tiết
            DetailReportGrid.DataContext = AddDataIntoDetailReport(detailCategory.Name, detailList);

        }

        private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransaction add = new AddTransaction();
            add.Show();
        }

        private void AddBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            AddBudget add = new AddBudget();
            add.Show();
        }

        private void CloseDetailBudget_Click(object sender, RoutedEventArgs e)
        {
            BudgetReportGrid.Visibility = Visibility.Collapsed;
            Budget.Width = 600;
            BudgetListBorder.Width = 600;
        }

        private void RunningButton_Click(object sender, RoutedEventArgs e)
        {
            // Chỉnh lại định dạng nút cho nổi bật
            RunningTextblock.Foreground = Brushes.Green;
            RunningTextblock.FontSize = 20;
            RunningButton.BorderThickness = new Thickness(0, 0, 0, 1);
            RunningButton.BorderBrush = Brushes.Green;
            RunningUnderlineTextBlock.Visibility = Visibility.Visible;

            // Chỉnh lại định dạng nút còn lại thành bình thường
            FinishedTextblock.Foreground = Brushes.Black;
            FinishedButton.BorderThickness = new Thickness(0, 0, 0, 0);
            FinishedTextblock.FontSize = 15;
            FinishedUnderlineTextBlock.Visibility = Visibility.Collapsed;

            // Cập nhật danh sách những ngân sách đã quá hạn
            BudgetList.ItemsSource = runningBudgetList;

        }

        private void FinishedButton_Click(object sender, RoutedEventArgs e)
        {
            // Chỉnh lại định dạng nút cho nổi bật
            FinishedTextblock.Foreground = Brushes.Green;
            FinishedTextblock.FontSize = 20;
            FinishedButton.BorderThickness = new Thickness(0, 0, 0, 1);
            FinishedButton.BorderBrush = Brushes.Green;
            FinishedUnderlineTextBlock.Visibility = Visibility.Visible;

            // Chỉnh lại định dạng nút còn lại thành bình thường
            RunningTextblock.Foreground = Brushes.Black;
            RunningButton.BorderThickness = new Thickness(0, 0, 0, 0);
            RunningTextblock.FontSize = 15;
            RunningUnderlineTextBlock.Visibility = Visibility.Collapsed;

            // Cập nhật danh sách những ngân sách đã quá hạn
            BudgetList.ItemsSource = finishedBudgetList;

        }

        private void ViewTransactionListButton_Click(object sender, RoutedEventArgs e)
        {
            BudgetDetail win = new BudgetDetail();
            win.Show();
        }

        private void BudgetLineChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //BudgetLineChart.Series.Clear();
           
            //var end = DateTime.Parse(budgetInfo.EndDate);
            //var start = DateTime.Parse(budgetInfo.StartingDate);
            //var curr = new DateTime();
            //TimeSpan time = end - start;
            //int durationOfBudget = time.Days + 1;

            //List<double> budgetGoalLine = new List<double>();
            //List<double> budgetSpentLine = new List<double>();
           
            //Dictionary<DateTime, double> myDic = new Dictionary<DateTime, double>();
            //Dictionary<DateTime, double> myDic1 = new Dictionary<DateTime, double>();
            //Dictionary<DateTime, double> myDic2 = new Dictionary<DateTime, double>();
            //double money= 0;

            //for (int i = 0; i < transactionLists.Count; i++)    
            //{
            //    if (transactionLists[i].TransactionType == budgetInfo.Name)
            //    {
                   
            //        for (int j = 0; j < transactionLists[i].Transactions.Count; j++)
            //        {
                       
            //            if (transactionLists[i].Transactions[j].Date < end && transactionLists[i].Transactions[j].Date > start)
            //            {
                          
            //                if (!myDic2.ContainsKey(transactionLists[i].Transactions[j].Date))
            //                {
            //                    myDic2.Add(transactionLists[i].Transactions[j].Date, transactionLists[i].Transactions[j].Amount);
            //                }
            //                else
            //                {
            //                    myDic2[transactionLists[i].Transactions[j].Date] = myDic2[transactionLists[i].Transactions[j].Date] + transactionLists[i].Transactions[j].Amount;
            //                }
                            
                            
            //            }
            //        }
            //    }

                
                
            //}

            //for (int i = 0; i < durationOfBudget; i++)
            //{
            //    budgetGoalLine.Add(budgetInfo.MoneyFund);
            //    curr = start.AddDays(i);
            //    if (myDic2.ContainsKey(curr))
            //    {
            //        money += myDic2[curr];
            //    }
            //    budgetSpentLine.Add(money);

            //}


        

            //BudgetLineChart.Series.Add(new LineSeries()
            //{
            //    Values = new ChartValues<double> (budgetGoalLine),
            //    LineSmoothness = 0,
            //    PointGeometry = null,
            //    PointGeometrySize = 0,
            //    Title = "Max"
            //});
            //BudgetLineChart.Series.Add(new LineSeries()
            //{
            //    Values = new ChartValues<double> (budgetSpentLine),
            //    LineSmoothness = 0,
            //    PointGeometry = null,
            //    PointGeometrySize = 0,
            //    Title = "Current"
            //});
        }

        // Nút chuyển sang tháng trước trong giao diện giao dịch
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousTextblock.Foreground = Brushes.Green;
            PreviousTextblock.FontSize = 20;
            PreviousButton.BorderThickness = new Thickness(0, 0, 0, 1);
            PreviousButton.BorderBrush = Brushes.Green;

            CurrentTextblock.Foreground = Brushes.Black;
            CurrentButton.BorderThickness = new Thickness(0, 0, 0, 0);
            CurrentTextblock.FontSize = 15;

            NextTextblock.Foreground = Brushes.Black;
            NextButton.BorderThickness = new Thickness(0, 0, 0, 0);
            NextTextblock.FontSize = 15;
        }

        // Nút tháng hiện tại, có thể dùng để chuyển sang tháng tiếp theo với tháng trước 
        private void CurrentButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentTextblock.Foreground = Brushes.Green;
            CurrentTextblock.FontSize = 20;
            CurrentButton.BorderThickness = new Thickness(0, 0, 0, 1);
            CurrentButton.BorderBrush = Brushes.Green;

            PreviousTextblock.Foreground = Brushes.Black;
            PreviousButton.BorderThickness = new Thickness(0, 0, 0, 0);
            PreviousTextblock.FontSize = 15;

            NextTextblock.Foreground = Brushes.Black;
            NextButton.BorderThickness = new Thickness(0, 0, 0, 0);
            NextTextblock.FontSize = 15;
        }

        // Nút chuyển sang tháng tiếp theo của Nút tháng hiện tại
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextTextblock.Foreground = Brushes.Green;
            NextTextblock.FontSize = 20;
            NextButton.BorderThickness = new Thickness(0, 0, 0, 1);
            NextButton.BorderBrush = Brushes.Green;

            CurrentTextblock.Foreground = Brushes.Black;
            CurrentButton.BorderThickness = new Thickness(0, 0, 0, 0);
            CurrentTextblock.FontSize = 15;

            PreviousTextblock.Foreground = Brushes.Black;
            PreviousButton.BorderThickness = new Thickness(0, 0, 0, 0);
            PreviousTextblock.FontSize = 15;
        }

        private void ViewReportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BudgetListButton_Click(object sender, RoutedEventArgs e)
        {

            BudgetReportGrid.Visibility = Visibility.Visible;
            Budget.Width = 410;
            BudgetListBorder.Width = 410;
            BudgetReportGrid.Width = 600;

            var temp = sender as Button;

            budgetInfo = temp.DataContext as Budget;
            int lol = 0;
            foreach (var budget in budgetList)
            {
                if (budget.ID == budgetInfo.ID)
                {
                    //transactionType = budget.Name;
                    break;
                }
                lol++;
            }

            BudgetInfo.DataContext = budgetList[lol];
            budgetDocPanel.DataContext = budgetList[lol];



            // Hiển thị dữ liệu cho biểu đồ(nên gôp với hàm tính số tiền đã chi tiêu cho ngân sách, nếu được thì chỉ cập nhật sau khi CRUD 1 giao dịch)
            BudgetLineChart.Series.Clear();

            var end = DateTime.Parse(budgetInfo.EndDate);
            var start = DateTime.Parse(budgetInfo.StartingDate);
            var curr = new DateTime();
            TimeSpan time = end - start;
            int durationOfBudget = time.Days + 1;

            List<double> budgetGoalLine = new List<double>();
            List<double> budgetSpentLine = new List<double>();
            // Lưu <ngày, số tiền chi tiêu> trong các giao dịch 
            Dictionary<DateTime, double> myDic = new Dictionary<DateTime, double>();
            double money = 0;

            for (int i = 0; i < transactionLists.Count; i++)
            {
                if (transactionLists[i].TransactionType == budgetInfo.Name)
                {

                    for (int j = 0; j < transactionLists[i].Transactions.Count; j++)
                    {

                        if (transactionLists[i].Transactions[j].Date < end && transactionLists[i].Transactions[j].Date > start)
                        {

                            if (!myDic.ContainsKey(transactionLists[i].Transactions[j].Date))
                            {
                                myDic.Add(transactionLists[i].Transactions[j].Date, transactionLists[i].Transactions[j].Amount);
                            }
                            else
                            {
                                myDic[transactionLists[i].Transactions[j].Date] = myDic[transactionLists[i].Transactions[j].Date] + transactionLists[i].Transactions[j].Amount;
                            }


                        }
                    }
                }



            }

            for (int i = 0; i < durationOfBudget; i++)
            {
                budgetGoalLine.Add(budgetInfo.MoneyFund);
                curr = start.AddDays(i);
                // nếu có ngày trong danh sách giao dịch thì thêm vào
                if (myDic.ContainsKey(curr))
                {
                    money += myDic[curr];
                }
                budgetSpentLine.Add(money);

            }



            
           

            BudgetLineChart.Series.Add(new LineSeries()
            {
                Values = new ChartValues<double>(budgetGoalLine),
                LineSmoothness = 0,
                PointGeometry = null,
                PointGeometrySize = 0,
                Title = "Max",
              
            });
            BudgetLineChart.Series.Add(new LineSeries()
            {
                Values = new ChartValues<double>(budgetSpentLine),
                LineSmoothness = 0,
                PointGeometry = null,
                PointGeometrySize = 0,
                Title = "Current",
                
            });

            // Xóa Axisx
            BudgetLineChart.AxisX.Add(new Axis
            {
                Labels = new string[0]
            });



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        // Hàm chặn khi auto scrolling lúc nhấn vào vị trí bất kì trong scrollviewer
        private void ScrollViewer_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

    }

}
