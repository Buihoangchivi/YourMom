﻿using LiveCharts;
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
using System.ComponentModel;
using System.Configuration;

using System.Text.RegularExpressions;

using System.Windows.Media.Animation;

using Microsoft.Win32;
using System.Globalization;

namespace YourMom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Button clickedButton;

        private List<Transaction> transactionList;
        List<TempTransaction> tempTransactionList;
        private List<Budget> budgetList;

        private Dictionary<string, Category> categoryList = new Dictionary<string, Category>();
        private Budget budgetInfo;
        private DateTime startingDate, endDate;
        private bool isDebtTransaction = false;

        List<DetailCategory> incomeList = new List<DetailCategory>();
        List<DetailCategory> expenseList = new List<DetailCategory>();
        List<DetailCategory> debtList = new List<DetailCategory>();
        List<DetailCategory> loanList = new List<DetailCategory>();

        private BindingList<ColorSetting> ListColor;

        //Class lưu trữ màu trong Color setting
        public class ColorSetting
        {
            public string Color { get; set; }
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

        ObservableCollection<CategoryList> categoryCollection = new ObservableCollection<CategoryList>();
        ObservableCollection<CategoryList> categoryDebtCollection = new ObservableCollection<CategoryList>();
        ObservableCollection<TransactionList> transactionCollection = new ObservableCollection<TransactionList>();
        ObservableCollection<TransactionList> transactionDebtCollection = new ObservableCollection<TransactionList>();

        Stack<DetailInfomation> detailStack = new Stack<DetailInfomation>();

        // Danh sách ngân sách đang sử dụng
        ObservableCollection<Budget> runningBudgetList = new ObservableCollection<Budget> { };
        // Danh sách ngân sách đã quá hạn
        ObservableCollection<Budget> finishedBudgetList = new ObservableCollection<Budget> { };

        public MainWindow()
        {

            InitializeComponent();

            //Đọc dữ liệu
            ReadData();

            //Thời gian mặc định là tháng hiện tại
            startingDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            endDate = startingDate.AddMonths(1).AddDays(-1);

            //Nạp dữ liệu vào màn hình danh sách giao dịch
            AddDataIntoTransactionScreen();

            //Nạp dữ liệu vào màn hình ngân sách
            AddDataIntoBudgetScreen();

            //Khởi tạo dữ liệu cho màn hình báo cáo
            InitializeReportData();

        }

        //Khởi tạo dữ liệu báo cáo
        private void InitializeReportData()
        {

            //Khởi tạo các từ điển
            var incomePairs = new Dictionary<string, double>();
            var expensePairs = new Dictionary<string, double>();
            var debtPairs = new Dictionary<string, double>();
            var loanPairs = new Dictionary<string, double>();

            //Xóa sạch dữ liệu trong các danh sách
            incomeList.Clear();
            expenseList.Clear();
            debtList.Clear();
            loanList.Clear();

            //Tính tổng 4 nhóm giao dịch
            foreach (Transaction transaction in transactionList)
            {

                if (startingDate <= transaction.Date && transaction.Date <= endDate)
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

            }

            //Chuyển dữ liệu 4 nhóm giao dịch vào danh sách
            AddDataIntoList(incomePairs, incomeList);
            AddDataIntoList(expensePairs, expenseList);
            AddDataIntoList(debtPairs, debtList);
            AddDataIntoList(loanPairs, loanList);

            //Khởi tạo dữ liệu cho khung vay nợ
            InitDebtReportData(debtList, DebtTextBlock, DebtLeftTextBlock);
            InitDebtReportData(loanList, LoanTextBlock, LoanLeftTextBlock);

        }

        //Nạp dữ liệu vào màn hình giao dịch
        private void AddDataIntoTransactionScreen()
        {

            //Trường hợp đang mở màn hình giao dịch vay nợ
            if (isDebtTransaction == true)
            {

                categoryDebtCollection = new ObservableCollection<CategoryList>();
                transactionDebtCollection = new ObservableCollection<TransactionList>();

                //Đọc dữ liệu tất cả các giao dịch vay nợ vào danh sách giao dịch ở dạng nhóm
                InitDataIntoObservableCollection(categoryDebtCollection, startingDate, endDate);
                CategoryList.ItemsSource = categoryDebtCollection;

                //Đọc dữ liệu tất cả các giao dịch vay nợ vào danh sách giao dịch ở dạng giao dịch
                InitDataIntoObservableCollection(transactionDebtCollection, startingDate, endDate);
                TransactionList.ItemsSource = transactionDebtCollection;

            }
            else //Trường hợp đang mở màn hình giao dịch thông thường
            {

                categoryCollection = new ObservableCollection<CategoryList>();
                transactionCollection = new ObservableCollection<TransactionList>();

                //Đọc dữ liệu tất cả các giao dịch vào danh sách giao dịch ở dạng nhóm
                InitDataIntoObservableCollection(categoryCollection, startingDate, endDate);
                CategoryList.ItemsSource = categoryCollection;

                //Đọc dữ liệu tất cả các giao dịch vào danh sách giao dịch ở dạng giao dịch
                InitDataIntoObservableCollection(transactionCollection, startingDate, endDate);
                TransactionList.ItemsSource = transactionCollection;

            }

        }

        //Nạp dữ liệu vào màn hình ngân sách
        private void AddDataIntoBudgetScreen()
        {

            runningBudgetList.Clear();
            finishedBudgetList.Clear();

            // Hàm xử lý ngân sách
            for (int i = 0; i < budgetList.Count; i++)
            {

                // Lấy số tiền đã chi tiêu cho ngân sách thông qua cách giao dịch
                double moneyTotal = 0;

                //Duyện qua danh sách tất cả các giao dịch
                for (int j = 0; j < transactionList.Count; j++)
                {

                    var index = transactionList[j].TransactionType.IndexOf(budgetList[i].ID);

                    //Kiểm tra có cùng loại giao dịch hay không
                    if (//Thời gian giao dịch phải nằm trong khoảng thời gian của ngân sách
                        transactionList[j].Date <= budgetList[i].EndDate &&
                        transactionList[j].Date >= budgetList[i].StartingDate && index == 0)
                    {

                        moneyTotal += transactionList[j].Amount; //Tính tổng tiền

                    }

                }

                //Gán tổng tiền vào ngân sách
                budgetList[i].SpentMoney = moneyTotal;

                // lấy số ngày còn lại trong ngân sách			
                DateTime currentdate = DateTime.Now;

                // Số ngày còn lại của ngân sách
                TimeSpan time = budgetList[i].EndDate - currentdate;
                int dayLeft = time.Days < 0 ? 0 : time.Days;

                budgetList[i].DaysLeft = dayLeft > 0 ? $"{time.Days} days left" : "Finished";

                // số tiền dư còn lại cho ngân sách
                budgetList[i].Balance = budgetList[i].MoneyFund - budgetList[i].SpentMoney;

                // lấy tiến độ hiện tại, làm tròn 2 số sau dấu phẩy
                var temp = Math.Round(budgetList[i].SpentMoney / budgetList[i].MoneyFund * 100, 2);
                budgetList[i].Progress = temp;

                // số tiền nên chi hàng ngày
                budgetList[i].ShouldSpending_DayMoney = time.Days >= 0 ?
                    Math.Round(budgetList[i].Balance / dayLeft, 2) : 0;

                //Số tiền chi tiêu thực tế trong ngày
                budgetList[i].RealitySpending_DayMoney = Math.Round(budgetList[i].SpentMoney /
                    ((currentdate - budgetList[i].StartingDate).Days + 1), 2);

                // số tiền dự kiến chi tiêu
                budgetList[i].ExpectedSpendingMoney = budgetList[i].SpentMoney +
                    budgetList[i].RealitySpending_DayMoney * dayLeft;

                //Ngân sách đã kết thúc
                if (time.Days < 0)
                {

                    finishedBudgetList.Add(budgetList[i]);

                }
                else //Ngân sách đang thực hiện
                {

                    runningBudgetList.Add(budgetList[i]);

                }

            }

            BudgetList.ItemsSource = runningBudgetList;

        }

        //Hàm khởi tạo dữ liệu theo kiểu nhóm vào danh sách giao dịch hoặc danh sách vay nợ
        private void InitDataIntoObservableCollection(ObservableCollection<CategoryList> categories, DateTime startingDate, DateTime endDate)
        {

            //Mảng lưu số tiền thu vào và số tiền chi ra
            double[] amountArray = { 0.0, 0.0 };

            //Đọc qua tất cả các giao dịch
            foreach (var transaction in transactionList)
            {

                //Kiểm tra xem thời gian của giao dịch có nằm trong khoảng thời thời đầu vào hay không
                if (startingDate <= transaction.Date && transaction.Date <= endDate)
                {

                    //ID của loại giao dịch
                    var type = transaction.TransactionType;
                    var firstID = type[0] - '0';
                    //Biến kiểm tra ID có xuất phát là số 0 hoặc số 1 không
                    var isTransaction = firstID < 2;

                    //Kiểm tra có phải trường hợp danh sách giao dịch không
                    if ((isTransaction && categories == categoryCollection) ||
                        //Kiểm tra có phải trường hợp danh sách vay nợ không
                        (!isTransaction && categories == categoryDebtCollection))
                    {

                        var pos = 0;

                        for (; pos < categories.Count; pos++)
                        {

                            if (categories[pos].Name == categoryList[type].Name)
                            {

                                break;

                            }

                        }

                        //Kiểm tra đã thêm loại giao dịch này vào list chưa
                        if (pos < categories.Count)
                        {

                            //Lấy danh sách giao dịch cần tìm ra
                            var list = categories[pos];

                            //Tìm vị trí để chèn giao dịch vào danh sách
                            var index = IndexOfCollectionBinarySearch(list.Transactions, transaction.Date);

                            //Thêm giao dịch vào danh sách
                            list.Transactions.Insert(index, transaction);

                            //Tăng số lượng giao dịch lên
                            list.NumberOfTransactions++;

                            //Cộng dồn số tiền của danh sách giao dịch lên
                            list.TotalMoney += transaction.Amount;

                        }
                        else //Trường hợp chưa thêm danh sách giao dịch vào list
                        {

                            //Khởi tạo và thêm mới giao dịch vào list
                            categories.Add(new CategoryList
                            {

                                //Đường dẫn ảnh của icon loại giao dịch
                                ImagePath = categoryList[type].ImagePath,
                                //Só lượng giao dịch hiện có
                                NumberOfTransactions = 1,
                                //Tên loại giao dịch
                                Name = categoryList[type].Name,
                                //Tổng số tiền
                                TotalMoney = transaction.Amount,
                                //Danh sách giao dịch
                                Transactions = new ObservableCollection<Transaction>() { transaction },

                            });

                        }

                        //Cộng dồn số tiền tương ứng
                        //firstID: 0 hoặc 2 (thu nhập hoặc đi vay) được tính vào khoản tiền vào
                        //firstID: 1 hoặc 3 (chi tiêu hoặc cho vay) được tính vào khoản tiền ra
                        amountArray[firstID % 2] += transaction.Amount;

                    }

                }

            }

            //Nếu không có giao dịch nào trong khoảng thời gian đầu vào thì hiển thị khung trống
            if (categories.Count == 0)
            {

                TransactionScrollViewer.Visibility = Visibility.Collapsed;
                NoTransactionBorder.Visibility = Visibility.Visible;
                return;

            }
            else //Nếu có giao dịch thì hiển thị khung danh sách giao dịch
            {

                TransactionScrollViewer.Visibility = Visibility.Visible;
                NoTransactionBorder.Visibility = Visibility.Collapsed;

            }

            //Định dạng lại số tiền ở dạng chuỗi và truyền vào màn hình
            Modal.MoneyConverter moneyConverter = new Modal.MoneyConverter();

            //Hiển thị số tiền chi ra
            var money = (string)moneyConverter.Convert(amountArray[0], null, null, null);
            InflowTextBlock.Text = money;

            //Hiển thị số tiền thu vào
            money = (string)moneyConverter.Convert(-amountArray[1], null, null, null);
            OutflowTextBlock.Text = money;

            //Hiển thị số tiền còn lại
            money = (string)moneyConverter.Convert(amountArray[0] - amountArray[1], null, null, null);
            LeftTextBlock.Text = money;

        }

        //Hàm khởi tạo dữ liệu theo kiểu giao dịch vào danh sách giao dịch hoặc danh sách vay nợ
        private void InitDataIntoObservableCollection(ObservableCollection<TransactionList> transactions, DateTime startingDate, DateTime endDate)
        {

            //Mảng lưu số tiền thu vào và số tiền chi ra
            double[] amountArray = { 0.0, 0.0 };

            //Đọc qua tất cả các giao dịch
            foreach (var transaction in transactionList)
            {

                //Kiểm tra xem thời gian của giao dịch có nằm trong khoảng thời thời đầu vào hay không
                if (startingDate <= transaction.Date && transaction.Date <= endDate)
                {

                    //ID của loại giao dịch
                    var type = transaction.TransactionType;
                    var firstID = type[0] - '0';
                    //Biến kiểm tra ID có xuất phát là số 0 hoặc số 1 không
                    var isTransaction = firstID < 2;

                    //Kiểm tra có phải trường hợp danh sách giao dịch không
                    if ((isTransaction && transactions == transactionCollection) ||
                        //Kiểm tra có phải trường hợp danh sách vay nợ không
                        (!isTransaction && transactions == transactionDebtCollection))
                    {

                        var date = transaction.Date.ToLongDateString();
                        var amount = transaction.Amount;
                        if (firstID % 2 != 0)
                        {

                            amount *= -1;

                        }

                        var detail = new DetailTransaction
                        {

                            Amount = transaction.Amount,
                            Date = transaction.Date,
                            ID = transaction.ID,
                            ImagePath = categoryList[transaction.TransactionType].ImagePath,
                            Name = categoryList[transaction.TransactionType].Name,
                            Note = transaction.Note,
                            Stakeholder = transaction.Stakeholder,
                            TransactionType = transaction.TransactionType

                        };

                        //Tìm vị trí để chèn giao dịch vào danh sách
                        var index = IndexOfCollectionBinarySearch(transactions, transaction.Date);

                        //Kiểm tra đã thêm loại ngày tháng năm này vào list chưa
                        if (index >= 0 && index < transactions.Count &&
                            transactions[index].Date.ToLongDateString() == date)
                        {

                            //Lấy danh sách giao dịch cần tìm ra
                            var list = transactions[index];

                            //Thêm giao dịch vào danh sách
                            list.Transactions.Add(detail);

                            //Cộng dồn số tiền của danh sách giao dịch lên
                            list.TotalMoney += amount;

                        }
                        else //Trường hợp chưa thêm danh sách giao dịch vào list
                        {

                            //Khởi tạo và thêm mới giao dịch vào list
                            var list = new TransactionList
                            {

                                //Thời gian tạo giao dịch
                                Date = transaction.Date,
                                //Tổng số tiền
                                TotalMoney = amount,
                                //Danh sách giao dịch
                                Transactions = new ObservableCollection<DetailTransaction>() { detail },

                            };

                            transactions.Insert(index, list);

                        }

                        //Cộng dồn số tiền tương ứng
                        //firstID: 0 hoặc 2 (thu nhập hoặc đi vay) được tính vào khoản tiền vào
                        //firstID: 1 hoặc 3 (chi tiêu hoặc cho vay) được tính vào khoản tiền ra
                        amountArray[firstID % 2] += transaction.Amount;

                    }

                }

            }

            //Nếu không có giao dịch nào trong khoảng thời gian đầu vào thì hiển thị khung trống
            if (transactions.Count == 0)
            {

                TransactionScrollViewer.Visibility = Visibility.Collapsed;
                NoTransactionBorder.Visibility = Visibility.Visible;
                return;

            }
            else //Nếu có giao dịch thì hiển thị khung danh sách giao dịch
            {

                TransactionScrollViewer.Visibility = Visibility.Visible;
                NoTransactionBorder.Visibility = Visibility.Collapsed;

            }

            //Định dạng lại số tiền ở dạng chuỗi và truyền vào màn hình
            Modal.MoneyConverter moneyConverter = new Modal.MoneyConverter();

            //Hiển thị số tiền chi ra
            var money = (string)moneyConverter.Convert(amountArray[0], null, null, null);
            InflowTextBlock.Text = money;

            //Hiển thị số tiền thu vào
            money = (string)moneyConverter.Convert(-amountArray[1], null, null, null);
            OutflowTextBlock.Text = money;

            //Hiển thị số tiền còn lại
            money = (string)moneyConverter.Convert(amountArray[0] - amountArray[1], null, null, null);
            LeftTextBlock.Text = money;

        }

        //Hàm tìm kiếm nhị phân vị trí để chèn giao dịch vào danh sách tăng dần theo ngày tháng
        private int IndexOfCollectionBinarySearch(dynamic list, DateTime dateTime)
        {

            int low = 0, high = list.Count;
            int mid;

            while (low < high)
            {

                mid = (low + high) / 2;

                if (list[mid].Date.ToLongDateString() == dateTime.ToLongDateString())
                {

                    return mid;

                }

                if (dateTime.CompareTo(list[mid].Date) < 0)
                {

                    low = mid + 1;

                }
                else
                {

                    high = mid;

                }

            }

            return low;

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

            List<TempBudget> tempBudgetList;
            // Đọc dữ liệu các ngân sách từ data
            xs = new XmlSerializer(typeof(List<TempBudget>));
            try
            {
                using (var reader = new StreamReader(@"Data\Budget.xml"))
                {
                    tempBudgetList = (List<TempBudget>)xs.Deserialize(reader);
                }
            }
            catch
            {
                tempBudgetList = new List<TempBudget>();
            }

            //Sao chép dữ liệu sang danh sách ngân sách sử dụng kiểu Datetime
            budgetList = new List<Budget>();

            foreach (var budget in tempBudgetList)
            {

                budgetList.Add(new Budget
                {

                    Balance = budget.Balance,
                    DaysLeft = budget.DaysLeft,
                    EndDate = DateTime.Parse(budget.EndDate),
                    ExpectedSpendingMoney = budget.ExpectedSpendingMoney,
                    ID = budget.ID,
                    ImagePath = budget.ImagePath,
                    MoneyFund = budget.MoneyFund,
                    Name = budget.Name,
                    Note = budget.Note,
                    Progress = budget.Progress,
                    RealitySpending_DayMoney = budget.RealitySpending_DayMoney,
                    ShouldSpending_DayMoney = budget.ShouldSpending_DayMoney,
                    SpentMoney = budget.SpentMoney,
                    StartingDate = DateTime.Parse(budget.StartingDate),

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

            this.DataContext = this;
            ViewReportTextBlock.Foreground = TitleBar.Background;
            ViewTransactionTextBlock.Foreground = TitleBar.Background;

            ListColor = new BindingList<ColorSetting>
            {
                new ColorSetting { Color = "#4D0400"}, new ColorSetting { Color = "#7A0600"}, new ColorSetting { Color = "#A80900"}, new ColorSetting { Color = "#D60B00"}, new ColorSetting { Color = "#FF1205"}, new ColorSetting { Color = "#FF3D33"},new ColorSetting { Color = "#FF6961"},
                new ColorSetting { Color = "#AC001E"}, new ColorSetting { Color = "#D90026"}, new ColorSetting { Color = "#FF0833"}, new ColorSetting { Color = "#FF3659"}, new ColorSetting { Color = "#FF647F"}, new ColorSetting { Color = "#FF92A5"},new ColorSetting { Color = "#FFC0CB"},
                new ColorSetting { Color = "#7D7D02"}, new ColorSetting { Color = "#AAAA03"}, new ColorSetting { Color = "#D7D704"}, new ColorSetting { Color = "#FAFA0F"}, new ColorSetting { Color = "#FBFB3C"}, new ColorSetting { Color = "#FCFC69"},new ColorSetting { Color = "#FDFD96"},
                new ColorSetting { Color = "#51087E"}, new ColorSetting { Color = "#6C0BA9"}, new ColorSetting { Color = "#880ED4"}, new ColorSetting { Color = "#A020F0"}, new ColorSetting { Color = "#B24BF3"}, new ColorSetting { Color = "#C576F6"},new ColorSetting { Color = "#D7A1F9"},
                new ColorSetting { Color = "#0D340D"}, new ColorSetting { Color = "#165816"}, new ColorSetting { Color = "#1F7D1F"}, new ColorSetting { Color = "#28A228"}, new ColorSetting { Color = "#32C732"}, new ColorSetting { Color = "#52D452"},new ColorSetting { Color = "#77DD77"},
                new ColorSetting { Color = "#273B42"}, new ColorSetting { Color = "#38555F"}, new ColorSetting { Color = "#496E7C"}, new ColorSetting { Color = "#5B8899"}, new ColorSetting { Color = "#749DAD"}, new ColorSetting { Color = "#91B2BE"},new ColorSetting { Color = "#AEC6CF"},
                new ColorSetting { Color = "#0A0A0A"}, new ColorSetting { Color = "#212121"}, new ColorSetting { Color = "#383838"}, new ColorSetting { Color = "#4F4F4F"}, new ColorSetting { Color = "#666666"}, new ColorSetting { Color = "#7D7D7D"},new ColorSetting { Color = "#949494"}
            };

            //Binding dữ liệu màu cho Setting Color Table
            SettingColorItemsControl.ItemsSource = ListColor;
            //
            ColorScheme = ConfigurationManager.AppSettings["ColorScheme"];

            //Default buttons
            AddBudgetButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            AddTransactionButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            clickedButton = TransactionsButton;

        }

        private void InitDebtReportData(List<DetailCategory> list, TextBlock textBlock, TextBlock leftTextBlock)
        {

            //Tổng tiền và số dư
            double sum, left;
            //Định dạng lại số tiền ở dạng chuỗi và truyền vào màn hình
            Modal.MoneyConverter moneyConverter = new Modal.MoneyConverter();

            //Tính tổng số tiền nợ người khác
            sum = SumComponent(list);

            //Tính toán số dư
            if (list.Count == 0)
            {

                left = 0;

            }
            else
            {

                left = list[0].Amount;

                if (list.Count == 2)
                {

                    left -= list[1].Amount;

                }

            }

            //Tính toán số dư
            var money = (string)moneyConverter.Convert(left, null, null, null);

            //Hiển thị số tiền nợ
            textBlock.Text = (string)moneyConverter.Convert(sum, null, null, null);
            //Hiển thị số dư
            leftTextBlock.Text = $"{money} left";







            //Tạo dữ liệu màu cho ListColor
            //ListColor = new BindingList<ColorSetting>
            //{
            //    new ColorSetting { Color = "#FFCA5010"}, new ColorSetting { Color = "#FFFF8C00"}, new ColorSetting { Color = "#FFE81123"}, new ColorSetting { Color = "#FFD13438"}, new ColorSetting { Color = "#FFFF4081"},
            //    new ColorSetting { Color = "#FFC30052"}, new ColorSetting { Color = "#FFBF0077"}, new ColorSetting { Color = "#FF9A0089"}, new ColorSetting { Color = "#FF881798"}, new ColorSetting { Color = "#FF744DA9"},
            //    new ColorSetting { Color = "#FF4CAF50"}, new ColorSetting { Color = "#FF10893E"}, new ColorSetting { Color = "#FF018574"}, new ColorSetting { Color = "#FF03A9F4"}, new ColorSetting { Color = "#FF304FFE"},
            //    new ColorSetting { Color = "#FF0063B1"}, new ColorSetting { Color = "#FF6B69D6"}, new ColorSetting { Color = "#FF8E8CD8"}, new ColorSetting { Color = "#FF8764B8"}, new ColorSetting { Color = "#FF038387"},
            //    new ColorSetting { Color = "#FF525E54"}, new ColorSetting { Color = "#FF7E735F"}, new ColorSetting { Color = "#FF9E9E9E"}, new ColorSetting { Color = "#FF515C6B"}, new ColorSetting { Color = "#FF000000"}
            //};

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
            SaveData();

            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ColorScheme"].Value = ColorScheme;
            config.Save(ConfigurationSaveMode.Minimal);

            Application.Current.Shutdown();

        }

        private void SaveData()
        {

            //Sao chép dữ liệu sang danh sách ngân sách sử dụng kiểu chuỗi ngày tháng
            var tempBudgetList = new List<TempBudget>();

            foreach (var budget in budgetList)
            {

                tempBudgetList.Add(new TempBudget
                {

                    Balance = budget.Balance,
                    DaysLeft = budget.DaysLeft,
                    EndDate = budget.EndDate.ToString(),
                    ExpectedSpendingMoney = budget.ExpectedSpendingMoney,
                    ID = budget.ID,
                    ImagePath = budget.ImagePath,
                    MoneyFund = budget.MoneyFund,
                    Name = budget.Name,
                    Note = budget.Note,
                    Progress = budget.Progress,
                    RealitySpending_DayMoney = budget.RealitySpending_DayMoney,
                    ShouldSpending_DayMoney = budget.ShouldSpending_DayMoney,
                    SpentMoney = budget.SpentMoney,
                    StartingDate = budget.StartingDate.ToString()

                });

            }

            XmlSerializer xs = new XmlSerializer(typeof(List<TempBudget>));
            TextWriter writer = new StreamWriter(@"Data\Budget.xml");
            xs.Serialize(writer, tempBudgetList);

            //Sao chép dữ liệu sang danh sách giao dịch sử dụng kiểu Datetime
            var tempTransactionList = new List<TempTransaction>();

            foreach (var transaction in transactionList)
            {

                tempTransactionList.Add(new TempTransaction
                {
                    Amount = transaction.Amount,
                    Date = transaction.Date.ToString(),
                    ID = transaction.ID,
                    Note = transaction.Note,
                    Stakeholder = transaction.Stakeholder,
                    TransactionType = transaction.TransactionType
                });

            }

            xs = new XmlSerializer(typeof(List<TempTransaction>));
            writer = new StreamWriter(@"Data\Transaction.xml");
            xs.Serialize(writer, tempTransactionList);

            writer.Close();

        }

        //Cài đặt nút ẩn cửa sổ
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;

        }

        //Thay đổi kiểu xem danh sách giao dịch
        private void ChangeTransactionViewButton_Click(object sender, RoutedEventArgs e)
        {

            if (CategoryListScrollView.Visibility == Visibility.Visible)
            {

                CategoryListScrollView.Visibility = Visibility.Collapsed;
                TransactionListScrollView.Visibility = Visibility.Visible;
                var imgName = "Images/view_by_transaction.png";
                ChangeImage(imgName, ChangeTransactionViewImage);

            }
            else
            {

                CategoryListScrollView.Visibility = Visibility.Visible;
                TransactionListScrollView.Visibility = Visibility.Collapsed;
                var imgName = "Images/view_by_category.png";
                ChangeImage(imgName, ChangeTransactionViewImage);

            }

        }

        //Quay về danh sách giao dịch ở tháng hiện tại
        private void JumpToTodayButton_Click(object sender, RoutedEventArgs e)
        {

            //Chuyển nút hiện tại sang trạng thái được chọn
            ChangeButtonStatus(CurrentDash, CurrentTextBlock, true);
            //Chuyển nút tiếp theo sang trạng thái không được chọn
            ChangeButtonStatus(NextDash, NextTextBlock, false);

            //Khoảng thời gian là tháng hiện tại
            startingDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            endDate = startingDate.AddMonths(1).AddDays(-1);

            //Thay đổi tên các nút hiển thị khoảng thời gian
            ChangeTitleOfDateButton();

        }

        private void TransactionsButton_Click(object sender, RoutedEventArgs e)
        {

            //Đóng tất cả các màn hình khác
            ReportScreenGrid.Visibility = Visibility.Collapsed;
            BudgetScreenGrid.Visibility = Visibility.Collapsed;
            SettingScreenStackPanel.Visibility = Visibility.Collapsed;
            AboutScreenStackPanel.Visibility = Visibility.Collapsed;
            AddBudgetButton.Visibility = Visibility.Collapsed;

            //Mở màn hình giao dịch
            TransactionScreenGrid.Visibility = Visibility.Visible;
            AddTransactionButton.Visibility = Visibility.Visible;
            UtilityButtonsStackPanel.Visibility = Visibility.Visible;
            AddTextBlock.Text = "ADD TRANSACTION";

            //Đọc dữ liệu tất cả các giao dịch thông thường vào danh sách giao dịch
            categoryCollection = new ObservableCollection<CategoryList>();
            transactionCollection = new ObservableCollection<TransactionList>();
            isDebtTransaction = false;

            //Hiển thị danh sách giao dịch của tháng hiện tại
            JumpToTodayButton_Click(null, new RoutedEventArgs());

            ChangeButtonColor(TransactionsButton);

        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {

            var button = (Button)sender;
            //Đóng tất cả các màn hình khác
            TransactionScreenGrid.Visibility = Visibility.Collapsed;
            BudgetScreenGrid.Visibility = Visibility.Collapsed;
            SettingScreenStackPanel.Visibility = Visibility.Collapsed;
            AboutScreenStackPanel.Visibility = Visibility.Collapsed;
            UtilityButtonsStackPanel.Visibility = Visibility.Collapsed;
            AddBudgetButton.Visibility = Visibility.Collapsed;
            AddTransactionButton.Visibility = Visibility.Collapsed;

            //Đóng khung báo cáo chi tiết
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

            //Trường hợp nhấn nút báo cáo thì hiển thị mặc định là giai đoạn tháng hiện tại
            if (button.Name == "ReportButton")
            {

                startingDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                endDate = startingDate.AddMonths(1).AddDays(-1);

            }

            //Khởi tạo lại dữ liệu báo cáo với giai đoạn tháng hiện tại
            InitializeReportData();

            //Reload dữ liệu của biểu đồ thu nhập và biểu đồ chi tiêu
            IncomeReportChart_IsVisibleChanged(null, new DependencyPropertyChangedEventArgs());
            ExpenseReportChart_IsVisibleChanged(null, new DependencyPropertyChangedEventArgs());

            //Mở màn hình báo cáo
            ReportScreenGrid.Visibility = Visibility.Visible;

            ChangeButtonColor(ReportButton);

        }

        private void BudgetButton_Click(object sender, RoutedEventArgs e)
        {

            //Đóng tất cả các màn hình khác
            TransactionScreenGrid.Visibility = Visibility.Collapsed;
            ReportScreenGrid.Visibility = Visibility.Collapsed;
            SettingScreenStackPanel.Visibility = Visibility.Collapsed;
            AboutScreenStackPanel.Visibility = Visibility.Collapsed;
            AddTransactionButton.Visibility = Visibility.Collapsed;
            UtilityButtonsStackPanel.Visibility = Visibility.Collapsed;

            //Đóng khung báo cáo chi tiết
            DetailBudgetGrid.Visibility = Visibility.Collapsed;

            //Phóng to kích thước của khung ngân sách chung
            Budget.Width = 600;
            BudgetListBorder.Width = 600;

            //Mở màn hình ngân sách
            BudgetScreenGrid.Visibility = Visibility.Visible;
            AddBudgetButton.Visibility = Visibility.Visible;

            ChangeButtonColor(BudgetButton);

        }

        private void DebtsButton_Click(object sender, RoutedEventArgs e)
        {

            //Đóng tất cả các màn hình khác
            ReportScreenGrid.Visibility = Visibility.Collapsed;
            BudgetScreenGrid.Visibility = Visibility.Collapsed;
            SettingScreenStackPanel.Visibility = Visibility.Collapsed;
            AboutScreenStackPanel.Visibility = Visibility.Collapsed;
            AddBudgetButton.Visibility = Visibility.Collapsed;

            //Mở màn hình giao dịch
            TransactionScreenGrid.Visibility = Visibility.Visible;
            AddTransactionButton.Visibility = Visibility.Visible;
            UtilityButtonsStackPanel.Visibility = Visibility.Visible;
            AddTextBlock.Text = "ADD DEBTS";

            //Đọc dữ liệu tất cả các giao dịch vay nợ vào danh sách giao dịch
            categoryDebtCollection = new ObservableCollection<CategoryList>();
            transactionDebtCollection = new ObservableCollection<TransactionList>();
            isDebtTransaction = true;

            //Hiển thị danh sách giao dịch của tháng hiện tại
            JumpToTodayButton_Click(null, new RoutedEventArgs());

            ChangeButtonColor(DebtsButton);

        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {

            //Đóng tất cả các màn hình khác
            ReportScreenGrid.Visibility = Visibility.Collapsed;
            BudgetScreenGrid.Visibility = Visibility.Collapsed;
            TransactionScreenGrid.Visibility = Visibility.Collapsed;
            AboutScreenStackPanel.Visibility = Visibility.Collapsed;
            UtilityButtonsStackPanel.Visibility = Visibility.Collapsed;
            AddBudgetButton.Visibility = Visibility.Collapsed;
            AddTransactionButton.Visibility = Visibility.Collapsed;

            //Mở màn hình cài đặt
            SettingScreenStackPanel.Visibility = Visibility.Visible;

            ChangeButtonColor(SettingButton);

        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {

            //Đóng tất cả các màn hình khác
            ReportScreenGrid.Visibility = Visibility.Collapsed;
            BudgetScreenGrid.Visibility = Visibility.Collapsed;
            TransactionScreenGrid.Visibility = Visibility.Collapsed;
            SettingScreenStackPanel.Visibility = Visibility.Collapsed;
            UtilityButtonsStackPanel.Visibility = Visibility.Collapsed;
            AddBudgetButton.Visibility = Visibility.Collapsed;
            AddTransactionButton.Visibility = Visibility.Collapsed;

            //Mở màn hình cài đặt
            AboutScreenStackPanel.Visibility = Visibility.Visible;

            ChangeButtonColor(AboutButton);

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

            //XmlSerializer xs = new XmlSerializer(typeof(List<Report>));
            //TextWriter writer = new StreamWriter(@"Data\Report.xml");
            //xs.Serialize(writer, reports);
            //writer.Close();

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

            PieChartIconTextBlock.Background = TitleBar.Background;
            ColumnChartIconTextBlock.Background = Brushes.White;

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

            ColumnChartIconTextBlock.Background = TitleBar.Background;
            PieChartIconTextBlock.Background = Brushes.White;

            //Hiển thị biểu đồ hình cột
            DynamicColumnChart.Visibility = Visibility.Visible;
            DynamicColumnChartTextBlock.Visibility = Visibility.Visible;
            //Ẩn biểu đồ hình tròn
            DynamicPieChart.Visibility = Visibility.Collapsed;
            DynamicPieChartTextBlock.Visibility = Visibility.Collapsed;

        }

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

            var arr = GetTransactionType(detailCategory.ID);

            //Trường hợp hiển thị danh sách giao dịch của một loại trong một tháng cụ thể
            if (arr.Length == 1)
            {

                var detail = detailStack.Peek();

                foreach (var category in categoryList)
                {

                    if (category.Value.Name == detail.Title)
                    {

                        var id = category.Value.ID[0] - '0';
                        var isDebt = id > 1;
                        BudgetDetail win = new BudgetDetail()
                        {

                            isDebtTransaction = isDebt,
                            categoryList = categoryList,
                            transactionList = transactionList,
                            startingDate = startingDate,
                            endDate = endDate,
                            transactionType = category.Value.ID,
                            title = detailCategory.Name

                        };

                        win.Show();

                    }

                }

            }
            else if (arr.Length == 2) //Trường hợp khung chi tiết loại nhóm cha
            {

                var valuePairs = new Dictionary<string, double>();
                var check = false;

                //Đọc qua tất cả các giao dịch để tìm các chi tiết loại nhóm con
                foreach (Transaction transaction in transactionList)
                {

                    if (startingDate.Date <= transaction.Date && transaction.Date <= endDate)
                    {

                        var type = transaction.TransactionType;
                        var index = type.IndexOf(detailCategory.ID);

                        //Nhóm con thì thêm dữ liệu vào từ điển
                        if ((detailStack.Count == 1 && index == 0) ||
                            (detailStack.Count > 1 && index == 0 && type != detailCategory.ID))
                        {

                            AddDataIntoDictionary(valuePairs, type, transaction.Amount, false);
                            if (detailCategory.ID != type)
                            {

                                check = true;

                            }

                        }

                    }

                }

                //Nếu có từ 2 nhóm con trở lên thì hiển thị khung chi tiết loại nhóm con
                if (check == true)
                {

                    List<DetailCategory> detailList = new List<DetailCategory>();

                    //Chuyển dữ liệu 4 nhóm giao dịch vào danh sách
                    AddDataIntoList(valuePairs, detailList);

                    //Đổi tên tiêu đề khung báo cáo chi tiết
                    DetailReportGrid.DataContext = AddDataIntoDetailReport(detailCategory.Name, detailList);

                }
                else
                {

                    //Nếu là nhóm con thì hiển thị biểu đồ theo tháng
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

                if (startingDate.Date <= transaction.Date && transaction.Date <= endDate)
                {

                    var index = transaction.TransactionType.IndexOf(detailCategory.ID);

                    //Giao dịch phải thỏa điều kiện cùng ID với ID của tham số đầu vào
                    if (index == 0)
                    {

                        //Cộng dồn số tiền giao dịch trong tháng
                        amountPerMonth[transaction.Date.Month - 1] += transaction.Amount;

                    }

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
            AddTransaction addScreen = new AddTransaction(ColorScheme);

            if (clickedButton == TransactionsButton)
            {

                addScreen.isTransaction = true;

            }
            else
            {

                addScreen.isTransaction = false;

            }

            addScreen.ColorScheme = ColorScheme;
            // Reset lại dữ liệu khi tạo một giao dịch mới
            AddTransaction.Global.tempDate = default(DateTime);
            AddTransaction.Global.tempAmount = "";
            AddTransaction.Global.tempStakeholder = "";
            AddTransaction.Global.tempNote = "";
            AddTransaction.Global.tempTransactionType = "";

            addScreen.Handler += Screen_Handler;

            addScreen.Show();
        }

        private void Screen_Handler(Transaction transaction, Category category)
        {

            transactionList.Add(transaction);
            AddDataIntoTransactionScreen();
            AddDataIntoBudgetScreen();

        }

        private void AddBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            AddBudget addScreen = new AddBudget(ColorScheme);
            addScreen.ColorScheme = ColorScheme;
            // Reset lại dữ liệu khi tạo một giao dịch mới            
            AddBudget.Global.tempStartingDate = default(DateTime);
            AddBudget.Global.tempEndDate = default(DateTime);
            AddBudget.Global.tempMoneyFund = "";
            AddBudget.Global.tempNote = "";
            //AddBudget.Global.tempTransactionType = "";

            addScreen.Handler += BudgetScreen_Handler;

            addScreen.Show();
        }

        private void BudgetScreen_Handler(Budget budget)
        {

            budgetList.Add(budget);
            AddDataIntoBudgetScreen();

        }

        private void CloseDetailBudget_Click(object sender, RoutedEventArgs e)
        {
            DetailBudgetGrid.Visibility = Visibility.Collapsed;
            Budget.Width = 600;
            BudgetListBorder.Width = 600;
        }

        private void RunningButton_Click(object sender, RoutedEventArgs e)
        {
            // Chỉnh lại định dạng nút cho nổi bật
            RunningTextblock.Foreground = ChangeHexToBrushColor(ColorScheme);
            RunningTextblock.FontSize = 20;
            RunningButton.BorderThickness = new Thickness(0, 0, 0, 1);
            RunningButton.BorderBrush = ChangeHexToBrushColor(ColorScheme);
            RunningUnderlineTextBlock.Visibility = Visibility.Visible;

            // Chỉnh lại định dạng nút còn lại thành bình thường
            FinishedTextblock.Foreground = ChangeHexToBrushColor("#757575");
            FinishedButton.BorderThickness = new Thickness(0, 0, 0, 0);
            FinishedTextblock.FontSize = 15;
            FinishedUnderlineTextBlock.Visibility = Visibility.Collapsed;

            // Cập nhật danh sách những ngân sách đã quá hạn
            BudgetList.ItemsSource = runningBudgetList;

        }

        private void FinishedButton_Click(object sender, RoutedEventArgs e)
        {
            // Chỉnh lại định dạng nút cho nổi bật
            FinishedTextblock.Foreground = ChangeHexToBrushColor(ColorScheme);
            FinishedTextblock.FontSize = 20;
            FinishedButton.BorderThickness = new Thickness(0, 0, 0, 1);
            FinishedButton.BorderBrush = ChangeHexToBrushColor(ColorScheme);
            FinishedUnderlineTextBlock.Visibility = Visibility.Visible;

            // Chỉnh lại định dạng nút còn lại thành bình thường
            RunningTextblock.Foreground = ChangeHexToBrushColor("#757575");
            RunningButton.BorderThickness = new Thickness(0, 0, 0, 0);
            RunningTextblock.FontSize = 15;
            RunningUnderlineTextBlock.Visibility = Visibility.Collapsed;

            // Cập nhật danh sách những ngân sách đã quá hạn
            BudgetList.ItemsSource = finishedBudgetList;

        }

        private void ViewTransactionListButton_Click(object sender, RoutedEventArgs e)
        {
            var id = budgetInfo.ID[0] - '0';
            var isDebt = id > 1;

            BudgetDetail win = new BudgetDetail()
            {

                isDebtTransaction = isDebt,
                categoryList = categoryList,
                transactionList = transactionList,
                startingDate = budgetInfo.StartingDate,
                endDate = budgetInfo.EndDate,
                transactionType = budgetInfo.ID,
                title = budgetInfo.Name

            };
            win.ColorScheme = ColorScheme;
            win.Handler += UpdateUI;

            win.Show();
        }

        private void UpdateUI()
        {

            AddDataIntoTransactionScreen();
            AddDataIntoBudgetScreen();
            ViewTransactionListButton_Click(null, new RoutedEventArgs());
            var button = new Button()
            {

                DataContext = budgetInfo

            };
            CreateBudgetLineChart(button);

        }

        // Nút chuyển sang tháng trước trong giao diện giao dịch
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {

            //Trường hợp đang ở trạng thái tháng tương lai
            if (CurrentDash.Background != ChangeHexToBrushColor(ColorScheme))
            {

                //Chuyển nút hiện tại sang trạng thái được chọn
                ChangeButtonStatus(CurrentDash, CurrentTextBlock, true);
                //Chuyển nút tiếp theo sang trạng thái không được chọn
                ChangeButtonStatus(NextDash, NextTextBlock, false);

                //Giảm khoảng thời gian bắt đầu xuống tháng hiện tại
                startingDate = startingDate.AddMonths(-1);

            }

            //Giảm khoảng thời gian bắt đầu xuống tháng trước
            startingDate = startingDate.AddMonths(-1);
            endDate = startingDate.AddMonths(1).AddDays(-1);

            //Thay đổi tên các nút hiển thị khoảng thời gian
            ChangeTitleOfDateButton();

        }

        // Nút chuyển sang tháng hiện tại trong giao diện giao dịch
        private void CurrentButton_Click(object sender, RoutedEventArgs e)
        {

            //Trường hợp đang ở trạng thái tháng tương lai
            if (CurrentDash.Background != ChangeHexToBrushColor(ColorScheme))
            {

                //Chuyển nút hiện tại sang trạng thái được chọn
                ChangeButtonStatus(CurrentDash, CurrentTextBlock, true);
                //Chuyển nút tiếp theo sang trạng thái không được chọn
                ChangeButtonStatus(NextDash, NextTextBlock, false);

                //Khoảng thời gian là tháng hiện tại
                startingDate = startingDate.AddMonths(-1);
                endDate = startingDate.AddMonths(1).AddDays(-1);

                //Thay đổi tên các nút hiển thị khoảng thời gian
                ChangeTitleOfDateButton();

            }

        }

        // Nút chuyển sang tháng tiếp theo của Nút tháng hiện tại
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

            //Trường hợp không ở trạng thái tháng tương lai
            if (NextDash.Background != ChangeHexToBrushColor(ColorScheme))
            {

                //Tăng thời gian bắt đầu lên 1 tháng
                startingDate = startingDate.AddMonths(1);
                endDate = startingDate.AddMonths(1).AddDays(-1);

                //Thay đổi tên các nút hiển thị khoảng thời gian
                ChangeTitleOfDateButton();

            }

        }

        //Thay đổi tiêu đề của 3 nút trạng thái thời gian
        private void ChangeTitleOfDateButton()
        {

            //Thời gian bắt đầu của tháng hiện tại theo thời gian thực
            var startingOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            //Thời gian bắt đầu của tháng trước
            var lastStartingDate = startingDate.AddMonths(-1);
            //Thời gian kết thúc của tháng trước
            var lastEndDate = lastStartingDate.AddMonths(1).AddDays(-1);
            //Thời gian bắt đầu của tháng tiếp theo
            var nextStartingDate = startingDate.AddMonths(1);
            //Thời gian kết thúc của tháng tiếp theo
            var nextEndDate = nextStartingDate.AddMonths(1).AddDays(-1);

            //Hiển thị tên 3 nút
            PreviousTextBlock.Text =
                $"{lastStartingDate.Day}/{lastStartingDate.Month}/{lastStartingDate.Year}" +
                $" - {lastEndDate.Day}/{lastEndDate.Month}/{lastEndDate.Year}";

            CurrentTextBlock.Text =
                $"{startingDate.Day}/{startingDate.Month}/{startingDate.Year}" +
                $" - {endDate.Day}/{endDate.Month}/{endDate.Year}";

            NextTextBlock.Text =
                $"{nextStartingDate.Day}/{nextStartingDate.Month}/{nextStartingDate.Year}" +
                $" - {nextEndDate.Day}/{nextEndDate.Month}/{nextEndDate.Year}";

            //Kiểm tra có phải là 2 tháng trước hay không
            if (startingDate == startingOfThisMonth.AddMonths(-2))
            {

                //Tháng tiếp theo chính là tháng trước
                NextTextBlock.Text = "LAST MONTH";

            }
            //Kiểm tra có phải là tháng trước hay không
            else if (startingDate == startingOfThisMonth.AddMonths(-1))
            {

                //Tháng hiện tại chính là tháng trước
                CurrentTextBlock.Text = "LAST MONTH";
                //Tháng tiếp theo chính là tháng này
                NextTextBlock.Text = "THIS MONTH";

            }
            //Kiểm tra có phải là tháng hiện tại trở đi hay không
            else if (startingDate >= startingOfThisMonth)
            {

                PreviousTextBlock.Text = "LAST MONTH";
                CurrentTextBlock.Text = "THIS MONTH";
                NextTextBlock.Text = "FUTURE";

                //Đang ở trạng thái tháng tương lai
                if (startingDate > startingOfThisMonth)
                {

                    //Thời gian kết thúc là vô cùng
                    endDate = DateTime.MaxValue;

                    //Chuyển nút hiện tại sang trạng thái không được chọn
                    ChangeButtonStatus(CurrentDash, CurrentTextBlock, false);
                    //Chuyển nút tiếp theo sang trạng thái được chọn
                    ChangeButtonStatus(NextDash, NextTextBlock, true);

                }

            }

            //Đưa dữ liệu vào màn hình danh sách giao dịch
            AddDataIntoTransactionScreen();

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

        private void BudgetListButton_Click(object sender, RoutedEventArgs e)
        {

            //Hiển thị màn hình chi tiết ngân sách
            DetailBudgetGrid.Visibility = Visibility.Visible;
            Budget.Width = 410;
            BudgetListBorder.Width = 410;
            DetailBudgetGrid.Width = 600;

            CreateBudgetLineChart(sender);

        }

        private void CreateBudgetLineChart(object sender)
        {

            var temp = sender as Button;

            budgetInfo = temp.DataContext as Budget;
            int count = 0;

            //Tìm ngân sách được nhấn trong danh sách ngân sách
            foreach (var budget in budgetList)
            {

                if (budget.ID == budgetInfo.ID)
                {

                    break;

                }
                count++;

            }

            BudgetInfo.DataContext = budgetList[count];
            budgetDocPanel.DataContext = budgetList[count];

            // Hiển thị dữ liệu cho biểu đồ(nên gôp với hàm tính số tiền đã chi tiêu cho ngân sách, nếu được thì chỉ cập nhật sau khi CRUD 1 giao dịch)
            BudgetLineChart.Series.Clear();

            TimeSpan time = budgetInfo.EndDate - budgetInfo.StartingDate;
            int durationOfBudget = time.Days + 1;

            //Đường giới hạn ngân sách
            List<double> budgetGoalLine = new List<double>();
            //Đường chi tiêu thực tế
            List<double> budgetSpentLine = new List<double>();

            // Lưu <ngày, số tiền chi tiêu> trong các giao dịch 
            Dictionary<DateTime, double> myDic = new Dictionary<DateTime, double>();
            double money = 0;

            //Tính toán từ điển <ngày, số tiền chi tiêu>
            for (int i = 0; i < transactionList.Count; i++)
            {

                var index = transactionList[i].TransactionType.IndexOf(budgetInfo.ID);

                if (transactionList[i].Date <= budgetInfo.EndDate &&
                    transactionList[i].Date >= budgetInfo.StartingDate && index == 0)
                {

                    var date = new DateTime
                        (
                            transactionList[i].Date.Year,
                            transactionList[i].Date.Month,
                            transactionList[i].Date.Day
                        );

                    if (myDic.ContainsKey(date))
                    {

                        myDic[date] = myDic[date] + transactionList[i].Amount;

                    }
                    else
                    {

                        myDic.Add(date, transactionList[i].Amount);

                    }

                }
            }

            //Duyệt qua hết tất cả các ngày trong khoảng thời gian của ngân sách
            for (int i = 0; i < durationOfBudget; i++)
            {

                var currentDate = budgetInfo.StartingDate.AddDays(i);
                var startingCurrentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

                // nếu có ngày trong danh sách giao dịch thì thêm vào
                if (myDic.ContainsKey(startingCurrentDate))
                {

                    money += myDic[startingCurrentDate];

                }

                if (startingCurrentDate <= DateTime.Today)
                {

                    //Đường chi tiêu thực tế
                    budgetSpentLine.Add(money);

                }

                //Đường ngân sách giới hạn
                budgetGoalLine.Add(budgetInfo.MoneyFund);

            }

            //Tạo đường chi tiêu thực tế
            BudgetLineChart.Series.Add(new LineSeries()
            {

                Values = new ChartValues<double>(budgetSpentLine),
                LineSmoothness = 0,
                PointGeometry = null,
                PointGeometrySize = 10,
                Title = "Current"

            });

            //Tạo đường giới hạn ngân sách
            BudgetLineChart.Series.Add(new LineSeries()
            {
                Values = new ChartValues<double>(budgetGoalLine),
                LineSmoothness = 0,
                PointGeometry = null,
                PointGeometrySize = 0,
                Title = "Maximum"

            });

            // Xóa Axisx
            BudgetLineChart.AxisX.Add(new Axis
            {
                Labels = new string[0]
            });

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

        private void ChangeButtonColor(Button button)
        {

            //Tắt màu của nút hiện tại
            var stackpanel = (StackPanel)clickedButton.Content;
            var collection = stackpanel.Children;
            var image = (Image)collection[0];
            var text = (TextBlock)collection[1];
            var button_name = clickedButton.Name.Replace("Button", "").ToLower();

            image.Source = new BitmapImage(new Uri($"Images/{button_name}.png",
                        UriKind.Relative));
            text.Foreground = Brushes.Black;
            clickedButton.Background = Brushes.White;

            //Hiển thị màu cho nút vừa được nhấn
            stackpanel = (StackPanel)button.Content;
            collection = stackpanel.Children;
            image = (Image)collection[0];
            button_name = button.Name;
            button_name = button_name.Replace("Button", "").ToLower();
            button_name = "white_" + button_name;
            image.Source = new BitmapImage(new Uri($"Images/{button_name}.png",
                       UriKind.Relative));
            text = (TextBlock)collection[1];

            text.Foreground = Brushes.White;
            button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);

            //Cập nhật nút mới
            clickedButton = button;
        }

        //Xử lý hiển thị chi tiết giao dịch từ màn hình giao dịch
        private void CategoryListDetail_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var listView = sender as ListView;
            var name = listView.Name;
            var window = new TransactionDetails();
            var transaction = new DetailTransaction();

            if (name == "TransactionListDetail")
            {

                transaction = listView.SelectedItem as DetailTransaction;

            }
            else
            {

                var tempTransaction = listView.SelectedItem as Transaction;
                var type = tempTransaction.TransactionType;

                transaction = new DetailTransaction()
                {

                    Amount = tempTransaction.Amount,
                    Date = tempTransaction.Date,
                    ImagePath = categoryList[type].ImagePath,
                    Name = categoryList[type].Name,
                    Note = tempTransaction.Note,
                    Stakeholder = tempTransaction.Stakeholder,
                    TransactionType = tempTransaction.TransactionType

                };

            }

            window.detailTransaction = transaction;
            window.tempDetailTransaction.Add(transaction);

            window.ColorScheme = ColorScheme;
            window.Handler += DetailTransaction_Screen_Handler;

            window.Show();

        }

        private void DetailTransaction_Screen_Handler(DetailTransaction transaction, bool isDeleted)
        {

            var index = transactionList.FindIndex(element => element.ID == transaction.ID);

            if (isDeleted == true)
            {

                transactionList.RemoveAt(index);

            }
            else
            {

                if (index != -1)
                {

                    transactionList[index] = transaction;

                }

            }
            AddDataIntoTransactionScreen();
            AddDataIntoBudgetScreen();

        }

        //Xóa ngân sách
        private void DeleteBudgetButton_Click(object sender, RoutedEventArgs e)
        {

            var noti = MessageBox.Show("Are you really want to delete this budget?",
                    "Notification",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (noti == MessageBoxResult.Yes)
            {

                var budget = BudgetInfo.DataContext as Budget;
                budgetList.Remove(budget);
                AddDataIntoBudgetScreen();
                CloseDetailBudget_Click(null, new RoutedEventArgs());

            }
            else
            {

                // Do nothing

            }

        }

        private void EditBudgetButton_Click(object sender, RoutedEventArgs e)
        {

            var budget = BudgetInfo.DataContext as Budget;
            AddBudget addScreen = new AddBudget(ColorScheme);
            addScreen.Category = new Category()
            {

                ID = budget.ID,
                ImagePath = budget.ImagePath,
                Name = budget.Name

            };
            // Reset lại dữ liệu khi tạo một giao dịch mới            
            AddBudget.Global.tempStartingDate = budget.StartingDate;
            AddBudget.Global.tempEndDate = budget.EndDate;
            AddBudget.Global.tempMoneyFund = budget.MoneyFund.ToString();
            AddBudget.Global.tempNote = budget.Note;
            //AddBudget.Global.tempTransactionType = "";

            addScreen.Handler += EditBudgetScreen_Handler;

            addScreen.Show();
        }

        private void EditBudgetScreen_Handler(Budget budget)
        {

            var editedBudget = BudgetInfo.DataContext as Budget;
            var index = budgetList.FindIndex(element => element.ID == editedBudget.ID);

            if (index != -1)
            {

                budgetList[index] = budget;

            }

            AddDataIntoBudgetScreen();
            var button = new Button()
            {

                DataContext = budget

            };
            CreateBudgetLineChart(button);

        }



        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            var datatContex = (sender as Button).DataContext;
            var color = (datatContex as ColorSetting).Color;
            ColorScheme = color;
            TitleBar.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            ViewReportTextBlock.Foreground = TitleBar.Background;
            ViewTransactionTextBlock.Foreground = TitleBar.Background;
            RunningTextblock.Foreground = TitleBar.Background;
            RunningUnderlineTextBlock.Background = TitleBar.Background;
            FinishedUnderlineTextBlock.Background = TitleBar.Background;
            FinishedTextblock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#757575");
            PieChartIconTextBlock.Background = TitleBar.Background;
            ColumnChartIconTextBlock.Background = TitleBar.Background;
            PieChartIconTextBlock.Background = TitleBar.Background;
            ColumnChartIconTextBlock.Background = Brushes.White;

            // Chỉnh lại giao diện nút setting đang được chọn
            SettingButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
            SettingTitleTextBlock.Foreground = Brushes.White;
            var stackpanel = (StackPanel)SettingButton.Content;
            var collection = stackpanel.Children;
            var image = (Image)collection[0];
            image.Source = new BitmapImage(new Uri($"Images/white_setting.png",
                       UriKind.Relative));

            // Cập nhật màu cho các nút chung
            AddBudgetButton.Background = TitleBar.Background;
            AddTransactionButton.Background = TitleBar.Background;
            //AddBudget add = new AddBudget(ColorScheme);
            //AddTransaction add1 = new AddTransaction();
            //add1.ColorScheme = ColorScheme;



        }

    }

}
