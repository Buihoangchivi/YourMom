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

		DetailInfomationClass detailInfomation = new DetailInfomationClass();

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

		public MainWindow()
		{

			InitializeComponent();

			detailInfomation.Components = detailCategoryList;

			DetailReportGrid.DataContext = detailInfomation;
			CategoryListView.ItemsSource = detailCategoryList;

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			
			//

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
			var sum = AddDataIntoReportPieChart(IncomeReportChart, detailInfomation);
			IncomeAmountTextBlock.Text = $"+{sum}";

		}

		//Biểu đồ hình quạt về chi tiêu
		private void ExpenseReportChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

			ExpenseReportChart.Series = new SeriesCollection();
			((DefaultTooltip)ExpenseReportChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
			var sum = AddDataIntoReportPieChart(ExpenseReportChart, detailInfomation);
			ExpenseAmountTextBlock.Text = $"+{sum}";

		}

		//Biểu đồ hình quạt linh động
		private void DynamicPieChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

			DynamicPieChart.Series = new SeriesCollection();
			((DefaultTooltip)DynamicPieChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
			var sum = AddDataIntoReportPieChart(DynamicPieChart, detailInfomation);
			DynamicPieChartTextBlock.Text = $"{sum}";

		}

		//Biểu đồ hình cột linh động
		private void DynamicColumnChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

			DynamicColumnChart.Series = new SeriesCollection();
			((DefaultTooltip)DynamicColumnChart.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
			DynamicColumnChart.AxisY = new AxesCollection();
			var sum = AddDataIntoReportColumnChart(DynamicColumnChart, detailInfomation);
			DynamicColumnChartTextBlock.Text = $"{sum}";

		}

		//Hàm thêm dữ liệu vào biểu đồ hình tròn
		private double AddDataIntoReportPieChart(PieChart pieChart, DetailInfomationClass detail)
		{

			//Tinh tổng số tiền của các thành phần
			var sum = 0.0;
			foreach (var component in detail.Components)
			{

				pieChart.Series.Add(
					new PieSeries()
					{
						Values = new ChartValues<double> { component.Amount },
						Title = component.Name
					}
				);
				sum += component.Amount;

			}
			return sum;

		}

		//Hàm thêm dữ liệu vào biểu đồ hình cột
		private double AddDataIntoReportColumnChart(CartesianChart columnChart, DetailInfomationClass detail)
		{

			//Tinh tổng số tiền của các thành phần
			var sum = 0.0;
			foreach (var component in detail.Components)
			{

				columnChart.Series.Add(new ColumnSeries()
				{
					Values = new ChartValues<double> { component.Amount },
					Title = component.Name
				});
				sum += component.Amount;

			}
			return sum;

		}

		private void IncomeButton_Click(object sender, RoutedEventArgs e)
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

			//Mặc định hiển thị biểu đồ hình tròn
			detailInfomation.TypeOfChart = false;
			PieChartIconButton.Height = 46;
			PieChartIconTextBlock.Visibility = Visibility.Visible;
			//Hiển thị biểu đồ hình tròn
			DynamicPieChart.Visibility = Visibility.Visible;
			DynamicPieChartTextBlock.Visibility = Visibility.Visible;
			//Ẩn biểu đồ hình cột
			DynamicColumnChart.Visibility = Visibility.Collapsed;
			DynamicColumnChartTextBlock.Visibility = Visibility.Collapsed;

			//Hiển thị tiêu đề của khung chi tiết thu nhập
			detailInfomation.Title = "Income";

			//Nạp dữ liệu cho khung báo báo thu nhập chi tiết
			detailInfomation.Components = detailCategoryList;
			
			if (detailCategoryList.Count <= 5)
			{

				CategoryListView.Height = detailCategoryList.Count * 65;

			}
			else
			{

				CategoryListView.Height = 5 * 65;

			}
			DetailScrollViewer.ScrollToTop();

		}

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

		private void DetailButton_Click(object sender, RoutedEventArgs e)
		{

			var temp = sender as Button;

			//Đổi tên tiêu đề khung báo cáo chi tiết
			if (temp.DataContext.GetType().Name == "DetailCategory")
			{

				var detailCategory = temp.DataContext as DetailCategory;
				//Hiển thị tiêu đề của khung chi tiết thu nhập
				detailInfomation.Title = detailCategory.Name;
				CategoryListView.ItemsSource = detailCategoryList1;

			}
			
			if (detailCategoryList1.Count <= 5)
			{

				CategoryListView.Height = detailCategoryList1.Count * 65;

			}
			else
			{

				CategoryListView.Height = 5 * 65;

			}
			DetailScrollViewer.ScrollToTop();

		}
	}
}
