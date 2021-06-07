using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

		private void Button_Click(object sender, RoutedEventArgs e)
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

		}
	}
}
