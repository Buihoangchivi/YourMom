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
using System.Xml.Serialization;

namespace YourMom
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		int[] arr = { 2, 5, 6, 7, 9, 10 };

		public MainWindow()
		{

			InitializeComponent();

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

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

		private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void CloseDetailButton_Click(object sender, RoutedEventArgs e)
		{

			//Ẩn báo cáo chi tiết
			IncomeReportGrid.Visibility = Visibility.Collapsed;

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

			IncomeReportChart.Series = new SeriesCollection();
			((DefaultTooltip)IncomeReportChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
			foreach (var member in arr)
			{
				IncomeReportChart.Series.Add(
						new PieSeries()
						{
							Values = new ChartValues<decimal> { member }
						}
					); ;
			}

		}

		//Biểu đồ hình quạt về chi tiêu
		private void ExpenseReportChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

			ExpenseReportChart.Series = new SeriesCollection();
			((DefaultTooltip)ExpenseReportChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
			foreach (var member in arr)
			{
				ExpenseReportChart.Series.Add(
						new PieSeries()
						{
							Values = new ChartValues<decimal> { member }
						}
					);
			}

		}

		private void IncomeButton_Click(object sender, RoutedEventArgs e)
		{

			//Hiển thị khung báo cáo chi tiết thu nhập
			IncomeReportGrid.Visibility = Visibility.Visible;

			//Thu nhỏ chiều rộng của khung báo cáo chung
			GeneralReportGrid.Width = 410;

			//Phóng to khung báo cáo chi tiết về thu nhập
			IncomeReportGrid.Width = 600;

			//Thu nhỏ kích thước của 2 biểu đồ hình bánh thể hiện thu chi
			IncomeReportChart.Width = 200;
			IncomeReportChart.Height = 200;
			ExpenseReportChart.Width = 200;
			ExpenseReportChart.Height = 200;

			//Thu nhỏ chiều rộng của 2 nút vay nợ
			DebtDockPanel.Width = 410;
			LoanDockPanel.Width = 410;

			//Mặc định hiển thị biểu đồ hình bánh
			PieChartIconButton.Height = 47.5;
			PieChartIconTextBlock.Visibility = Visibility.Visible;

		}

		private void PieChartIconButton_Click(object sender, RoutedEventArgs e)
		{

			//Hiển thị biểu đồ hình bánh
			PieChartIconButton.Height = 47.5;
			PieChartIconTextBlock.Visibility = Visibility.Visible;

			//Ẩn biểu đồ hình cột
			ColumnChartIconButton.Height = 50;
			ColumnChartIconTextBlock.Visibility = Visibility.Collapsed;

		}

		private void ColumnChartIconButton_Click(object sender, RoutedEventArgs e)
		{

			//Hiển thị biểu đồ hình cột
			ColumnChartIconButton.Height = 47.5;
			ColumnChartIconTextBlock.Visibility = Visibility.Visible;

			//Ẩn biểu đồ hình cột
			PieChartIconButton.Height = 50;
			PieChartIconTextBlock.Visibility = Visibility.Collapsed;

		}
	}
}
