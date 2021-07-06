using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Budget : Category
{
	protected string startingDate;
	protected string endDate;
	// dayLeft kiểu string để hiển thị hết hạn thay vì còn 0 ngày
	protected string daysLeft;
	protected double moneyFund;
	protected double balance;
	protected double spentMoney;
	protected double expectedSpendingMoney;
	protected double shouldSpending_DayMoney;
	protected double realitySpending_DayMoney;
	protected double progress;
	protected string note;

	public string StartingDate
	{
		get
		{
			return startingDate;
		}
		set
		{
			startingDate = value;
			OnPropertyChanged("StartingDate");
		}
	}

	public string EndDate
	{
		get
		{
			return endDate;
		}
		set
		{
			endDate = value;
			OnPropertyChanged("EndDate");
		}
	}

	public string DaysLeft
	{
		get
		{
			return daysLeft;
		}
		set
		{
			daysLeft = value;
			OnPropertyChanged("DaysLeft");
		}
	}

	public double MoneyFund
	{
		get
		{
			return moneyFund;
		}
		set
		{
			moneyFund = value;
			OnPropertyChanged("MoneyFund");
		}
	}

	public double Balance
	{
		get
		{
			return balance;
		}
		set
		{
			balance = value;
			OnPropertyChanged("Balance");
		}
	}

	public double SpentMoney
	{
		get
		{
			return spentMoney;
		}
		set
		{
			spentMoney = value;
			OnPropertyChanged("SpentMoney");
		}
	}

	public double ExpectedSpendingMoney
	{
		get
		{
			return expectedSpendingMoney;
		}
		set
		{
			expectedSpendingMoney = value;
			OnPropertyChanged("ExpectedSpendingMoney");
		}
	}

	public double ShouldSpending_DayMoney
	{
		get
		{
			return shouldSpending_DayMoney;
		}
		set
		{
			shouldSpending_DayMoney = value;
			OnPropertyChanged("ShouldSpending_DayMoney");
		}
	}

	public double RealitySpending_DayMoney
	{
		get
		{
			return realitySpending_DayMoney;
		}
		set
		{
			realitySpending_DayMoney = value;
			OnPropertyChanged("RealitySpending_DayMoney");
		}
	}

	public double Progress
	{
		get
		{
			return progress;
		}
		set
		{
			progress = value;
			OnPropertyChanged("Progress");
		}
	}

	public string Note
	{
		get
		{
			return note;
		}
		set
		{
			note = value;
			OnPropertyChanged("Note");
		}
	}

}
