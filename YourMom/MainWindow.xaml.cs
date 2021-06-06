﻿using LiveCharts;
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
		//Cài đặt nút phóng to/ thu nhỏ cửa sổ
		private void MaximizeButton_Click(object sender, RoutedEventArgs e)
		{
			AdjustWindowSize();
		}

		//Cài đặt nút ẩn cửa sổ
		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		//Thay đổi kích thước cửa sổ
		//Nếu đang ở trạng thái phóng to thì thu nhỏ và ngược lại
		private void AdjustWindowSize()
		{
			var imgName = "";

			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
				imgName = "Images/maximize.png";
			}
			else
			{
				WindowState = WindowState.Maximized;
				imgName = "Images/restoreDown.png";
			}

			//Lấy nguồn ảnh
			var img = new BitmapImage(new Uri(
						imgName,
						UriKind.Relative)
				);

			//Thiết lập ảnh chất lượng cao
			RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);

			//Thay đổi icon
			(MaxButton.Content as Image).Source = img;
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
			DetailReportGrid.Visibility = Visibility.Collapsed;
			//Phóng to báo cáo chung
			if (WindowState == WindowState.Normal) //Cửa sổ ở trạng thái bình thường
			{

				GeneralReportGrid.Width = 600;

			}
			else //Cửa sổ ở trạng thái thu nhỏ
			{

				GeneralReportGrid.Width = 800;

			}

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
					); ;
			}

		}
	}
}
