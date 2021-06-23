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
	/// 
	public class Test
	{
		public string Image { get; set; }
		public string Text { get; set; }
		public string Text1 { get; set; }
		public string Text2 { get; set; }
	}


	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			List<Test> items = new List<Test>();
			items.Add(new Test() { Image = "Images/category_foodndrink.png", Text = "Ăn uống", Text1 = "9.000.000", Text2 = " Còn lại 8.999.000" });
			items.Add(new Test() { Image = "Images/category_foodndrink.png", Text = "Sex", Text1 = "7.000.000", Text2 = " Còn lại 15.999.000" });

			BudgetList.ItemsSource = items;
			
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
			AddTransaction add = new AddTransaction();
			add.Show();
		}

		private void AddBudgetButton_Click(object sender, RoutedEventArgs e)
		{
			AddBudget add = new AddBudget();
			add.Show();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
        {
			BudgetReportGrid.Visibility = Visibility.Visible;
			Budget.Width = 410;
			BudgetReportGrid.Width = 600;
			
			
			
		}


        private void CloseDetailBudget_Click(object sender, RoutedEventArgs e)
        {
			BudgetReportGrid.Visibility = Visibility.Collapsed;
			Budget.Width = 600;
		}

        private void RunningButton_Click(object sender, RoutedEventArgs e)
        {
			RunningTextblock.Foreground = Brushes.Green;
			RunningTextblock.FontSize = 20;
			RunningButton.BorderThickness = new Thickness(0, 0, 0, 1);
			RunningButton.BorderBrush = Brushes.Green;

			FinishedTextblock.Foreground = Brushes.Black;
			FinishedButton.BorderThickness = new Thickness(0, 0, 0, 0);
			FinishedTextblock.FontSize = 15;

		}

        private void FinishedButton_Click(object sender, RoutedEventArgs e)
        {
			FinishedTextblock.Foreground = Brushes.Green;
			FinishedTextblock.FontSize = 20;
			FinishedButton.BorderThickness = new Thickness(0, 0, 0, 1);
			FinishedButton.BorderBrush = Brushes.Green;

			RunningTextblock.Foreground = Brushes.Black;
			RunningButton.BorderThickness = new Thickness(0, 0, 0, 0);
			RunningTextblock.FontSize = 15;
		}

        private void ViewTransactionListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BudgetLineChart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
			BudgetLineChart.Series.Clear();
			BudgetLineChart.Series.Add(new LineSeries()
			{
				Values = new ChartValues<double> { 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90 },
				LineSmoothness = 0,
				PointGeometry = null,
				PointGeometrySize = 0,
				Title = "Max"
			});
			BudgetLineChart.Series.Add(new LineSeries()
			{
				Values = new ChartValues<double> { 0, 0, 0, 3, 30, 30, 30, 30, 30 },
				LineSmoothness = 0,
				PointGeometry = null,
				PointGeometrySize = 0,
				Title = "Current"
			});
		}

        
    }
}
