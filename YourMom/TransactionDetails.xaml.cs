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
    /// Interaction logic for TransactionDetails.xaml
    /// </summary>
    public partial class TransactionDetails : Window
    {

        public DetailTransaction detailTransaction = new DetailTransaction();
        public ObservableCollection<DetailTransaction> tempDetailTransaction = 
            new ObservableCollection<DetailTransaction>();

        public delegate void ModifyDelegate(DetailTransaction transaction, bool isDeleted);
        public event ModifyDelegate Handler;

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

        public TransactionDetails()
        {

            InitializeComponent();

        }

        private void CloseListDetailBudget_Click(object sender, RoutedEventArgs e)
        {

            //Đã chỉnh sửa giao dịch
            if (Handler != null && detailTransaction != tempDetailTransaction[0])
            {

                tempDetailTransaction[0].ID = detailTransaction.ID;
                Handler(tempDetailTransaction[0], false);

            }

            Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DataContext = tempDetailTransaction;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            var noti = MessageBox.Show("Are you really want to delete this transaction?",
                    "Notification",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            //Xóa giao dịch
            if (noti == MessageBoxResult.Yes)
            {

                if (Handler != null)
                {

                    Handler(detailTransaction, true);

                }
                Close();

            }
            else
            {

                // Do nothing

            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            AddTransaction addScreen = new AddTransaction(ColorScheme);
            // Reset lại dữ liệu khi tạo một giao dịch mới
            AddTransaction.Global.tempDate = tempDetailTransaction[0].Date;
            AddTransaction.Global.tempAmount = tempDetailTransaction[0].Amount.ToString();
            AddTransaction.Global.tempStakeholder = tempDetailTransaction[0].Stakeholder;
            AddTransaction.Global.tempNote = tempDetailTransaction[0].Note;
            AddTransaction.Global.tempTransactionType = tempDetailTransaction[0].TransactionType;
            addScreen.Category = new Category()
            {

                ID = tempDetailTransaction[0].TransactionType,
                ImagePath = tempDetailTransaction[0].ImagePath,
                Name = tempDetailTransaction[0].Name

            };

            addScreen.Handler += Screen_Handler;

            addScreen.Show();
        }

        private void Screen_Handler(Transaction transaction, Category category)
        {

            tempDetailTransaction[0] = new DetailTransaction()
            {

                Amount = transaction.Amount,
                Date = transaction.Date,
                ID = transaction.ID,
                ImagePath = category.ImagePath,
                Name = category.Name,
                Note = transaction.Note,
                Stakeholder = transaction.Stakeholder,
                TransactionType = transaction.TransactionType

            };

        }

    }
}
