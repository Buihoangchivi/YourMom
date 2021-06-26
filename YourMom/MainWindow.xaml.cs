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
	


	public partial class MainWindow : Window
	{		
		

		List<Budget> budgetList = new List<Budget>
		{
			new Budget
			{
				ImagePath = "Images/category_foodndrink.png",
				Name = "Ăn uống",
				MoneyFund = 9000000,
				Balance = 5000000,
				StartingDate = "06/06/2021",
				EndDate = "06/30/2021"
			},

			new Budget
			{
				ImagePath = "Images/category_foodndrink.png",
				Name = "Mua sắm",
				MoneyFund = 5000000,
				Balance = 3000000,
				StartingDate = "06/01/2021",
				EndDate = "06/30/2021"
			}
		};





		public MainWindow()
		{
			InitializeComponent();
			double temp;
			DateTime convert;
			foreach (var budget in budgetList)
            {
				// lấy tiến độ hiện tại, làm tròn 2 số sau dấu phẩy
                temp = Math.Round((double)(budget.MoneyFund - budget.Balance) / budget.MoneyFund * 100,2);
                budget.Progress = temp;

				// lấy số ngày còn lại trong ngân sách			
				DateTime startingdate = Convert.ToDateTime(budget.StartingDate);				
				DateTime enddate = Convert.ToDateTime(budget.EndDate);
                TimeSpan time = enddate - startingdate;
                budget.DaysLeft = time.Days;

				// định dạng lại ngày
				convert = DateTime.Parse(budget.StartingDate);
				budget.StartingDate = convert.ToString("dd-MM-yyyy");
				convert = DateTime.Parse(budget.EndDate);
				budget.EndDate = convert.ToString("dd-MM-yyyy");


			}
			BudgetList.ItemsSource = budgetList;
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
			RunningUnderlineTextBlock.Visibility = Visibility.Visible;

			FinishedTextblock.Foreground = Brushes.Black;
			FinishedButton.BorderThickness = new Thickness(0, 0, 0, 0);
			FinishedTextblock.FontSize = 15;
			FinishedUnderlineTextBlock.Visibility = Visibility.Collapsed;

		}

        private void FinishedButton_Click(object sender, RoutedEventArgs e)
        {
			FinishedTextblock.Foreground = Brushes.Green;
			FinishedTextblock.FontSize = 20;
			FinishedButton.BorderThickness = new Thickness(0, 0, 0, 1);
			FinishedButton.BorderBrush = Brushes.Green;
			FinishedUnderlineTextBlock.Visibility = Visibility.Visible;

			RunningTextblock.Foreground = Brushes.Black;
			RunningButton.BorderThickness = new Thickness(0, 0, 0, 0);
			RunningTextblock.FontSize = 15;
			RunningUnderlineTextBlock.Visibility = Visibility.Collapsed;
			
		}

        private void ViewTransactionListButton_Click(object sender, RoutedEventArgs e)
        {
			BudgetDetail win = new BudgetDetail();
			win.Show();
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
    }
}
