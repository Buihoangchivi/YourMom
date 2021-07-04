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
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace YourMom
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	


	public partial class MainWindow : Window
	{

		ObservableCollection<TransactionList> transactionLists = new ObservableCollection<TransactionList>
			{
					new TransactionList()
					{
						Transactions = new ObservableCollection<Transaction>()
						{
							new Transaction
							{
								ID = "gd1",
								Amount = 10000,
								Date = new DateTime(2021, 1, 1),
							},
							new Transaction
							{
								ID = "gd2",
								Amount = 20000,
								Date = new DateTime(2021, 1, 2),
							},
						},

						
						ImagePath = "Images/category_foodndrink.png",
						TransactionType = "Ăn uống"

					},
					new TransactionList()
					{
						Transactions = new ObservableCollection<Transaction>()
						{
							new Transaction
							{
								ID = "gd3",
								Amount = 40000,
								Date = new DateTime(2021, 1, 3),
							},
							new Transaction
							{
								ID = "gd4",
								Amount = 50000,
								Date = new DateTime(2021, 1, 4),
							},
						},
						
						ImagePath = "Images/category_foodndrink.png",
						TransactionType = "Ăn uống"
					},
					new TransactionList()
					{
						Transactions = new ObservableCollection<Transaction>()
						{
							new Transaction
							{
								ID = "gd5",
								Amount = 40000,
								Date =new DateTime(2021, 1, 3),
							},
							new Transaction
							{
								ID = "gd6",
								Amount = 50000,
								Date = new DateTime(2021, 1, 4),
							},
						},
						
						ImagePath = "Images/category_foodndrink.png",
						TransactionType = "Ăn uống"
					},
					new TransactionList()
					{
						Transactions = new ObservableCollection<Transaction>()
						{
							new Transaction
							{
								ID = "gd7",
								Amount = 40000,
								Date = new DateTime(2021, 1, 3),
							},
							new Transaction
							{
								ID = "gd8",
								Amount = 50000,
								Date = new DateTime(2021, 1, 4),
							},
							new Transaction
							{
								ID = "gd9",
								Amount = 50000,
								Date = new DateTime(2021, 1, 4),
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
				Name = "Ăn uống",
				MoneyFund = 9000000,
				SpentMoney = 4000000,
				StartingDate = new DateTime(2021,6,1),
				EndDate = new DateTime(2021,6,30)
			},

			new Budget
			{
				ID = "2",
				ImagePath = "Images/category_foodndrink.png",
				Name = "Mua sắm",
				MoneyFund = 5000000,
				SpentMoney = 2000000,
				StartingDate = new DateTime(2021,6,1),
				EndDate = new DateTime(2021,6,30)
			},

			new Budget
			{
				ID = "3",
				ImagePath = "Images/category_foodndrink.png",
				Name = "Đi chơi",
				MoneyFund = 5000000,
				SpentMoney = 2000000,
				StartingDate = new DateTime(2021,6,1),
				EndDate = new DateTime(2021,6,27)
			},
			new Budget
			{
				ID = "4",
				ImagePath = "Images/category_foodndrink.png",
				Name = "Đi chơi",
				MoneyFund = 5000000,
				SpentMoney = 2000000,
				StartingDate = new DateTime(2021,6,1),
				EndDate =new DateTime(2021,7,27)
			}
		};

		// Danh sách ngân sách đang sử dụng
		List<Budget> runningBudgetList = new List<Budget> { };
		// Danh sách ngân sách đã quá hạn
		List<Budget> finishedBudgetList = new List<Budget> { };

		


		public MainWindow()
		{
			
			InitializeComponent();
			double temp;
			DateTime convert;
			// Hàm xử lý ngân sách
			for (int i = 0; i < budgetList.Count;i++)
			{

				// lấy số ngày còn lại trong ngân sách			

				DateTime currentdate = DateTime.Now;				
				TimeSpan time = budgetList[i].EndDate - currentdate;

				budgetList[i].DaysLeft =  time.Days < 0 ? 0 : time.Days;
				//budgetList[i].DaysLeft = time.Days;


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
				budgetList[i].ShouldSpending_DayMoney = time.Days >= 0 ? Math.Round(budgetList[i].Balance / budgetList[i].DaysLeft, 2) : 0 ;

				// số tiền thực tế chi hàng ngày
				//DateTime startingdate = Convert.ToDateTime(budgetList[i].StartingDate);
				
				budgetList[i].RealitySpending_DayMoney = Math.Round(budgetList[i].SpentMoney / ((currentdate - budgetList[i].StartingDate).Days + 1), 2);

				// số tiền dự kiến chi tiêu
				budgetList[i].ExpectedSpendingMoney = budgetList[i].SpentMoney + budgetList[i].RealitySpending_DayMoney * budgetList[i].DaysLeft;

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
			for (int i = 0; i< transactionLists.Count; i++)
            {
				transactionLists[i].NumberOfTransactions = transactionLists[i].Transactions.Count;
				for (int j = 0; j < transactionLists[i].Transactions.Count;j++)
                {
					transactionLists[i].TotalMoney += transactionLists[i].Transactions[j].Amount;
                }
            }
			

			

            TransactionList.ItemsSource = transactionLists;
			
			





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

        private void BudgetListButton_Click(object sender, RoutedEventArgs e)
        {
			


			BudgetReportGrid.Visibility = Visibility.Visible;
            Budget.Width = 410;
			BudgetListBorder.Width = 410;
			BudgetReportGrid.Width = 600;
            var temp = sender as Button;

			var budgetInfo = temp.DataContext as Budget;
			int lol = 0;
			foreach(var budget in budgetList)
            {
				if (budget.ID == budgetInfo.ID)
                {
					break;
                }
				lol++;
            }

			BudgetInfo.DataContext = budgetList[lol];

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


		// Hàm chặn khi auto scrolling lúc nhấn vào vị trí bất kì trol scrollviewer
        private void ScrollViewer_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
			e.Handled = true;
		}

        
    }
}
