using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for BudgetDetail.xaml
    /// </summary>
    public partial class BudgetDetail : Window
    {

        ObservableCollection<TransactionList> transactionCollection = new ObservableCollection<TransactionList>();
        ObservableCollection<TransactionList> transactionDebtCollection = new ObservableCollection<TransactionList>();
        public Dictionary<string, Category> categoryList = new Dictionary<string, Category>();
        public List<Transaction> transactionList;
        public string title = "";
        public DateTime startingDate, endDate;
        public string transactionType = "";

        public bool isDebtTransaction;

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

        public BudgetDetail()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Nạp dữ liệu vào màn hình giao dịch

            //Trường hợp đang mở màn hình giao dịch vay nợ
            if (isDebtTransaction == true)
            {

                transactionDebtCollection = new ObservableCollection<TransactionList>();

                //Đọc dữ liệu tất cả các giao dịch vay nợ vào danh sách giao dịch ở dạng giao dịch
                InitDataIntoObservableCollection(transactionDebtCollection, startingDate, endDate);
                TransactionList.ItemsSource = transactionDebtCollection;

            }
            else //Trường hợp đang mở màn hình giao dịch thông thường
            {

                transactionCollection = new ObservableCollection<TransactionList>();

                //Đọc dữ liệu tất cả các giao dịch vào danh sách giao dịch ở dạng giao dịch
                InitDataIntoObservableCollection(transactionCollection, startingDate, endDate);
                TransactionList.ItemsSource = transactionCollection;

            }

            //Hiển thị tiêu đề của cửa sổ
            if (title != "")
            {

                TitleTextBlock.Text = title;

            }
            else
            {

                TitleTextBlock.Text = Name;

            }

        }

        //Hàm khởi tạo dữ liệu theo kiểu giao dịch vào danh sách giao dịch hoặc danh sách vay nợ
        private void InitDataIntoObservableCollection(ObservableCollection<TransactionList> transactions, DateTime startingDate, DateTime endDate)
        {

            //Mảng lưu số tiền thu vào và số tiền chi ra
            double[] amountArray = { 0.0, 0.0 };

            //Đọc qua tất cả các giao dịch
            foreach (var transaction in transactionList)
            {

                var index = transaction.TransactionType.IndexOf(transactionType);

                //Kiểm tra xem thời gian của giao dịch có nằm trong khoảng thời thời đầu vào hay không
                if (startingDate <= transaction.Date && transaction.Date <= endDate && index == 0)
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
                        index = IndexOfCollectionBinarySearch(transactions, transaction.Date);

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

        private void CloseListDetailBudget_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

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

        private void TransactionListDetail_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var listView = sender as ListView;
            var window = new TransactionDetails();
            var transaction = listView.SelectedItem as DetailTransaction;

            window.detailTransaction = transaction;
            window.tempDetailTransaction.Add(transaction);
            window.ColorScheme = ColorScheme;

            window.Handler += DetailTransaction_Screen_Handler;

            window.Show();

        }

        public delegate void UpdateUIDelegate();
        public event UpdateUIDelegate Handler;

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
            
            if (Handler != null)
            {

                Handler();

            }

            Close();
        }

        // Hàm chặn khi auto scrolling lúc nhấn vào vị trí bất kì trong scrollviewer
        private void ScrollViewer_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {

            e.Handled = true;

        }
    }
}
